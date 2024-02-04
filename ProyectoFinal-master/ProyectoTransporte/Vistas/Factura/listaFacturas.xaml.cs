using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Collections.ObjectModel;

namespace ProyectoTransporte.Vistas;

public partial class listaFacturas : ContentPage
{
    private const string Url = "http://192.168.1.48/transportes/listaFactura.php"; // Reemplaza con la URL correcta
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Factura> facturas;
    public listaFacturas()
	{
		InitializeComponent();
        ObtenerFacturas();
    }

    private async void ObtenerFacturas()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            List<Factura> mostrarFacturas = JsonConvert.DeserializeObject<List<Factura>>(content);
            facturas = new ObservableCollection<Factura>(mostrarFacturas);
            listamosFacturas.ItemsSource = facturas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al obtener la lista de facturas: {ex.Message}", "OK");
        }
    }

    private void listaFacturas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var facturaSeleccionada = (Factura)e.SelectedItem;

        ((ListView)sender).SelectedItem = null;
    }
}