using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SkyFan.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SkyFan.Models.Vendor> Vendors { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.VendorProduct> VendorProducts { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.Purchase> Purchases { get; set; }


        
        public System.Data.Entity.DbSet<SkyFan.Models.Dealer> Dealer { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.DealerProduct> DealerProducts { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.Sale> Sale { get; set; }


       

       


        public System.Data.Entity.DbSet<SkyFan.Models.Employee> Emloyee { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.StockOut> StockOut { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.Fan> Fans { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.FanPart> FanParts { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<SkyFan.Models.OrderDetail> OrderDetails { get; set; }
    }
}