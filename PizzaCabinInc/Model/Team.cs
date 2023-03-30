namespace PizzaCabinInc.Model
{
    public class Team
    {
        public int ID { get; set; }

        public Worker Leader { get; set; }

        public List<Worker> Workers { get; set; }
    }
}