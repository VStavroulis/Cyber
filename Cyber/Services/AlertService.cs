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
                int pageSize = 100;
                int pageNo = 1;
                List<AlertDto> alertDtos = new List<AlertDto>();
                using (var response = await httpClient.GetAsync(string.Format("https://localhost:5001/api/alerts/a?page_size={0}&page_no={1}", pageSize, pageNo)))
                {
                    int currentPage = 1;
                    
                    var apiHeaders =  response.Headers.GetValues("X-Pagination").ToList().FirstOrDefault();
                    var xPaginationDto = JsonConvert.DeserializeObject<XPaginationDto>(apiHeaders);
                    
                    var pageCount = xPaginationDto.total_record_count;

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    var alertList = JsonConvert.DeserializeObject<List<AlertDto>>(apiResponse);
                    
                    alertDtos.AddRange(alertList);
                    while (pageNo <= xPaginationDto.page_count)
                    {
                        pageNo++;
                        using (var nextResponse = await httpClient.GetAsync(string.Format("https://localhost:5001/api/alerts/a?page_size={0}&page_no={1}", pageSize, pageNo)))
                        {
                            string nextApiResponse = await nextResponse.Content.ReadAsStringAsync();
                            var nextAlertList = JsonConvert.DeserializeObject<List<AlertDto>>(nextApiResponse);
                            alertDtos.AddRange(nextAlertList);
                        }
                    }
                    return alertDtos;
                }

                
            }
        }
    }
}
