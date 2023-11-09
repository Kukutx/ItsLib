using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

ConfigurationManager configuration = builder.Configuration;
// builder.Services.AddControllers().AddNewtonsoftJson();



string connStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSqlServer<LibDbContext>(connStr);
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<LibDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});



builder.Services.AddSingleton<Mapper>();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<Product>();
modelBuilder.EntitySet<Product>("Product");

modelBuilder.EntityType<User>();
modelBuilder.EntitySet<User>("User");

modelBuilder.EntityType<Category>();
modelBuilder.EntitySet<Category>("Category");

modelBuilder.EntityType<DiscountCode>();
modelBuilder.EntitySet<DiscountCode>("DiscountCode");

modelBuilder.EntityType<ProductUser>().HasKey(t => new { t.ProductId, t.UserId });
modelBuilder.EntitySet<ProductUser>("ProductUser").EntityType.HasKey(t => new { t.ProductId, t.UserId });

modelBuilder.EntityType<UserDiscountCode>().HasKey(t => new { t.DiscountCodeId, t.UserId });
modelBuilder.EntitySet<UserDiscountCode>("UserDiscountCode").EntityType.HasKey(t => new { t.DiscountCodeId, t.UserId });

ODataMapper.MapUser(modelBuilder);
modelBuilder.EnableLowerCamelCase();


builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
}).AddNewtonsoftJson().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));


builder.Services.AddScoped<ILibOfWork, LibOfWork>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 7 Web API",
        Description = "Authentication and Authorization in ASP.NET 7 with JWT and Swagger"
    });
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET 7 Web API v1"));

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetService<LibDbContext>();
    ctx.Database.Migrate();

}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

// app.MapControllers();

app.Run();




