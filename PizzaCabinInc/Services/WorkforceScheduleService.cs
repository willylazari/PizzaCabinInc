using FizzWare.NBuilder;
using System.Data;
using System;
using PizzaCabinInc.Utils;
using PizzaCabinInc.Model;

namespace PizzaCabinInc.Services
{
    public class WorkforceScheduleService
    {     
        public IEnumerable<WorkforceSchedule> GetWorkforceSchedule(WorkForceScheduleRequest workforceScheduleRequest)
        {
            Team leadersTeam = SearchTeam(workforceScheduleRequest);
            WorkforceSchedule workForceSchedule = SearchSchedule(workforceScheduleRequest, leadersTeam);

            return null;
        }

        private Team SearchTeam(WorkForceScheduleRequest workforceScheduleRequest)
        {
            return Generator.GenerateTeam(workforceScheduleRequest);
        }

        private WorkforceSchedule SearchSchedule(WorkForceScheduleRequest workforceScheduleRequest, Team team)
        {


            return null;
        }
    }
}
