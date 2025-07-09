using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GestorTareas.Models;

namespace GestorTareas.Services
{
    public class TareaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://apihomer.bsite.net/api/tareas";

        public TareaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TareaModel>> ObtenerTareasAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TareaModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return new List<TareaModel>();
        }

        public async Task<bool> AgregarTareaAsync(TareaModel tarea)
        {
            var json = JsonSerializer.Serialize(tarea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarTareaAsync(TareaModel tarea)
        {
            var json = JsonSerializer.Serialize(tarea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{tarea.TareaId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarTareaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
