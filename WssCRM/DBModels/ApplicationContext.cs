using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class ApplicationContext : DbContext
    {
        //dotnet ef migrations add nameMig
        //dotnet ef database update
        //dotnet ef database update LastGoodMigration
        //dotnet ef migrations remove
        public DbSet<Company> Companies { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<AbstractPoint> AbstractPoints { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.name)
                .IsUnique();
            modelBuilder.Entity<Stage>()
                //.ToTable("Stage")
                .HasIndex(s => new { s.Name, s.CompanyID })
                .IsUnique()
                ;
            modelBuilder.Entity<AbstractPoint>()
                .HasIndex(p => new { p.name, p.StageID })
                .IsUnique();
            modelBuilder.Entity<Manager>()
                .HasIndex(m => new { m.name, m.CompanyID })
                .IsUnique();

        }
    }
}
