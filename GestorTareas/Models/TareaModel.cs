using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GestorTareas.Models
{
    public partial class TareaModel : ObservableObject
    {
        [ObservableProperty]
        private int tareaId;

        [ObservableProperty]
        private string titulo;

        [ObservableProperty]
        private string descripcion;

        [ObservableProperty]
        private DateTime fechaCreacion;

        [ObservableProperty]
        private DateTime fechaVencimiento;

        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private int usuarioId;


    }
}
