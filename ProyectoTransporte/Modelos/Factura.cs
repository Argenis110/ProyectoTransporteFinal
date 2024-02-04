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
