using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace GestorTareas
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string usuario = UsernameEntry.Text?.Trim();
            string contraseña = PasswordEntry.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                await DisplayAlert("Error", "Por favor ingresa usuario y contraseña.", "OK");
                return;
            }

            var loginRequest = new
            {
                Usuario = usuario,
                Contraseña = contraseña
            };

            string apiUrl = "https://apihomer.bsite.net/api/login"; // Cambia aquí la URL a tu API local

            try
            {
                string jsonRequest = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonResponse);

                    await DisplayAlert("Éxito", $"Bienvenido {loginResponse.Nombre}!", "OK");

                    // Navegar a MainPage o página principal
                    await Navigation.PushAsync(new MainPage());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un error en el servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo conectar al servidor: {ex.Message}", "OK");
            }
        }

        private class LoginResponse
        {
            public int UsuarioId { get; set; }
            public string Nombre { get; set; }
            public string Mensaje { get; set; }
        }
    }
}
