using GestorTareas.ViewModels;
using CommunityToolkit.Mvvm.Input;


namespace GestorTareas
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnMostrarFormularioClicked(object sender, EventArgs e)
        {
            FormularioTarea.IsVisible = true;
        }

        private void OnCerrarFormularioClicked(object sender, EventArgs e)
        {
            FormularioTarea.IsVisible = false;
        }

        private async void OnAgregarYCerrar(object sender, EventArgs e)
        {
            if (BindingContext is TareaViewModel vm)
            {
                await vm.AgregarTareaCommand.ExecuteAsync(null);
                FormularioTarea.IsVisible = false;
            }
        }


    }
}
