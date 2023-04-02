using FizzWare.NBuilder;
using PizzaCabinInc.Model;
using PizzaCabinInc.Services;
using System;
using System.Data;

namespace PizzaCabinInc.Utils
{
    public class Generator
    {

        public static Team GenerateTeam(WorkForceScheduleRequest workforceScheduleRequest)
        {
            // Parameters
            int maxTeamWorkers = WorkforceScheduleService.maxTeamWorkers;
            int meetingDurationMinutes = WorkforceScheduleService.meetingDurationMinutes;
            int[] minutesAllowed = WorkforceScheduleService.minutesAllowed;
            DateTime companyOpenTime = new DateTime(workforceScheduleRequest.date.Year, workforceScheduleRequest.date.Month, workforceScheduleRequest.date.Day, WorkforceScheduleService.companyOpenHour, 0, 0);
            DateTime companyCloseTime = new DateTime(workforceScheduleRequest.date.Year, workforceScheduleRequest.date.Month, workforceScheduleRequest.date.Day, WorkforceScheduleService.companyCloseHour, 00, 0);
            
            Random random = new Random();

            /*
            Dictionary<DateTime, DateTime> pickedTimesToMeeting = new Dictionary<DateTime, DateTime>();
            int commonTimesQuantity = random.Next(5, 10);
            for (int i = 0; i < commonTimesQuantity; i++)
            {
                DateTime meetingTime = RandomDateTime(companyOpenTime, companyCloseTime, minutesAllowed);
                while (pickedTimesToMeeting.ContainsKey(meetingTime))
                {
                    meetingTime = RandomDateTime(companyOpenTime, companyCloseTime, minutesAllowed);
                }
                pickedTimesToMeeting.Add(meetingTime, meetingTime.AddMinutes(15));
            }
            var sortedPickedTimesToMeeting = from entry in pickedTimesToMeeting orderby entry.Key ascending select entry;
            */
                
            Worker workerLeader = new Worker();
            workerLeader.ID = workforceScheduleRequest.leaderID;
            workerLeader.Name = "Worker " + workforceScheduleRequest.leaderID;
            workerLeader.StartTime = RandomDateTime(companyOpenTime, companyOpenTime.AddHours(7), minutesAllowed);
            workerLeader.BreakTime = RandomDateTime(workerLeader.StartTime.AddHours(2), workerLeader.StartTime.AddHours(3), minutesAllowed);
            workerLeader.LunchTime = RandomDateTime(workerLeader.BreakTime.AddHours(2), workerLeader.BreakTime.AddHours(3), minutesAllowed);
            workerLeader.EndTime = RandomDateTime(workerLeader.LunchTime.AddHours(2), workerLeader.LunchTime.AddHours(3), minutesAllowed);

            Team team = new Team();
            team.ID = random.Next(1, 1000);
            team.Leader = workerLeader;

            List<int> pickedIDs = new List<int>();
            pickedIDs.Add(workforceScheduleRequest.leaderID);
                        
            team.Workers = new List<Worker>();
            int teamWorkersNumber = random.Next(workforceScheduleRequest.quantity, maxTeamWorkers);
              
            for (int i = 0; i < teamWorkersNumber; i++)
            {
                int workerID = random.Next(1, 1000);
                while (pickedIDs.Contains(workerID))
                {
                    workerID = random.Next(1, 1000);
                }
                pickedIDs.Add((workerID));

                Worker worker = new Worker();
                worker.ID = workerID;
                worker.Name = "Worker " + workerID;
                worker.StartTime = RandomDateTime(companyOpenTime, companyOpenTime.AddHours(7), minutesAllowed);
                worker.BreakTime = RandomDateTime(worker.StartTime.AddHours(2), worker.StartTime.AddHours(3), minutesAllowed);
                worker.LunchTime = RandomDateTime(worker.BreakTime.AddHours(2), worker.BreakTime.AddHours(3), minutesAllowed);
                worker.EndTime = RandomDateTime(worker.LunchTime.AddHours(2), worker.LunchTime.AddHours(3), minutesAllowed);
                team.Workers.Add(worker);
            }

            WorkforceScheduleService _workforceScheduleService = new WorkforceScheduleService();
            WorkforceScheduleResponse workForceSchedule = _workforceScheduleService.SearchSchedule(workforceScheduleRequest, team);
            if (workForceSchedule.MeetingTimes.Count < 0)
            {
                //...
            }

            return team;
        }
               
        public static DateTime RandomDateTime(DateTime initialDateTime, DateTime endDateTime, int[] minutesAllowed)
        {
            DateTime start = initialDateTime;
            Random gen = new Random();

            int randomHour = gen.Next(0, endDateTime.Hour-initialDateTime.Hour);
                        
            int rnd = gen.Next(0, minutesAllowed.Length);
            int randomMinutes = minutesAllowed[rnd];

            return start.AddHours(randomHour).AddMinutes(randomMinutes);
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
                                            GenerateDataTable<Worker>(100)                                            
                                         });

            return dataset;
        }
        */

    }
}
