using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace NeuroGenesisAI.Core.Services
{
    public class InternetStimulusService
    {
        private static readonly HttpClient _httpClient = new();

        /// <summary>
        /// Busca um estímulo aleatório da Internet.
        /// Atualmente pega um título aleatório da Wikipedia.
        /// </summary>
        public async Task<string> GetStimulusAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://en.wikipedia.org/api/rest_v1/page/random/summary");
                var json = JsonDocument.Parse(response);

                if (json.RootElement.TryGetProperty("title", out var titleElement))
                {
                    var title = titleElement.GetString();
                    return title ?? "Estímulo desconhecido";
                }
                else
                {
                    return "Estímulo não encontrado";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Erro ao buscar estímulo da internet: {ex.Message}");
                return "Falha ao buscar estímulo";
            }
        }
    }
}
