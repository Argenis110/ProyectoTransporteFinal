using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTransporte.Modelos
{
    public class Ordenes
    {
        public int idOrden { get; set; }
        public int nOrden { get; set; }
        public string observacion { get; set; }
        public string destino { get; set; }
        public DateTime fecha { get; set; }
        public int fkFactura { get; set; }
        public int fkTransportista { get; set; }
        public int fkUsuario { get; set; }
        public int fkTransporte { get; set; }
    }
}
