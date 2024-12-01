using App.Core.Interface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext:DbContext,IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Card> Card { get; set; } // Add the Card entity>
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<CartMaster> CartMaster { get; set; } // Add the CartMaster entity>
        public DbSet<SalesMaster> SalesMaster { get; set; }
        public DbSet<SalesDetail> SalesDetail { get; set; }
        public DbSet<Product> Product { get; set; } // Add the Product entity>
        public DbSet<Otp> Otp { get; set; }
        public DbSet<Role> Role { get; set; }

        public IDbConnection GetConnection()
        {
            return this.Database.GetDbConnection();
        }
    }
}
