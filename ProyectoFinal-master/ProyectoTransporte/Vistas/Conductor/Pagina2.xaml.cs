namespace ProyectoTransporte.Vistas;

using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
public partial class Pagina2 : ContentPage
{
    private List<Usuario> listaUsuarios;

    public Pagina2()
	{
		InitializeComponent();
        ObtenerYConfigurarUsuariosAsync();
    }
    private async Task ObtenerYConfigurarUsuariosAsync()
    {
        try
        {
            // Obtener la lista de usuarios desde el servicio web
            listaUsuarios = await ObtenerUsuariosAsync();

            // Configurar el Picker con la lista de nombres de usuarios
            foreach (var usuario in listaUsuarios)
            {
                UsuarioPicker.Items.Add(usuario.nombre);
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la configuración del Picker
            await DisplayAlert("Error", $"Error al configurar usuarios: {ex.Message}", "OK");
        }
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


    private void btnguardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", EntryNombre.Text);
            parametros.Add("apellido", EntryApellido.Text);
            parametros.Add("edad", EntryEdad.Text);
            parametros.Add("cedula", EntryCedula.Text);
            parametros.Add("observacion", EntryObservacion.Text);
            parametros.Add("tipoLicencia", EntryLicencia.Text);
            parametros.Add("estado", EntryEstado.Text);
            parametros.Add("telefono", EntryTelefono.Text);
            var usuarioSeleccionado = listaUsuarios[UsuarioPicker.SelectedIndex];
            parametros.Add("fkUsuario", usuarioSeleccionado.idUsuario.ToString());
            cliente.UploadValues("http://192.168.1.48/transportes/listaTransportista.php", "POST", parametros);
            DisplayAlert("Éxito", "El registro se ha insertado correctamente.", "OK");
            //Navigation.PushAsync(new inicio());

        }
        catch (Exception ex)
        {

            DisplayAlert("Error", ex.Message, "CERRAR");
        }
    }



    private async void btnadjuntar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Abrir el selector de archivos
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Seleccione un archivo",
            });

            if (result == null)
            {
                // Si no se selecciona una imagen, intentar seleccionar un PDF
                result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Pdf,
                    PickerTitle = "Seleccione un archivo PDF",
                });
            }

            if (result != null)
            {
                // Obtener la ruta del archivo seleccionado
                string filePath = result.FullPath;

            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante la selección de archivos
            Console.WriteLine($"Error al seleccionar el archivo: {ex.Message}");
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.ListaConductores());
    }
}