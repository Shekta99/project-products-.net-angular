using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

public class ProductsContext : DbContext
{
    public ProductsContext(DbContextOptions<ProductsContext> options)
        : base(options)
    {
    }

    public DbSet<ProductItem> ProductItems { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

}