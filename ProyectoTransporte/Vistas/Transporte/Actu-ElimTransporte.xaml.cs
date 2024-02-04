using Newtonsoft.Json;
using ProyectoTransporte.Modelos;


namespace ProyectoTransporte.Vistas.Transporte
{
    public partial class Actu_ElimTransporte : ContentPage
    {
        private Modelos.Transporte transporte;

        public Actu_ElimTransporte(Modelos.Transporte transporte)
        {
            InitializeComponent();
            this.transporte = transporte;

            LabelMatricula.Text = transporte.nMatricula;
            EntryMarca.Text = transporte.marca;
            EntryModelo.Text = transporte.modelo;
            EntryTipo.Text = transporte.tipo;
            EntryPlaca.Text = transporte.placa;
            EntryEstado.Text = transporte.estado.ToString();
            EntryObservacion.Text = transporte.observacion;

            // Ajusta los Pickers según tus necesidades y modelos
            CargarTransportistasAsync();
        }


        private async void CargarTransportistasAsync()
        {
            TransportistaPicker.ItemsSource = await ObtenerTransportistasAsync();
            TransportistaPicker.SelectedItem = transporte.fkTransportista;
        }


        private async void btnActualizarTransporte_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Obtén el objeto seleccionado de Picker (transportista)
                var transportista = (Modelos.Transportista)TransportistaPicker.SelectedItem;

                // Ajusta la URL según tu servicio web y modelo Transporte
                string url = "http://tu-servicio-web/transporte";

                HttpClient httpClient = new HttpClient();

                var transporteModificado = new Modelos.Transporte
                {
                    nMatricula = LabelMatricula.Text,
                    marca = EntryMarca.Text,
                    modelo = EntryModelo.Text,
                    tipo = EntryTipo.Text,
                    placa = EntryPlaca.Text,
                    observacion = EntryObservacion.Text,



                };

                // Intenta convertir el texto del EntryEstado a un entero
                if (int.TryParse(EntryEstado.Text, out int estadoConvertido))
                {
                    transporteModificado.estado = estadoConvertido;
                }
                else
                {
                    // Maneja el caso en el que la conversión no sea exitosa
                    DisplayAlert("ERROR", "El estado no es un número válido", "CERRAR");
                    return;
                }

                string transporteJson = JsonConvert.SerializeObject(transporteModificado);

                StringContent content = new StringContent(transporteJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Éxito", "Se ha modificado el transporte con éxito", "OK");
                    await Navigation.PopAsync(); // Regresa a la página anterior
                }
                else
                {
                    throw new Exception($"Error al modificar el transporte. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "CERRAR");
            }
        }


        private async void btnEliminarTransporte_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Ajusta la URL según tu servicio web y modelo Transporte
                string url = "http://tu-servicio-web/transporte?matricula=" + LabelMatricula.Text;

                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Éxito", "Se ha eliminado el transporte con éxito", "OK");
                    await Navigation.PopAsync(); // Regresa a la página anterior
                }
                else
                {
                    throw new Exception($"Error al eliminar el transporte. Código de estado: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "CERRAR");
            }
        }

        private async Task<List<Transportista>> ObtenerTransportistasAsync()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = "http://tu-servicio-web/transportista";

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
                throw new Exception($"Error en la solicitud al servicio web: {ex.Message}");
            }
        }
    }
}
