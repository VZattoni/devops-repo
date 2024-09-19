using devops_project.Interfaces;
using devops_project.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace devops_project.Services
{
    public class IbgeService : IIbgeService
    {
        HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly string baseUrlApi = "https://servicodados.ibge.gov.br/api/v2";

        public IbgeService(ILogger<IbgeService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }
        public async Task<List<NameInfo>> GetNameFrequencyAsync(string name)
        {
            string ibgeEndpointUrl = $"/censos/nomes/{name}";
            string uriString = string.Concat(baseUrlApi, ibgeEndpointUrl);
            _logger.LogInformation($"Starting request to IBGE API: {uriString}");
            var test = await _httpClient.GetAsync(uriString);
            if(test.IsSuccessStatusCode)
            {
                var content = await test.Content.ReadAsStringAsync();
                var json = JsonSerializer.Deserialize<List<NameInfo>>(content, new JsonSerializerOptions {PropertyNameCaseInsensitive = true });
                _logger.LogInformation(content);
                List<ResInfo> res = json[0].Res;
                foreach(ResInfo resInfo in res)
                {
                    _logger.LogInformation($"{resInfo.Periodo} | {resInfo.Frequencia}");
                }
                return json;
            }
            return null;
        }
    }
}
