using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdontoApp.Model.Odontograma;
using OdontoApp.Model.Paciente;
using OdontoApp.Storage;

namespace OdontoApp.Pages.Atenciones
{

    public class IndexModel : PageModel
    {
        public string MetodoHttp => HttpContext.Request.Method;
        [BindProperty]
        public string DocumentoBuscado { get; set; }

        public List<OdontogramaEntrada> OdontogramasDelPaciente { get; set; } = new();

        public Paciente? PacienteEncontrado { get; set; }

        public void OnPost()
        {
            var pacientes = PacienteStorage.ObtenerTodos();
            PacienteEncontrado = pacientes.FirstOrDefault(p => p.Documento == DocumentoBuscado);

            if (PacienteEncontrado == null)
            {
                TempData["Error"] = "Paciente no encontrado.";
                return;
            }

            var rutaOdontogramas = Path.Combine("Data", "odontogramas.json");
            if (System.IO.File.Exists(rutaOdontogramas))
            {
                var contenido = System.IO.File.ReadAllText(rutaOdontogramas);
                var todos = JsonSerializer.Deserialize<List<OdontogramaPaciente>>(contenido);
                var pacienteOdonto = todos?.FirstOrDefault(p => p.Documento == PacienteEncontrado.Documento);
                if (pacienteOdonto != null)
                {
                    OdontogramasDelPaciente = pacienteOdonto.Odontogramas;
                }
            }

        }
    }
}
