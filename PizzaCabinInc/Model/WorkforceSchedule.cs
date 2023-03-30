namespace PizzaCabinInc.Model
{
    public class WorkforceSchedule
    {
        public int TeamID { get; set; }

        public List<DateTime> MeetingTimes { get; set; }
    }
}