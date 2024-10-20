// Services/ApiService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using BattleShip.App.Components.Menu;
using BattleShip.Models;

namespace BattleShip.App.Services
{

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GameData> StartGameAsync(GameStart gameStart)
        {
            var response = await _httpClient.PostAsJsonAsync("api/start", gameStart);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GameData>();
        }

        public async Task<GameData> SendGameDataAsync(GameData gameData)
        {
            var response = await _httpClient.PostAsJsonAsync("api/gameData", gameData);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GameData>();
        }
    }
}
