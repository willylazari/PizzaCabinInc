using Newtonsoft.Json.Linq;
using PizzaCabinInc.Model;

namespace PizzaCabinInc.Proxy
{
    public class ScheduleProxy
    {
        public TeamSchedule SearchTeamSchedule(ScheduleRequest scheduleRequest)
        {
            // Here we should be connecting to an API to get the data, if was available
            TeamSchedule teamSchedule = Newtonsoft.Json.JsonConvert.DeserializeObject<TeamSchedule>(File.ReadAllText(@".\Proxy\SampleDataSource.json"));
            foreach (Schedule schedule in teamSchedule.ScheduleResult.Schedules)
            {
                foreach (Projection projection in schedule.Projection)
                {
                    projection.Start = new DateTime(scheduleRequest.date.Year, scheduleRequest.date.Month, scheduleRequest.date.Day, projection.Start.Hour, projection.Start.Minute, projection.Start.Second);
                }
            }

            return teamSchedule;
        }

    }
}
