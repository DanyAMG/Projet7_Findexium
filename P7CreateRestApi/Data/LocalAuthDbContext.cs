using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Data
{
    public class LocalAuthDbContext : IdentityDbContext
    {
        public LocalAuthDbContext(DbContextOptions<LocalAuthDbContext> options) : base(options)
        {

        }

        protected LocalAuthDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "5a70739a-68b3-448b-8448-c86d93b491aa";
            string reader = "Reader";
            var writerRoledId = "f509ed92-4f47-4131-b8e5-3029cb42e445";
            string writer = "Writer";
            string adminRoleId = "995536c0-b1a7-4d3d-814e-659b86fe854e";
            string admin = "Admin";
            string technicianRoleId = "3e82d213-759f-4ec1-abe4-33225dc028e6";
            string technician = "Technician";
            string visitorRoleId = "1edcdfe0-1a9e-494d-905e-ea86d25b01ce";
            string visitor = "Visitor";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = reader,
                    NormalizedName = reader.ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoledId,
                    ConcurrencyStamp = writerRoledId,
                    Name = writer,
                    NormalizedName = writer.ToUpper()
                },

                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = admin,
                    NormalizedName = admin.ToUpper()
                },

                new IdentityRole
                {
                    Id = technicianRoleId,
                    ConcurrencyStamp = technicianRoleId,
                    Name = technician,
                    NormalizedName = technician.ToUpper()
                },

                new IdentityRole
                {
                    Id = visitorRoleId,
                    ConcurrencyStamp = visitorRoleId,
                    Name = visitor,
                    NormalizedName = visitor.ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
