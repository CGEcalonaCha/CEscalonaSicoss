using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public int Digito { get; set; }
        public int SuperDigito { get; set; }
        public string Fecha { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> Historials { get; set; }
    }
}
