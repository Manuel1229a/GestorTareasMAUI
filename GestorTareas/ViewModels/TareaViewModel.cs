using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorTareas.Models;

namespace GestorTareas.ViewModels
{
    public partial class TareaViewModel : ObservableObject
    {
        public ObservableCollection<TareaModel> ListaTareas { get; set; } = new();

        [ObservableProperty]
        private TareaModel nuevaTarea = new TareaModel
        {
            FechaCreacion = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddDays(1),
            Estado = "Pendiente"
        };

        [ObservableProperty]
        private string usuarioIdTexto;

        private TareaModel tareaEnEdicion = null;

        [RelayCommand]
        public void AgregarTarea()
        {
            int.TryParse(UsuarioIdTexto, out int usuarioIdConvertido);

            if (tareaEnEdicion == null)
            {
                // Crear nueva tarea
                var tarea = new TareaModel
                {
                    TareaId = ListaTareas.Count + 1,
                    Titulo = NuevaTarea.Titulo,
                    Descripcion = NuevaTarea.Descripcion,
                    FechaCreacion = DateTime.Now,
                    FechaVencimiento = NuevaTarea.FechaVencimiento,
                    Estado = NuevaTarea.Estado,
                    UsuarioId = usuarioIdConvertido
                };

                ListaTareas.Add(tarea);
            }
            else
            {
                // Editar tarea existente
                tareaEnEdicion.Titulo = NuevaTarea.Titulo;
                tareaEnEdicion.Descripcion = NuevaTarea.Descripcion;
                tareaEnEdicion.FechaVencimiento = NuevaTarea.FechaVencimiento;
                tareaEnEdicion.Estado = NuevaTarea.Estado;
                tareaEnEdicion.UsuarioId = usuarioIdConvertido;
                tareaEnEdicion = null;
            }

            ReiniciarFormulario();
        }

        [RelayCommand]
        public void EliminarTarea(TareaModel tarea)
        {
            if (ListaTareas.Contains(tarea))
            {
                ListaTareas.Remove(tarea);
            }
        }

        [RelayCommand]
        public void EditarTarea(TareaModel tarea)
        {
            NuevaTarea = new TareaModel
            {
                TareaId = tarea.TareaId,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaCreacion = tarea.FechaCreacion,
                FechaVencimiento = tarea.FechaVencimiento,
                Estado = tarea.Estado,
                UsuarioId = tarea.UsuarioId
            };

            UsuarioIdTexto = tarea.UsuarioId.ToString();
            tareaEnEdicion = tarea;
        }

        private void ReiniciarFormulario()
        {
            NuevaTarea = new TareaModel
            {
                FechaCreacion = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(1),
                Estado = "Pendiente"
            };
            UsuarioIdTexto = string.Empty;
        }
    }
}
