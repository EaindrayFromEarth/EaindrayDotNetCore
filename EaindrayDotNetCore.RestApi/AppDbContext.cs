using AKKLTZDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaindrayDotNetCore.RestApi
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)  
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ALTDotNetCore;User Id=sa;Password=sa@123; Encrypt=True; Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }


        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
