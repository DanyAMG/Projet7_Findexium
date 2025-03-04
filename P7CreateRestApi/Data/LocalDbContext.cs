using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : IdentityDbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<BidList> Bids { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<RuleName> Rules { get; set; }

    


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set;}
    }
}