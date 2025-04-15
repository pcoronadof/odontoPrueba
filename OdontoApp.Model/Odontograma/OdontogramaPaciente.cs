using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoApp.Model.Odontograma
{
    public class OdontogramaPaciente
    {
        public string Documento { get; set; }
        public List<OdontogramaEntrada> Odontogramas { get; set; } = new();
    }
}
