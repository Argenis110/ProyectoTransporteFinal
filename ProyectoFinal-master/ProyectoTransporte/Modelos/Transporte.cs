using System;

namespace ProyectoTransporte.Modelos
{
    public class Transporte
    {
        public int IdTransporte { get; set; }
        public string nMatricula { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        public string placa { get; set; }
        public int estado { get; set; }
        public string observacion { get; set; }
        public int fkTransportista { get; set; }
    }
}
