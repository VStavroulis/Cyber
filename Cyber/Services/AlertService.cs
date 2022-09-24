using Cyber.Abstractions.Services;
using Cyber.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cyber.Services
{

    public class AlertService : IAlertService
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        public async Task<List<AlertDto>> GetAlerts()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/alerts/a?page_size=3&page_no=2"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var alertList = JsonConvert.DeserializeObject<List<AlertDto>>(apiResponse);

                    return alertList;
                }
            }
        }
    }
}
