namespace ProyectoTransporte.Vistas;
using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using ProyectoTransporte.Vistas.Conductor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
public partial class ListaConductores : ContentPage
{
    private const string Url = "http://192.168.1.48/transportes/listaTransportista.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Transportista> transportistas;
    public ListaConductores()
	{
		InitializeComponent();
        ObtenerTransportistas();
    }
    private void listaTransportistas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is Transportista selectedTransportista)
            {
                Navigation.PushAsync(new Vistas.Conductor.Actu_ElimiConductor(selectedTransportista));
                listaTransportistas.SelectedItem = null;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error al seleccionar el conductor: {ex.Message}", "OK");
        }
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
    
}