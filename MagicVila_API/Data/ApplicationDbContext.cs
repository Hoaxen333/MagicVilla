using MagicVila_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace MagicVila_API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Vila> vilas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vila>().HasData(
                    new Vila()
                    {
                        Id = 1,
                        Name = "VilaReal",
                        Description = "Description",
                        ImageUrl = "",
                        Ocupantes = 10,
                        MetrosQuadrados = 10,
                        Tarifa = 10,
                        Amenidad = "",
                        DateOfFoundation = DateTime.Now,
                        DataDeActualization = DateTime.Now,

                    }
                );
        }
    }
}
