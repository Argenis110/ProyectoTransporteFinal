using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTransporte.Modelos
{
    public class Transportista
    {
        public int idTransportista { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public string cedula { get; set; }
        public string observacion { get; set; }
        public string tipoLicencia { get; set; }
        public int estado { get; set; }
        public string telefono { get; set; }
        public int fkUsuario { get; set; }

    }
}
