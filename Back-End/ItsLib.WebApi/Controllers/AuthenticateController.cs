using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace ItsLib.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private ILibOfWork _repo;
        private readonly Mapper _map;

        public AuthenticateController
        (
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILibOfWork repo,
            Mapper mapper
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _repo = repo;
            _map = mapper;
        }


        [HttpPut("Switch/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Switch(string id)
        {
            User? userLog = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userLog == null)
                return NotFound();
            if (userLog.Id == id)
                return BadRequest("Non puoi auto disabilitarti");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            user.IsDisabled = !user.IsDisabled;
            if (_repo.UserRepo.Update(user))
                return Ok();
            return BadRequest();
        }

        [HttpGet("GetUserRole/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            RoleModel role = new RoleModel();
            role.Roles = await _userManager.GetRolesAsync(user);
            if (role.Roles == null)
                return NotFound();
            return Ok(role);
        }

        [HttpGet("Admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAdmin()
        {
            List<User> adminUsers = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            if (adminUsers != null)
                return Ok(adminUsers.ConvertAll(_map.MapEntityToModel));
            return BadRequest();
        }

        [HttpGet("User")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetUser()
        {
            List<User> User = _userManager.GetUsersInRoleAsync("User").Result.ToList();
            if (User != null)
                return Ok(User.ConvertAll(_map.MapEntityToModel));
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user.IsDisabled == true)
                return BadRequest("Sei stato disabilitato, chiedi ad un Admin di riabilitarti");
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return BadRequest("Email e/o password sbagliata");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                DateOfBirth = (DateTime)model.DateOfBirth,
                FiscalCode = model.FiscalCode,
                IsDisabled = false
            };
            user.LoyaltyCardCode = _repo.UserRepo.NewLoyaltyCardCode();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                DateOfBirth = (DateTime)model.DateOfBirth,
                FiscalCode = model.FiscalCode,
                IsDisabled = false
            };
            user.LoyaltyCardCode = _repo.UserRepo.NewLoyaltyCardCode();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            });
        }

        [HttpPost]
        [Route("revoke/{username}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [HttpPost]
        [Route("revoke-all")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        [HttpPut("UserToAdmin/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UserToAdmin(string id)
        {
            User? userLog = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userLog.Id == id)
                return BadRequest("Non puoi modificare il tuo ruolo");
            User? user = _repo.UserRepo.Get(id);
            if (user == null)
                return NotFound("Utente non trovato");
            if (user.IsDisabled == true)
                return BadRequest("Utente disabilitato");
            var result = await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            if (result.Succeeded)
                return Ok("Utente promosso con successo");
            return BadRequest("Passaggio da utente ad admin fallito");
        }

        [HttpPut("AdminToUser/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AdminToUser(string id)
        {
            User? userLog = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userLog.Id == id)
                return BadRequest("Non puoi modificare il tuo ruolo");
            User? user = _repo.UserRepo.Get(id);
            if (user == null)
                return NotFound("Utente non trovato");
            if (user.IsDisabled == true)
                return BadRequest("Utente disabilitato");
            var result = await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
            if (result.Succeeded)
                return Ok("Utente retrocesso con successo");
            return BadRequest("Passaggio da admin ad utente fallito");
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            User? userLog = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userLog == null)
                return NotFound("Utente non trovato");
            //  var t = await _userManager.GeneratePasswordResetTokenAsync(userLog);
            var t = await _userManager.ChangePasswordAsync(userLog, oldPassword, newPassword);
            return Ok("Password cambiata correttamente");
        }


        [HttpGet("MyInfo")]
        [Authorize]
        public async Task<IActionResult> GetMyInfo()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return NotFound("Utente non trovato");
            return Ok(_map.MapEntityToModel(user));
        }

    }
}
