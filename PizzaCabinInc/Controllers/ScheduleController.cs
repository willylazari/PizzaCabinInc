using Microsoft.AspNetCore.Mvc;
using PizzaCabinInc.Model;
using PizzaCabinInc.Services;
using System.ComponentModel.DataAnnotations;

namespace PizzaCabinInc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {      
        private readonly ILogger<ScheduleController> _logger;
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ILogger<ScheduleController> logger)
        {
            _logger = logger;
            _scheduleService = new ScheduleService();
        }
            
        /// <summary>
        /// Provides the team’s schedule for the given day  
        /// </summary>        
        /// <returns>Returns the meeting possibilities</returns>        
        [HttpGet]
        public ScheduleResponse GetSchedule([FromQuery] ScheduleRequest scheduleRequest)
        {
            try
            {
                return _scheduleService.GetSchedule(scheduleRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR:" + ex.Message);
                throw ex;
            }
        }
    }
}