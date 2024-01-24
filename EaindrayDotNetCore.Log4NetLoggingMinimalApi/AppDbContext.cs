using EaindrayDotNetCore.Log4NetLoggingMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EaindrayDotNetCore.Log4NetLoggingMinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
