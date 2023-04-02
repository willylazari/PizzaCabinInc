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
        /// <returns>Returns the meeting possibilities</returns>        
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public WorkforceScheduleResponse GetWorkforceSchedule([FromQuery] WorkForceScheduleRequest workforceScheduleRequest)
        {
            try
            {
                return _workforceScheduleService.GetWorkforceSchedule(workforceScheduleRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR:" + ex.Message);
                throw ex;
            }            
        }               
    }
}