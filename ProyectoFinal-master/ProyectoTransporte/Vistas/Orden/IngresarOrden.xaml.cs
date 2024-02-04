using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Net;

namespace ProyectoTransporte.Vistas;

public partial class IngresarOrden : ContentPage
{
    private List<Transportista> listaTransportistas;
    private List<Usuario> listaUsuarios;
    private List<Transporte> listaTransportes;
    private List<Factura> listaFacturas;

    public IngresarOrden()
	{
		InitializeComponent();
        CargarDatosPickerAsync();
    }

    private async Task CargarDatosPickerAsync()
    {
        try
        {
            // Cargar lista de transportistas
            listaTransportistas = await ObtenerTransportistasAsync();
            foreach (var transportista in listaTransportistas)
            {
                TransportistaPicker.Items.Add(transportista.nombre);
            }

            // Cargar lista de usuarios
            listaUsuarios = await ObtenerUsuariosAsync();
            foreach (var usuario in listaUsuarios)
            {
                UsuarioPicker.Items.Add(usuario.nombre);
            }

            // Cargar lista de transportes
            listaTransportes = await ObtenerTransportesAsync();
            foreach (var transporte in listaTransportes)
            {
                TransportePicker.Items.Add(transporte.nMatricula);
            }

            // Cargar lista de facturas
            listaFacturas = await ObtenerFacturasAsync();
            foreach (var factura in listaFacturas)
            {
                FacturaPicker.Items.Add(factura.nFactura.ToString());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
        }
    }

    private async Task<List<Transportista>> ObtenerTransportistasAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "http://192.168.1.48/transportes/listaTransportista.php";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Transportista> transportistas = JsonConvert.DeserializeObject<List<Transportista>>(content);

                    return transportistas;
                }
                else
                {
                    throw new Exception($"Error al obtener transportistas. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error en la solicitud al servicio web de transportistas: {ex.Message}");
        }
    }

    private async Task<List<Usuario>> ObtenerUsuariosAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "http://192.168.1.48/transportes/usuario.php";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);

                    return usuarios;
                }
                else
                {
                    throw new Exception($"Error al obtener usuarios. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error en la solicitud al servicio web de usuarios: {ex.Message}");
        }
    }

    private async Task<List<Transporte>> ObtenerTransportesAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "http://192.168.1.48/transportes/listaTransporte.php";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Transporte> transportes = JsonConvert.DeserializeObject<List<Transporte>>(content);

                    return transportes;
                }
                else
                {
                    throw new Exception($"Error al obtener transportes. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error en la solicitud al servicio web de transportes: {ex.Message}");
        }
    }

    private async Task<List<Factura>> ObtenerFacturasAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string url = "http://192.168.1.48/transportes/listaFactura.php";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Factura> facturas = JsonConvert.DeserializeObject<List<Factura>>(content);

                    return facturas;
                }
                else
                {
                    throw new Exception($"Error al obtener facturas. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error en la solicitud al servicio web de facturas: {ex.Message}");
        }
    }


    private void btnAgregarOrden_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();

            // Obtener los valores de los controles de la interfaz de usuario
            parametros.Add("nOrden", EntryNOrden.Text);
            parametros.Add("observacion", EntryObservacion.Text);
            parametros.Add("destino", EntryDestino.Text);
            parametros.Add("fecha", DatePickerFecha.Date.ToString("yyyy-MM-dd"));

            // Obtener los índices seleccionados de los Pickers
            var transportistaSeleccionado = listaTransportistas[TransportistaPicker.SelectedIndex];
            var usuarioSeleccionado = listaUsuarios[UsuarioPicker.SelectedIndex];
            var transporteSeleccionado = listaTransportes[TransportePicker.SelectedIndex];
            var facturaSeleccionada = listaFacturas[FacturaPicker.SelectedIndex];

            // Asignar los valores de las claves foráneas
            parametros.Add("fkTransportista", transportistaSeleccionado.idTransportista.ToString());
            parametros.Add("fkUsuario", usuarioSeleccionado.idUsuario.ToString());
            parametros.Add("fkTransporte", transporteSeleccionado.IdTransporte.ToString());
            parametros.Add("fkFactura", facturaSeleccionada.idFactura.ToString());

            // Enviar datos de la orden al servidor
            cliente.UploadValues("http://192.168.1.48/transportes/listaOrden.php", "POST", parametros);

            DisplayAlert("Éxito", "El registro se ha insertado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "CERRAR");
        }
    }

    private void btnVerOrdenes_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.listarOrdenes());
    }
}