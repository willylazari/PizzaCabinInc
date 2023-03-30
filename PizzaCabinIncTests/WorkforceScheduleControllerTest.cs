using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PizzaCabinInc.Controllers;
using PizzaCabinInc.Model;

namespace PizzaCabinIncTests
{
    public class WorkforceScheduleControllerTest
    {
        private readonly WorkforceScheduleController _controller;

        public WorkforceScheduleControllerTest()
        {
            _controller = new WorkforceScheduleController(NullLogger<WorkforceScheduleController>.Instance);
        }

        [Fact]
        public void GetWorkforceSchedule_WhenCalled_ReturnsOkResult()
        {
            WorkForceScheduleRequest request = new WorkForceScheduleRequest();
            request.date = DateTime.Now;
            request.quantity = 5;
            request.leaderID = 99;

            // Act
            var result = _controller.GetWorkforceSchedule(request);

            // Assert
            Assert.IsType<WorkforceSchedule>(result as WorkforceSchedule);
        }
    }
}