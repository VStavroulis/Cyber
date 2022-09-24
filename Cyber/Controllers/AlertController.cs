using Cyber.Abstractions;
using Cyber.Abstractions.Repositories;
using Cyber.Abstractions.Services;
using Cyber.Data;
using Cyber.Dtos;
using Cyber.Enums;
using Cyber.Model;
using Cyber.Abstractions.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cyber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        private readonly IAlertRepository _alertRepository;
        private readonly IIPAddressRepository _ipAddressRepository;
        private readonly IIPAdressService _ipAdressService;
        private readonly IAlertIpsRepository _alertIpsRepository;
        private readonly IAlertIpsService _alertIpsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AlertController> _logger;

        public AlertController(IAlertService alertService, IAlertRepository alertRepository,
            IIPAddressRepository _ipAddressRepository, IAlertIpsService _alertIpsService,
            IIPAdressService ipAdressService, IAlertIpsRepository _alertIpsRepository, IUnitOfWork unitOfWork,
            ILogger<AlertController> logger)
        {
            this._alertService = alertService;
            this._alertRepository = alertRepository;
            this._ipAddressRepository = _ipAddressRepository;
            this._ipAdressService = ipAdressService;
            this._alertIpsRepository = _alertIpsRepository;
            this._alertIpsService = _alertIpsService;
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        // GET: api/<AlertController>
        [HttpGet(Name = "GetAlerts")]
        [ProducesResponseType(typeof(IEnumerable<Alert>), (int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var alert = await _alertRepository.GetAll();
                if (alert != null)
                {
                    return Ok(alert);
                }
                else
                {
                    _logger.LogWarning($"No alerts found.");
                    return NotFound(Constants.NoRecordsMsg);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, Constants.ErrorMsg);
            }
        }

        // GET api/<AlertController>/5
        [HttpGet("{id}", Name = "GetAlert")]
        [ProducesResponseType(typeof(Alert), (int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var alert = await _alertRepository.GetAlertById(id);
                if (alert == null)
                {
                    _logger.LogWarning($"Alert with id: {id}, not found.");
                    return NotFound(Constants.NoFoundMsg);
                }

                return Ok(alert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, Constants.ErrorMsg);
            }
        }

        // POST api/<AlertController>
        [HttpPost]
        [ProducesResponseType(typeof(Alert), (int)System.Net.HttpStatusCode.Created)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.Found)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Alert>> Post([FromBody] Alert alertReq)
        {
            try
            {
                var alert = await _alertRepository.GetAlertByUniqueKey(alertReq.Title, alertReq.Description, alertReq.Severity);
                if (alert != null)
                {
                    _logger.LogWarning($"Alert with id: {alert.ID}, already exists.");
                    return StatusCode((int)System.Net.HttpStatusCode.Found, "This Alert is already exist.");
                }

                _alertRepository.Insert(alertReq);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtRoute("GetAlert", new { id = alert.ID }, alert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, Constants.ErrorMsg);
            }
        }

        // PUT api/<AlertController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Alert alertReq)
        {
            try
            {
                var alert = await _alertRepository.GetAlertById(id);
                if (alert == null)
                {
                    _logger.LogWarning($"Alert with id: {id}, not found.");
                    return NotFound(Constants.NoFoundMsg);
                }

                alert.Title = alertReq.Title;
                alert.Description = alertReq.Description;
                alert.Severity = alertReq.Severity;

                _alertRepository.Update(alert);
                await _unitOfWork.SaveChangesAsync();

                return Ok(Constants.UpdateMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, Constants.ErrorMsg);
            }
        }

        // DELETE api/<AlertController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.NotFound)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var alert = await _alertRepository.GetAlertById(id);
                if (alert == null)
                {
                    _logger.LogWarning($"Alert with id: {id}, not found.");
                    return NotFound(Constants.NoFoundMsg);
                }
               
                    _alertRepository.Delete(alert);
                    await _unitOfWork.SaveChangesAsync();

                    return Ok(Constants.DeleteMsg);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, Constants.ErrorMsg);
            }
        }

        // api/ip/IPStatistics/
        [HttpGet("[action]",Name = "GetIPStatistics")]
        [ProducesResponseType(typeof(StatisticsDto), (int)System.Net.HttpStatusCode.OK)]
        public async Task<IActionResult> GetIPStatistics()
        {
            var ipAddresses = await _ipAddressRepository.GetAll();

            var blackListedCount = ipAddresses.Where(x => x.BlackListed = true).ToList().Count();
            var internalIPsCount = ipAddresses.Where(x => x.SourceType == SourceType.Internal).ToList().Count();
            var externalIPsCount = ipAddresses.Where(x => x.SourceType == SourceType.External).ToList().Count();

            var statisticsDto = new StatisticsDto
            {
                blackListed = blackListedCount,
                internalIPs = internalIPsCount,
                externalIPs = externalIPsCount
            };

            return Ok(statisticsDto);
        }

        // api/ip/APIConnect/
        [HttpGet("[action]", Name = "AlertsConsume")]
        [ProducesResponseType(typeof(StatisticsDto), (int)System.Net.HttpStatusCode.OK)]
        public async Task AlertsConsume()
        {
            //recordcount 625 alerts            
            var alertDtos = await _alertService.GetAlerts();
            foreach (var alertDto in alertDtos)
            {
                var alert = await _alertRepository.GetAlertByUniqueKey(alertDto.title, alertDto.description, alertDto.severity);
                if (alert == null)
                {
                    alert = new Alert();
                    alert.Title = alertDto.title;
                    alert.Description = alertDto.description;
                    alert.Severity = alertDto.severity;
                    _alertRepository.Insert(alert);
                    await _unitOfWork.SaveChangesAsync();
                }

                foreach (var alertDtoIp in alertDto.ips)
                {
                    var ipAddress = await _ipAddressRepository.GetIPAddressByIp(alertDtoIp);
                    if (ipAddress == null)
                    {
                        ipAddress = new IPAddress();
                        ipAddress.IP = alertDtoIp;
                        ipAddress.IPCounter = 1;
                        var ipDto = await _ipAdressService.GetIpInfo(alertDtoIp);
                        if (ipDto != null)
                        {
                            ipAddress.BlackListed = ipDto.blacklisted;
                            ipAddress.SourceType = (SourceType)ipDto.sourceType;
                        }
                        _ipAddressRepository.Insert(ipAddress);
                    }
                    else
                    {
                        ipAddress.IPCounter += 1;
                        _ipAddressRepository.Update(ipAddress);
                    }
                    await _unitOfWork.SaveChangesAsync();

                    var alertIps = await _alertIpsRepository.GetAlertIPByKey(alert.ID, ipAddress.ID);
                    if (alertIps == null)
                    {
                        alertIps = new AlertIP();
                        alertIps.AlertId = alert.ID;
                        alertIps.IPAddressId = ipAddress.ID;
                        _alertIpsRepository.Insert(alertIps);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
