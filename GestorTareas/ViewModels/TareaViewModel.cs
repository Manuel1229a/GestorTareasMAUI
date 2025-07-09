using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorTareas.Models;
using GestorTareas.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestorTareas.ViewModels
{
    public class TareaViewModel : ObservableObject
    {
        private string _usuarioIdTexto;

        public string UsuarioIdTexto
        {
            get => _usuarioIdTexto;
            set
            {
                if (_usuarioIdTexto != value)
                {
                    _usuarioIdTexto = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly TareaService _tareaService;

        public ObservableCollection<TareaModel> ListaTareas { get; set; } = new();
        public TareaModel NuevaTarea { get; set; } = new();

        public TareaViewModel()
        {
            _tareaService = new TareaService();
            _ = CargarTareasAsync();
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

        public Command AgregarTareaCommand => new(async () =>
        {
            var exito = await _tareaService.AgregarTareaAsync(NuevaTarea);
            if (exito)
            {
                await CargarTareasAsync();
                NuevaTarea = new TareaModel();
                OnPropertyChanged(nameof(NuevaTarea));
            }
        });

        public Command<TareaModel> EliminarTareaCommand => new(async (tarea) =>
        {
            var exito = await _tareaService.EliminarTareaAsync(tarea.TareaId);
            if (exito)
            {
                await CargarTareasAsync();
            }
        });

        public Command<TareaModel> EditarTareaCommand => new(async (tarea) =>
        {
            var exito = await _tareaService.ActualizarTareaAsync(tarea);
            if (exito)
            {
                await CargarTareasAsync();
            }
        });
    }
}