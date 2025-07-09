using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GestorTareas.Models
{
    public partial class TareaModel 
    {
       
        public int TareaId { get; set; }

       
        public string Titulo { get; set;}

       
        public string Descripcion { get; set; }

        
        public DateTime FechaVencimiento { get; set; }

       
        public string Estado { get; set; }

       
        public int UsuarioId { get; set; }


    }
}
