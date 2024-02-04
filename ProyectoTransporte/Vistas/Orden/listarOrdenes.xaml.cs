using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Collections.ObjectModel;

namespace ProyectoTransporte.Vistas;

public partial class listarOrdenes : ContentPage
{
    private const string Url = "http://192.168.1.48/transportes/listaOrden.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Ordenes> ordenes;
    public listarOrdenes()
    {
        InitializeComponent();
        ObtenerOrdenes();
    }

    private async void ObtenerOrdenes()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            List<Ordenes> mostrarOrdenes = JsonConvert.DeserializeObject<List<Ordenes>>(content);
            ordenes = new ObservableCollection<Ordenes>(mostrarOrdenes);
            listamosOrdenes.ItemsSource = ordenes;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al obtener la lista de órdenes: {ex.Message}", "OK");
        }
    }

    private void listamosOrdenes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}