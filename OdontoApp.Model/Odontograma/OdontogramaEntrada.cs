using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoApp.Model.Odontograma
{
    public class OdontogramaEntrada
    {
        public string Fecha { get; set; }
        public List<PiezaOdontograma> Piezas { get; set; } = new();
    }
}
