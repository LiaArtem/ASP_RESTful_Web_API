using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASP_RESTful_Web_API.Models
{
    public partial class Database_Context : DbContext
    {
        public Database_Context()
        {
        }

        public Database_Context(DbContextOptions<Database_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<BuffImportData> BuffImportData { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory()) // установка пути к текущему каталогу
                                    .AddJsonFile("appsettings.json"); // получаем конфигурацию из файла appsettings.json                                    
                                                                      // создаем конфигурацию
                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuffImportData>(entity =>
            {
                entity.Property(e => e.SysDate).HasDefaultValueSql("(sysdatetime())");
            });            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
