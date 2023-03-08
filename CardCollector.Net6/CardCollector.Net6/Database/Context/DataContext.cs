using CardCollector.Net6.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CardCollector.Net6.Database.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
    }
}
