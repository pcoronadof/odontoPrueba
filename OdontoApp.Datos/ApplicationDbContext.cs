using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdontoApp.Model.Paciente;

namespace OdontoApp.Datos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}