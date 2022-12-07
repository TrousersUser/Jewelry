using Jewelery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Jewelery.Contexts
{
    public class JewelryDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Orders> Orders { get; set; } = null!;
        public DbSet<JewelryMaster> JeweleryMasters { get; set; } = null!;
        public DbSet<Buyer> Buyers { get; set; } = null!;
        public JewelryDbContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder wayBuilder)
        {
            /*var builder = new ConfigurationBuilder();
            
            
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("RoadsToDBs.json", optional: false, reloadOnChange: true)
                 .Build();
            wayBuilder.UseSqlServer(config["ConnectionStrings:first"]);  */
            wayBuilder.UseSqlServer(@"Data Source=SEASON\SERVER;Database=Jewelry;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelCreator)
        {
            base.OnModelCreating(modelCreator);
            modelCreator.Entity<Users>().HasData(new Users()
            {
                ID = 1,
                Login = "Owner",
                Password = "password123",
                DateOfRegistry = DateTime.Now,
                Permissions = "Administrator",
                PhoneNumber = "7-228-228-88-11",
                RegistryStatus = true
               
            });;

            modelCreator.Entity<Buyer>().HasData(new Buyer()
            {
                Buyer_DateOfRegistry = DateTime.Now,
                Buyer_ID = 1,
                Buyer_Name = "ЖидЕвгений"

            });
            modelCreator.Entity<JewelryMaster>().HasData(new JewelryMaster()
            {
                Master_ID = 1,
            });
            modelCreator.Entity<Orders>().HasData(new Orders()
            {
                OrderID = 1,
                OrderDescription = "Ветвистое кольцо, 35 мм в длину, 15 в ширину",
                OrderStatus = "Не замечен составом работников организации"
            });
        }
        public IQueryable<Users> UsersGetAll()
        {
            return Users;
        }

        public IQueryable<Buyer> BuyerGetAll()
        {
            return Buyers;
        }
        public IQueryable<JewelryMaster> MastersGetAll()
        {
            return JeweleryMasters;
        }
        public IQueryable<Orders> OrdersGetAll()
        {
            return Orders;
        }

    }
}
