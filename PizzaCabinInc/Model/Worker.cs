namespace PizzaCabinInc.Model
{
    public class Worker
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime BreakTime { get; set; }
        public DateTime LunchTime { get; set; }
        public DateTime EndTime { get; set; }


    }
}