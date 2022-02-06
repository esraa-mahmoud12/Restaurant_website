using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Restaurant_App.Models
{
    public class DBContext : DbContext
        {
            public DBContext() : base("DBContext")
            {

            }
        
            public DbSet<User> Users { get; set; }
            public DbSet<Meal> Meals { get; set; }
            public DbSet<Order> Orders { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
    }
