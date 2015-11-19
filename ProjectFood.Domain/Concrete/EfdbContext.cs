using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Concrete
{
    public class EfdbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public EfdbContext() : base("EfdbContext")
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        static EfdbContext()
        {
            Database.SetInitializer<EfdbContext>(new DataInitializer());
        }

        public static EfdbContext Create()
        {
            return new EfdbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("UserId");
            modelBuilder.Entity<Role>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
        }
    }
}
