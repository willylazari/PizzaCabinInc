using FizzWare.NBuilder;
using PizzaCabinInc.Model;
using System.Data;

namespace PizzaCabinInc.Utils
{
    public class Generator
    {

        public static Team GenerateTeam(WorkForceScheduleRequest workforceScheduleRequest)
        {
            Worker workerLeader = new Worker() { ID = workforceScheduleRequest.leaderID, Name = "Worker " + workforceScheduleRequest.leaderID };
            Team team = new Team();
            team.Leader = workerLeader;

            List<int> pickedIDs = new List<int>();
            pickedIDs.Add(workforceScheduleRequest.leaderID);

            Random random = new Random();
            team.Workers = new List<Worker>();

            for (int i = 1; i <= 16; i++)
            {
                int workerID = random.Next(1, 100);
                while (pickedIDs.Contains(workerID))
                {
                    workerID = random.Next(1, 100);
                }
                pickedIDs.Add((workerID));

                Worker worker = new Worker();
                worker.ID = workerID;
                worker.Name = "Worker " + workerID;
                team.Workers.Add(worker);
            }

            int commonTimes = random.Next(5, 10);


            
            return team;
        }

        /*
        static DataTable GenerateDataTable<T>(int rows)
        {
            var datatable = new DataTable(typeof(T).Name);

            typeof(T).GetProperties().ToList().ForEach(
                x => datatable.Columns.Add(x.Name));

            Builder<T>.CreateListOfSize(rows).Build()
                .ToList().ForEach(
                    x => datatable.LoadDataRow(x.GetType().GetProperties().Select(
                        y => y.GetValue(x, null)).ToArray(), true));

            return datatable;
        }
        
        public static DataSet GenerateData()
        {
            var dataset = new DataSet();
            dataset.Tables.AddRange(new[]{
                                            GenerateDataTable<Worker>(100),
                                            GenerateDataTable<Team>(100)
                                         });

            return dataset;
        }
        */

    }
}
