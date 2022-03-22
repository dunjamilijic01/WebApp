using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class KantinaContext:DbContext
    {
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<Jelo> Jela { get; set; }
        public DbSet<Meni> Meniji { get; set; }

        public DbSet<Kantina> Kantine { get; set; }

        public KantinaContext(DbContextOptions options): base(options)
        {
        }
            
    }
}
