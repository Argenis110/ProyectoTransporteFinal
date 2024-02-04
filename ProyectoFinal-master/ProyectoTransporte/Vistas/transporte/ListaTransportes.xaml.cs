using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Collections.ObjectModel;
using System.Net.Http;
namespace ProyectoTransporte.Vistas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

public partial class ListaTransportes : ContentPage
{
    private const string Url = "http://192.168.1.48/transportes/listaTransporte.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Transporte> trans;
    public ListaTransportes()
	{
		InitializeComponent();
        ObtenerTransportes();
    }

    private async void ObtenerTransportes()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            List<Transporte> mostrarTransportes = JsonConvert.DeserializeObject<List<Transporte>>(content);
            trans = new ObservableCollection<Transporte>(mostrarTransportes);
            listaTransportes.ItemsSource = trans;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al obtener la lista de transportes: {ex.Message}", "OK");
        }
    }

    private void listaTransportes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var transporteSeleccionado = (Transporte)e.SelectedItem;
    }
}