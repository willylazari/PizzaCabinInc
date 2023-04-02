namespace PizzaCabinInc.Model
{
    public class WorkforceScheduleResponse
    {
        public int TeamID { get; set; }

        public List<DateTime> MeetingTimes { get; set; }
    }
}