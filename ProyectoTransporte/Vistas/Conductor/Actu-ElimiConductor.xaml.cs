using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System.Net;

namespace ProyectoTransporte.Vistas.Conductor;

public partial class Actu_ElimiConductor : ContentPage
{
    private Transportista conductor;
    public Actu_ElimiConductor(Transportista conductor)
    {
        InitializeComponent();
        this.conductor = conductor;
        LabelId.Text = conductor.idTransportista.ToString();
        EntryNombre.Text = conductor.nombre;
        EntryApellido.Text = conductor.apellido;
        EntryEdad.Text = conductor.edad.ToString();
        EntryCedula.Text = conductor.cedula;
        EntryObservacion.Text = conductor.observacion;
        EntryLicencia.Text = conductor.tipoLicencia;
        EntryEstado.Text = conductor.estado.ToString();
        EntryTelefono.Text = conductor.telefono;

    }
    private async Task<List<Usuario>> ObtenerUsuariosAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {

                string url = "http://192.168.1.48/transportes/usuario.php";

                // Realizar una solicitud GET al servicio web
                HttpResponseMessage response = await httpClient.GetAsync(url);

                // Verificar si la solicitud fue exitosa (código de estado 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Leer y deserializar la respuesta JSON
                    string content = await response.Content.ReadAsStringAsync();
                    List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);

                    return usuarios;
                }
                else
                {
                    // Manejar el caso en que la solicitud no fue exitosa
                    throw new Exception($"Error al obtener usuarios. Código de estado: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la solicitud al servicio web
            throw new Exception($"Error en la solicitud al servicio web: {ex.Message}");
        }
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            string codigo = LabelId.Text;
            string nombre = EntryNombre.Text;
            string apellido = EntryApellido.Text;
            string edad = EntryEdad.Text;
            string cedula = EntryCedula.Text;
            string observacion = EntryObservacion.Text;
            string tipoLicencia = EntryLicencia.Text;
            string estado = EntryEstado.Text;
            string telefono = EntryTelefono.Text;


            string url = "http://192.168.1.48/transportes/listaTransportista.php?codigo=" + codigo + "&nombre=" + nombre + "&apellido=" + apellido + "&edad=" + edad + "&cedula=" + cedula + "&observacion=" + observacion + "&tipoLicencia=" + tipoLicencia + "&estado=" + estado + "&telefono=" + telefono;
            WebClient cliente = new WebClient();


            var parametros = new System.Collections.Specialized.NameValueCollection();

            cliente.UploadValues(url, "PUT", parametros);


            DisplayAlert("Éxito", "Se ha modificado el conductor con éxito", "OK");

            Navigation.PushAsync(new ListaConductores());
        }
        catch (Exception ex)
        {
            DisplayAlert("ERROR", ex.Message, "CERRAR");
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            string codigo = LabelId.Text;

            string url = "http://192.168.1.48/transportes/listaTransportista.php?codigo=" + codigo;
            WebClient cliente = new WebClient();

            cliente.UploadValues(url, "DELETE", new System.Collections.Specialized.NameValueCollection());

            // Mostrar alerta de eliminación exitosa
            DisplayAlert("Éxito", "Se ha eliminado el conductor con éxito", "OK");

            Navigation.PushAsync(new ListaConductores());
        }
        catch (Exception ex)
        {
            DisplayAlert("ERROR", ex.Message, "CERRAR");
        }
    }


}