using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using OdontoApp.Datos;
using OdontoApp.Model.Paciente;
using OdontoApp.Storage;

namespace OdontoApp.Pages.Pacientes
{
    public class IndexModel : PageModel
    {
        public List<Paciente> Pacientes { get; set; } = new();

        public void OnGet()
        {
            Pacientes = PacienteStorage.ObtenerTodos();
        }
    }
}