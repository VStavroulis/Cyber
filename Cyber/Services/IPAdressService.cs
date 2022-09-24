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
    public class IPAdressService : IIPAdressService
    {
        public async Task<IPAddressDto> GetIpInfo(string ipAddress)
        {
            HttpClientHandler _clientHandler = new HttpClientHandler();
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync(string.Format("http://localhost:5000/api/alerts/enrich?ip={0}", ipAddress)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var iPAddressDto = JsonConvert.DeserializeObject<IPAddressDto>(apiResponse);
                    
                    return iPAddressDto;
                }
            }
        }
    }
}
