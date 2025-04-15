using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdontoApp.Model.Diente;
using OdontoApp.Model.Odontograma;
using OdontoApp.Model.Paciente;
using OdontoApp.Storage;
using System.Text.Json;

namespace OdontoApp.Pages.Atenciones
{
    [IgnoreAntiforgeryToken]
    public class OdontogramaModel : PageModel
    {
        [BindProperty]

        public List<OdontogramaEntrada> OdontogramasDelPaciente { get; set; } = new();
        public List<Paciente> Pacientes { get; set; } = new();
        public Paciente? Paciente { get; set; }

        public List<Diente> MaestroDientes { get; set; } = new();
        public List<TipoDiente> MaestroTipos { get; set; } = new();
        public List<ImagenEtiqueta> ImagenesEtiquetas { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Fecha { get; set; }

        public OdontogramaEntrada? OdontogramaCargado { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Documento { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string TipoSeleccionado { get; set; } = "Permanente";

        public void OnGet()
        {
            // Buscar paciente desde el JSON
            Pacientes = PacienteStorage.ObtenerTodos();
            Paciente = Pacientes.FirstOrDefault(p => p.Documento == Documento);

            if (Paciente == null)
            {
                TempData["Error"] = "Paciente no encontrado.";
                return;
            }

            // Leer JSON de dientes
            var ruta = "Data/MaestroDientes.json";
            if (System.IO.File.Exists(ruta))
            {
                var json = System.IO.File.ReadAllText(ruta);
                var maestro = JsonSerializer.Deserialize<MaestroDientesContainer>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                MaestroDientes = maestro?.Dientes
                    .Where(d => d.Tipo.Equals(TipoSeleccionado, StringComparison.OrdinalIgnoreCase))
                    .ToList() ?? new();

                MaestroTipos = maestro?.TipoDiente ?? new();
            }


            var rutaEtiquetas = Path.Combine("Data", "imagenes.json");
            if (System.IO.File.Exists(rutaEtiquetas))
            {
                var jsonEtiquetas = System.IO.File.ReadAllText(rutaEtiquetas);
                ImagenesEtiquetas = JsonSerializer.Deserialize<List<ImagenEtiqueta>>(jsonEtiquetas, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
            }
        }

        public string ObtenerNombreTipo(int idTipo)
        {
            return MaestroTipos.FirstOrDefault(t => t.Id == idTipo)?.Nombre ?? $"Tipo {idTipo}";
        }


   

        // Clase temporal para recibir datos del cliente
        public class OdontogramaRegistroTemp
        {
            public string Documento { get; set; }
            public OdontogramaEntrada Odontograma { get; set; }
        }

 
        public async Task<IActionResult> OnPostGuardarAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var json = await reader.ReadToEndAsync();

            var data = JsonSerializer.Deserialize<OdontogramaRequest>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            if (data == null || data.Documento == null || data.Odontograma == null)
                return BadRequest("Datos inválidos");

            // Leer JSON existente
            var ruta = Path.Combine("Data", "odontogramas.json");
            List<OdontogramaPaciente> registros = new();

            if (System.IO.File.Exists(ruta))
            {
                if (new FileInfo(ruta).Length == 0)
                {
                    await System.IO.File.WriteAllTextAsync(ruta, "[]");
                }

                var contenido = await System.IO.File.ReadAllTextAsync(ruta);

                if (!string.IsNullOrWhiteSpace(contenido))
                {
                    registros = JsonSerializer.Deserialize<List<OdontogramaPaciente>>(contenido) ?? new();
                }
            }


            // Buscar paciente
            var paciente = registros.FirstOrDefault(p => p.Documento == data.Documento); 

            if (paciente == null)
            {
                paciente = new OdontogramaPaciente
                {
                    Documento = data.Documento,
                    Odontogramas = new List<OdontogramaEntrada>()
                };
                registros.Add(paciente);
            }

            paciente.Odontogramas.Add(data.Odontograma);

            var nuevoJson = JsonSerializer.Serialize(registros, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync(ruta, nuevoJson);

            return new JsonResult(new { mensaje = "Guardado correctamente" });
        }

    }
}
