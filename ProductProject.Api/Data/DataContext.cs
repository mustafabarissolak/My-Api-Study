using Microsoft.EntityFrameworkCore;
using ProductProject.Api.Core;

namespace ProductProject.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ProductEntity> ProductEntities { get; set; }
    }
}
