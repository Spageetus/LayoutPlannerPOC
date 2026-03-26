using Microsoft.EntityFrameworkCore;

namespace LayoutPlannerPOC.Data
{
    public class FactoryComponentContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public FactoryComponentContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("ComponentDB"));
        }

        public DbSet<FactoryComponent> FactoryComponents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FactoryComponent>()
                .ToTable("FactoryComponents");
            modelBuilder.Entity<FactoryComponent>()
                .HasData(
                    new FactoryComponent
                    {
                        Id = 1,
                        Name = "Constructor",
                        ImageFilePath = null,
                        Height = 10,
                        Width = 5,
                        Rotation = 0,
                        X = 10,
                        Y = 0
                    },

                    new FactoryComponent
                    {
                        Id = 2,
                        Name = "Smelter",
                        ImageFilePath = null,
                        Height = 4,
                        Width = 10,
                        Rotation = 0,
                        X = 15,
                        Y = 5
                    }
                );
        }
    }
}
