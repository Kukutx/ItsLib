using ItsLib.DAL.Data;
using Microsoft.OData.ModelBuilder;

namespace ItsLib.WebApi.Models
{
    public static class ODataMapper
    {
        public static void MapUser(ODataConventionModelBuilder modelBuilder)
        {
            modelBuilder.EntityType<User>().Ignore(p => p.PasswordHash);
            modelBuilder.EntityType<User>().Ignore(p => p.DateOfBirth);
            modelBuilder.EntityType<User>().Ignore(p => p.RefreshToken);
            modelBuilder.EntityType<User>().Ignore(p => p.RefreshTokenExpiryTime);
            modelBuilder.EntityType<User>().Ignore(p => p.EmailConfirmed);
            modelBuilder.EntityType<User>().Ignore(p => p.Email);
            modelBuilder.EntityType<User>().Ignore(p => p.PhoneNumber);
            modelBuilder.EntityType<User>().Ignore(p => p.PhoneNumberConfirmed);
            modelBuilder.EntityType<User>().Ignore(p => p.LockoutEnabled);
            modelBuilder.EntityType<User>().Ignore(p => p.ConcurrencyStamp);
            modelBuilder.EntityType<User>().Ignore(p => p.NormalizedEmail);
            modelBuilder.EntityType<User>().Ignore(p => p.NormalizedUserName);
            modelBuilder.EntityType<User>().Ignore(p => p.SecurityStamp);
            modelBuilder.EntityType<User>().Ignore(p => p.TwoFactorEnabled);
            modelBuilder.EntityType<User>().Ignore(p => p.LockoutEnd);
            modelBuilder.EntityType<User>().Ignore(p => p.AccessFailedCount);
        }
    }
}
