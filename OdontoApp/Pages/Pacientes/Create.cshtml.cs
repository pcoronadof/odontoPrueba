using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; 
using OdontoApp.Model.Paciente;
using OdontoApp.Storage; // Contexto

namespace OdontoApp.Pages.Pacientes
{
 
    public class CreateModel : PageModel
    {
        [BindProperty]
        public PacienteInputModel Paciente { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var nuevo = new Paciente
            {
                Nombre = Paciente.Nombre,
                Apellido = Paciente.Apellido,
                FechaNacimiento = Paciente.FechaNacimiento,
                Sexo = Paciente.Sexo,
                Documento = Paciente.Documento,
                Observaciones = Paciente.Observaciones
            };

            PacienteStorage.AgregarPaciente(nuevo);

            return RedirectToPage("Index");
        }

        public class PacienteInputModel
        {
            [Required]
            public string Nombre { get; set; }

            [Required]
            public string Apellido { get; set; }

            [DataType(DataType.Date)]
            public DateTime FechaNacimiento { get; set; }

            [Required]
            public string Sexo { get; set; }

            [Required]
            public string Documento { get; set; }


            public string Observaciones { get; set; }
        }
    }
}