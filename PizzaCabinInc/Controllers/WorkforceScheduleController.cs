using Microsoft.AspNetCore.Mvc;

namespace PizzaCabinInc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkforceScheduleController : ControllerBase
    {      
        private readonly ILogger<WorkforceScheduleController> _logger;

        public WorkforceScheduleController(ILogger<WorkforceScheduleController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWorkforceSchedule")]
        [HttpGet]
        public IEnumerable<WorkforceSchedule> GetWorkforceSchedule(DateTime date, int quantity)
        {
            return null;
        }
    }
}