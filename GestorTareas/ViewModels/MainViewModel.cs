using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace GestorTareas.ViewModels
{
   public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int count;

        [RelayCommand]
        public void Sumar()
        {
            Count++;
        }
    }
}
