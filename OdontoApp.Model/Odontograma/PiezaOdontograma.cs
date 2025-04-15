using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoApp.Model.Odontograma
{
    public class PiezaOdontograma
    {
        public string Etiqueta { get; set; }
        public int Fila { get; set; }
        public List<string> Iconos { get; set; } = new();

        public PartesDiente Partes { get; set; } = new();
    }
}
