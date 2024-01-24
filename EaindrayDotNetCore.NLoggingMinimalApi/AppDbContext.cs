using EaindrayDotNetCore.NLoggingMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EaindrayDotNetCore.NLoggingMinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
