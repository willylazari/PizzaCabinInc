using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PizzaCabinInc.Controllers;
using PizzaCabinInc.Model;

namespace PizzaCabinIncTests
{
    public class ScheduleControllerTest
    {
        private readonly ScheduleController _controller;

        public ScheduleControllerTest()
        {
            _controller = new ScheduleController(NullLogger<ScheduleController>.Instance);
        }

        [Fact]
        public void GetSchedule_WhenCalled_ReturnsOkResult()
        {
            ScheduleRequest request = new ScheduleRequest();
            request.date = DateTime.Now;
            request.quantity = 5;
            //request.leaderID = 99;

            // Act
            var result = _controller.GetSchedule(request);

            // Assert
            Assert.IsType<ScheduleResponse>(result as ScheduleResponse);
        }
    }
}