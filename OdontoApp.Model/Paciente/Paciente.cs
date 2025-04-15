using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdontoApp.Model.Odontograma;

namespace OdontoApp.Model.Paciente
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        public string Documento { get; set; }

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; } 
    }
}
 
