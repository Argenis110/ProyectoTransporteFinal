
namespace ProyectoTransporte.Vistas;

public partial class Pagina1 : ContentPage
{
    
    public string NombreUsuarioConectado { get; set; }
    public Pagina1(string nombreUsuario)
    {
        InitializeComponent();
        NombreUsuarioConectado = nombreUsuario;
        TituloLabel.Text = $" !Bienvenido {NombreUsuarioConectado}¡";
        
    }

    
    private async void Button_Conductor_Clicked(object sender, EventArgs e)
    {


        await Navigation.PushAsync(new Vistas.Pagina2());

    }

    private async void Button_Transporte_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Vistas.IngresarTransporte());
     }

    private async void btn_Ordenes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Vistas.IngresarOrden());
    }

    private async void btn_Facturas_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Vistas.IngresarFactura());
    }
}