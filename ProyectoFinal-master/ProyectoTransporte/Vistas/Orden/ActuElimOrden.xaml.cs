using Newtonsoft.Json;
using ProyectoTransporte.Modelos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProyectoTransporte.Vistas.Orden
{
    public partial class ActuElimOrden : ContentPage
    {
        private Ordenes orden;

        public ActuElimOrden(Ordenes orden)
        {
            InitializeComponent();
            this.orden = orden;
            // Asignar los valores de la orden a los controles
            AssignOrderValues();
        }

        private void AssignOrderValues()
        {
            EntryNOrden.Text = orden.idOrden.ToString();
            EntryObservacion.Text = orden.observacion;
            EntryDestino.Text = orden.destino;
            DatePickerFecha.Date = orden.fecha;
            // Asignar otros valores seg�n la estructura del modelo
            FacturaPicker.SelectedItem = orden.fkFactura; // Aseg�rate de tener la propiedad correspondiente en el modelo
            TransportistaPicker.SelectedItem = orden.fkTransportista; // Aseg�rate de tener la propiedad correspondiente en el modelo
            UsuarioPicker.SelectedItem = orden.fkUsuario; // Aseg�rate de tener la propiedad correspondiente en el modelo
            TransportePicker.SelectedItem = orden.fkTransporte; // Aseg�rate de tener la propiedad correspondiente en el modelo
        }

        private async Task UpdateOrderAsync()
        {
            try
            {
                // Obtener los valores de los controles
                string idOrden = EntryNOrden.Text;
                string numeroOrden = EntryNOrden.Text;
                string observacion = EntryObservacion.Text;
                string destino = EntryDestino.Text;
                DateTime fecha = DatePickerFecha.Date;
                // Obtener otros valores seg�n la estructura del modelo
                string factura = FacturaPicker.SelectedItem?.ToString(); // Aseg�rate de tener la propiedad correspondiente en el modelo
                string transportista = TransportistaPicker.SelectedItem?.ToString(); // Aseg�rate de tener la propiedad correspondiente en el modelo
                string usuario = UsuarioPicker.SelectedItem?.ToString(); // Aseg�rate de tener la propiedad correspondiente en el modelo
                string transporte = TransportePicker.SelectedItem?.ToString(); // Aseg�rate de tener la propiedad correspondiente en el modelo

                // Construir la URL con los par�metros para la actualizaci�n
                string url = $"http://192.168.1.48/transportes/listaTransportista.php?idOrden={idOrden}&numeroOrden={numeroOrden}&observacion={observacion}&destino={destino}&fecha={fecha}&factura={factura}&transportista={transportista}&usuario={usuario}&transporte={transporte}";

                // Realizar la solicitud PUT al servicio web
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.PutAsync(url, null);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        DisplayAlert("�xito", "Se ha actualizado la orden con �xito", "OK");
                        // Redirigir a la lista de �rdenes u otra p�gina seg�n tu flujo de navegaci�n
                        Navigation.PushAsync(new listarOrdenes());
                    }
                    else
                    {
                        // Manejar el caso en que la solicitud no fue exitosa
                        throw new Exception($"Error al actualizar la orden. C�digo de estado: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Error al actualizar la orden: {ex.Message}", "OK");
            }
        }

        private async Task DeleteOrderAsync()
        {
            try
            {
                // Obtener el ID de la orden
                string idOrden = EntryNOrden.Text;

                // Construir la URL con los par�metros para la eliminaci�n
                string url = $"http://192.168.1.48/transportes/listaTransportista.php?idOrden={idOrden}";

                // Realizar la solicitud DELETE al servicio web
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(url);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        DisplayAlert("�xito", "Se ha eliminado la orden con �xito", "OK");
                        // Redirigir a la lista de �rdenes u otra p�gina seg�n tu flujo de navegaci�n
                        Navigation.PushAsync(new listarOrdenes());
                    }
                    else
                    {
                        // Manejar el caso en que la solicitud no fue exitosa
                        throw new Exception($"Error al eliminar la orden. C�digo de estado: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Error al eliminar la orden: {ex.Message}", "OK");
            }
        }

        private async void btnActualizarOrden_Clicked(object sender, EventArgs e)
        {
            await UpdateOrderAsync();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            await DeleteOrderAsync();
        }
    }
}
