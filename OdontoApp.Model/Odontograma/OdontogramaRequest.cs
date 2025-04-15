using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoApp.Model.Odontograma
{
    public class OdontogramaRequest
    {
        public string Documento { get; set; }
        public OdontogramaEntrada Odontograma { get; set; }
    }
}
