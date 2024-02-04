using System.Net;

namespace ProyectoTransporte.Vistas;

public partial class IngresarFactura : ContentPage
{
	public IngresarFactura()
	{
		InitializeComponent();
	}

    private void btnAgregarFactura_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nFactura", EntryNFactura.Text);
            parametros.Add("cliente", EntryCliente.Text);
            parametros.Add("productos", EntryProductos.Text);
            parametros.Add("fecha", DatePickerFecha.Date.ToString("yyyy-MM-dd")); // Ajusta el formato de fecha según tu base de datos
            parametros.Add("total", EntryTotal.Text);

            // Enviar datos de factura al servidor
            cliente.UploadValues("http://192.168.1.48/transportes/listaFactura.php", "POST", parametros);

            DisplayAlert("Éxito", "El registro se ha insertado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "CERRAR");
        }
    }

    private void btnVerFacturas_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.listaFacturas());
    }
}