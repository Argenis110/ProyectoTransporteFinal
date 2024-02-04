using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Net;

namespace ProyectoTransporte.Vistas;

public partial class IngresarTransporte : ContentPage
{
    private List<Transportista> listaTransportistas;
    public IngresarTransporte()
    {
        InitializeComponent();
        ObtenerYConfigurarTransportistasAsync();
    }

    private async Task ObtenerYConfigurarTransportistasAsync()
    {
        try
        {
            // Obtener la lista de transportistas desde el servicio web
            listaTransportistas = await ObtenerTransportistasAsync();

            // Configurar el Picker con la lista de nombres de transportistas
            foreach (var transportista in listaTransportistas)
            {
                TransportistaPicker.Items.Add(transportista.nombre);
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la configuración del Picker
            await DisplayAlert("Error", $"Error al configurar transportistas: {ex.Message}", "OK");
        }
    }

    private async Task<List<Transportista>> ObtenerTransportistasAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "http://192.168.100.3/Transportos/listarTransportistas.php";

                // Realizar una solicitud GET al servicio web
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Verificar si la solicitud fue exitosa (código de estado 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar la respuesta JSON
                    string content = await response.Content.ReadAsStringAsync();
                    List<Transportista> transportistas = JsonConvert.DeserializeObject<List<Transportista>>(content);

                    return transportistas;
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    throw new Exception($"Error al obtener transportistas. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la solicitud al servicio web
            throw new Exception($"Error en la solicitud al servicio web: {ex.Message}");
        }
    }
    private void btnAgregarTransporte_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nMatricula", EntryMatricula.Text);
            parametros.Add("marca", EntryMarca.Text);
            parametros.Add("modelo", EntryModelo.Text);
            parametros.Add("tipo", EntryTipo.Text);
            parametros.Add("placa", EntryPlaca.Text);
            parametros.Add("estado", EntryEstado.Text);
            parametros.Add("observacion", EntryObservacion.Text);

            // Obtener el transportista seleccionado del Picker
            var transportistaSeleccionado = listaTransportistas[TransportistaPicker.SelectedIndex];
            parametros.Add("fkTransportista", transportistaSeleccionado.idTransportista.ToString());

            // Enviar datos de transporte al servidor
            cliente.UploadValues("http://192.168.100.3/Transportos/insertarTransporte.php", "POST", parametros);

            DisplayAlert("Éxito", "El registro se ha insertado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "CERRAR");
        }
    }

    private void btnTransportes_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.ListaTransportes());
    }
}