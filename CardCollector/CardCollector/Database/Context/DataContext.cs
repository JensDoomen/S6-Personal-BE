using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardCollector.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CardCollector.Database.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
    }
}
