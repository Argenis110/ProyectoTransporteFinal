namespace ProyectoTransporte.Vistas;
using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
public partial class ListaConductores : ContentPage
{
    private const string Url = "http://192.168.56.1/Transportos/listarTransportistas.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Transportista> transportistas;
    public ListaConductores()
    {
        InitializeComponent();
        ObtenerTransportistas();
    }

    private async void ObtenerTransportistas()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            List<Transportista> mostrarTransportistas = JsonConvert.DeserializeObject<List<Transportista>>(content);
            transportistas = new ObservableCollection<Transportista>(mostrarTransportistas);
            listaTransportistas.ItemsSource = transportistas;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al obtener la lista de transportistas: {ex.Message}", "OK");
        }
    }
    private void listaTransportistas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var transportistaSeleccionado = (Transportista)e.SelectedItem;
    }
}
