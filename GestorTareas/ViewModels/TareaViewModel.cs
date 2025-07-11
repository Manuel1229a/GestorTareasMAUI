using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorTareas.Models;
using GestorTareas.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GestorTareas.ViewModels
{
    public class TareaViewModel : ObservableObject
    {
        private string _usuarioIdTexto;
        public string UsuarioIdTexto
        {
            get => _usuarioIdTexto;
            set => SetProperty(ref _usuarioIdTexto, value);
        }

        private readonly TareaService _tareaService;

        public ObservableCollection<TareaModel> ListaTareas { get; } = new();
        public ObservableCollection<UsuarioModel> ListaUsuarios { get; } = new();

        private TareaModel _nuevaTarea = new() { FechaVencimiento = DateTime.Today };
        public TareaModel NuevaTarea
        {
            get => _nuevaTarea;
            set => SetProperty(ref _nuevaTarea, value);
        }

        private UsuarioModel _usuarioSeleccionado;
        public UsuarioModel UsuarioSeleccionado
        {
            get => _usuarioSeleccionado;
            set
            {
                if (SetProperty(ref _usuarioSeleccionado, value) && value != null)
                {
                    NuevaTarea.UsuarioId = value.UsuarioId;
                }
            }
        }

        // Comandos asíncronos
        public IAsyncRelayCommand AgregarTareaCommand { get; }
        public IAsyncRelayCommand<TareaModel> EliminarTareaCommand { get; }
        public IAsyncRelayCommand<TareaModel> EditarTareaCommand { get; }

        public TareaViewModel()
        {
            _tareaService = new TareaService();

            AgregarTareaCommand = new AsyncRelayCommand(AgregarTareaAsync);
            EliminarTareaCommand = new AsyncRelayCommand<TareaModel>(EliminarTareaAsync);
            EditarTareaCommand = new AsyncRelayCommand<TareaModel>(EditarTareaAsync);

            _ = CargarTareasAsync();
            _ = CargarUsuariosAsync();
        }

        public async Task CargarTareasAsync()
        {
            var tareas = await _tareaService.ObtenerTareasAsync();
            ListaTareas.Clear();
            foreach (var tarea in tareas)
            {
                ListaTareas.Add(tarea);
            }
        }

        public async Task CargarUsuariosAsync()
        {
            ListaUsuarios.Clear();

            // Simulando carga de usuarios (reemplaza si traes de API)
            ListaUsuarios.Add(new UsuarioModel { UsuarioId = 1, Nombre = "Carlos" });
            ListaUsuarios.Add(new UsuarioModel { UsuarioId = 2, Nombre = "Ana" });
            ListaUsuarios.Add(new UsuarioModel { UsuarioId = 3, Nombre = "Luis" });

            await Task.CompletedTask;
        }

        private async Task AgregarTareaAsync()
        {
            if (string.IsNullOrWhiteSpace(NuevaTarea.Titulo) ||
                NuevaTarea.FechaVencimiento <= DateTime.Today ||
                NuevaTarea.UsuarioId == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor completa todos los campos correctamente.", "OK");
                return;
            }

            var exito = await _tareaService.AgregarTareaAsync(NuevaTarea);
            if (exito)
            {
                await CargarTareasAsync();
                NuevaTarea = new TareaModel { FechaVencimiento = DateTime.Today };
            }
        }

        private async Task EliminarTareaAsync(TareaModel tarea)
        {
            var exito = await _tareaService.EliminarTareaAsync(tarea.TareaId);
            if (exito)
            {
                await CargarTareasAsync();
            }
        }

        private async Task EditarTareaAsync(TareaModel tarea)
        {
            var exito = await _tareaService.ActualizarTareaAsync(tarea);
            if (exito)
            {
                await CargarTareasAsync();
            }
        }
    }
}
