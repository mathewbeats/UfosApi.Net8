using ApiUfoCasesNet8.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiUfoCasesNet8.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> opciones): base(opciones)
        { 

        }

        public DbSet<Ufo> Ufo { get; set; }

        public DbSet<Testigo> Testigo { get; set; } 

        public DbSet<UsuariosUfos> UsuariosUfos { get; set; }
    }
}
