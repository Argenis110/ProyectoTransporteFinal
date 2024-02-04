using System;
using System.Net.Http;
using Newtonsoft.Json;
using ProyectoTransporte.Modelos;

namespace ProyectoTransporte.Vistas
{
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void BtnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // URL del servicio web
                string url = "http://192.168.1.48/transportes/usuario.php";

                // Credenciales ingresadas por el usuario
                string usuario = txtUsuario.Text;
                string contrasenia = txtContraseña.Text;

                // Realizar la solicitud GET para obtener la lista de usuarios
                using (HttpClient cliente = new HttpClient())
                {
                    HttpResponseMessage response = await cliente.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como cadena JSON
                        string contenido = await response.Content.ReadAsStringAsync();

                        // Deserializar la cadena JSON en una lista de usuarios
                        var listaUsuarios = JsonConvert.DeserializeObject<Usuario[]>(contenido);

                        // Autenticar al usuario
                        Usuario usuarioAutenticado = Array.Find(listaUsuarios, u => u.correo == usuario && u.contrasenia == contrasenia);

                        if (usuarioAutenticado != null)
                        {
                            // Usuario autenticado, puedes navegar a la siguiente página
                            await DisplayAlert("Éxito", "Inicio de sesión exitoso", "OK");
                            txtUsuario.Text = "";
                            txtContraseña.Text = "";
                            Navigation.PushAsync(new Vistas.Pagina1(usuario));
                        }
                        else
                        {
                            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error al obtener datos del servidor", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
