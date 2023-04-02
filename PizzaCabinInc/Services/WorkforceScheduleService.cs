using FizzWare.NBuilder;
using System.Data;
using System;
using PizzaCabinInc.Utils;
using PizzaCabinInc.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PizzaCabinInc.Proxy;

namespace PizzaCabinInc.Services
{
    public class WorkforceScheduleService
    {
        // Parameters
        // Must been added to the appsettings file
        public static int maxTeamWorkers = 16;
        public static int meetingDurationMinutes = 15;
        public static int companyOpenHour = 8;
        public static int companyCloseHour = 23;
        public static int breakDuration = 20;
        public static int lunchDuration = 60;
        public static int[] minutesAllowed = new int[] { 00, 15, 30, 45 };

        public WorkforceScheduleResponse GetWorkforceSchedule(WorkForceScheduleRequest workforceScheduleRequest)
        {
            Team leadersTeam = SearchTeam(workforceScheduleRequest);
            WorkforceScheduleResponse workForceSchedule = SearchSchedule(workforceScheduleRequest, leadersTeam);

            return workForceSchedule;
        }

        private Team SearchTeam(WorkForceScheduleRequest workforceScheduleRequest)
        {
            // Method to generate a random team with random workers and times simulating a database search
            return Generator.GenerateTeam(workforceScheduleRequest);
        }

        public WorkforceScheduleResponse SearchSchedule(WorkForceScheduleRequest workforceScheduleRequest, Team team)
        {
            WorkforceScheduleResponse result = new WorkforceScheduleResponse();            
            result.TeamID = team.ID;
            result.MeetingTimes = new List<DateTime>();

            for (int i = companyOpenHour; i < companyCloseHour; i+=1)
            {
                for (int j = 0; j < 50; j+= meetingDurationMinutes)
                {
                    DateTime meetingInvite = new DateTime(workforceScheduleRequest.date.Year, workforceScheduleRequest.date.Month, workforceScheduleRequest.date.Day, i, j, 0);
                    if (CanSchedule(workforceScheduleRequest, team, meetingInvite))
                    {
                        result.MeetingTimes.Add(meetingInvite);
                    }
                }
            }
            
            return result;
        }

        private bool CanSchedule(WorkForceScheduleRequest workforceScheduleRequest, Team team, DateTime meetingInvite)
        {
            int countAvailability = 0;
            if (HasAvailability(team.Leader, meetingInvite))
            {
                foreach (Worker worker in team.Workers)
                {
                    if (HasAvailability(worker,meetingInvite))
                    {
                        countAvailability++;
                    }
                }
            }
            else
            {
                return false;
            }

            if (workforceScheduleRequest.quantity > countAvailability)
            {
                return false;
            }

            return true;
        }

        private bool HasAvailability(Worker worker, DateTime meetingInvite)
        {
            if (worker.StartTime <= meetingInvite && worker.EndTime >= meetingInvite.AddMinutes(meetingDurationMinutes))
            {
                if (worker.BreakTime >= meetingInvite.AddMinutes(meetingDurationMinutes) || worker.BreakTime.AddMinutes(breakDuration) <= meetingInvite)
                {
                    if (worker.LunchTime >= meetingInvite.AddMinutes(meetingDurationMinutes) || worker.LunchTime.AddMinutes(lunchDuration) <= meetingInvite)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
