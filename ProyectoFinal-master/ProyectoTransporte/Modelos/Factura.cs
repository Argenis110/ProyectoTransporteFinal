using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTransporte.Modelos
{
    public class Factura
    {
        public int idFactura { get; set; }
        public int nFactura { get; set; }
        public string cliente { get; set; }
        public string productos { get; set; }
        public DateTime fecha { get; set; }
        public double total { get; set; }
    }
}
