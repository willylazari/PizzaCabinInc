using Microsoft.AspNetCore.Mvc;
using PizzaCabinInc.Model;
using PizzaCabinInc.Services;
using System.ComponentModel.DataAnnotations;

namespace PizzaCabinInc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkforceScheduleController : ControllerBase
    {      
        private readonly ILogger<WorkforceScheduleController> _logger;
        private readonly WorkforceScheduleService _workforceScheduleService;

        public WorkforceScheduleController(ILogger<WorkforceScheduleController> logger)
        {
            _logger = logger;
            _workforceScheduleService = new WorkforceScheduleService();
        }

        /// <summary>
        /// Provides the team’s schedule for the given day  
        /// </summary>
        /// <param name="date">Meeting day</param>
        /// <param name="quantity">Number of workers</param>
        /// <returns></returns>        
        [HttpGet]
        public IEnumerable<WorkforceSchedule> GetWorkforceSchedule([FromQuery] WorkForceScheduleRequest workforceScheduleRequest)
        {
            return _workforceScheduleService.GetWorkforceSchedule(workforceScheduleRequest);
        }
    }
}