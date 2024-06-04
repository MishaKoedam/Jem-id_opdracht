using Microsoft.EntityFrameworkCore;

namespace Jem_id.webapi.Models
{
    public class PlantContext : DbContext
    {
        public PlantContext(DbContextOptions<PlantContext> options) : base(options) { }
        public DbSet<Plant> Artikel { get; set; }
    }
}
