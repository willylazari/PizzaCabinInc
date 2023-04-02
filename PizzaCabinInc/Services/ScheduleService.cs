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
    public class ScheduleService
    {
        // Parameters
        // Must been added to the appsettings file
        public static int maxTeamWorkers = 16;
        public static int meetingDurationMinutes = 15;
        public static int[] minutesAllowed = new int[] { 00, 15, 30, 45 };

        public ScheduleResponse GetSchedule(ScheduleRequest scheduleRequest)
        {
            TeamSchedule leadersTeam = SearchTeamSchedule(scheduleRequest);
            ScheduleResponse schedule = SearchMeetingSchedule(scheduleRequest, leadersTeam);

            return schedule;
        }

        private TeamSchedule SearchTeamSchedule(ScheduleRequest scheduleRequest)
        {
            ScheduleProxy scheduleProxy = new ScheduleProxy();
            return scheduleProxy.SearchTeamSchedule(scheduleRequest);
        }

        public ScheduleResponse SearchMeetingSchedule(ScheduleRequest scheduleRequest, TeamSchedule teamSchedule)
        {
            ScheduleResponse result = new ScheduleResponse();
            result.MeetingTimes = new List<DateTime>();

            var timesAvailable = teamSchedule.ScheduleResult.Schedules.SelectMany(s =>
                                s.Projection.Select(p => new
                                {
                                    //PersonId = s.PersonId,
                                    //Name = s.Name,                                    
                                    //Description = p.Description,                                    
                                    //Minutes = p.minutes,
                                    Start = p.Start,                                    
                                    Available = IsTimeAvailable(p),
                                    IsFullDayAbsence = s.IsFullDayAbsence
                                })
                          ).Where(x => x.IsFullDayAbsence == false && x.Available == true).OrderBy(x => x.Start);
           
            for (int i = timesAvailable.First().Start.Hour; i < timesAvailable.Last().Start.Hour; i+=1)
            {
                for (int j = 0; j < 50; j+= meetingDurationMinutes)
                {
                    DateTime meetingInvite = new DateTime(scheduleRequest.date.Year, scheduleRequest.date.Month, scheduleRequest.date.Day, i, j, 0);
                    
                    if (CanSchedule(scheduleRequest, teamSchedule, meetingInvite))
                    {
                        result.MeetingTimes.Add(meetingInvite);
                    }
                }
            }
            
            return result;
        }
        private bool IsTimeAvailable(Projection time)
        {
            switch (time.Color)
            {
                case "#80FF80":
                    return true;
                    break;
                case "#1E90FF":
                    return true;
                    break;
                case "#FFC080":
                    return false;
                    break;
                case "#FFFF00":
                    return false;
                    break;
                case "#FF00000":
                    return false;
                    break;
                default:
                    return false;
                    break;
            }
        }

        private bool CanSchedule(ScheduleRequest scheduleRequest, TeamSchedule teamSchedule, DateTime meetingInvite)
        {            
            int countAvailability = 0;            
            foreach (Schedule schedule in teamSchedule.ScheduleResult.Schedules)
            {                
                if (HasAvailability(schedule, meetingInvite))
                {
                    countAvailability++;
                }                              
            }
           
            if (scheduleRequest.quantity > countAvailability)
            {
                return false;
            }
            
            return true;            
        }

        private bool HasAvailability(Schedule schedule, DateTime meetingInvite)
        {
            if (!schedule.IsFullDayAbsence)
            {
                foreach (Projection projection in schedule.Projection)
                {
                    if (IsTimeAvailable(projection))
                    {
                        if (projection.Start <= meetingInvite && projection.Start.AddMinutes(projection.minutes) >= meetingInvite.AddMinutes(meetingDurationMinutes))
                        {                            
                            return true;                                
                        }
                    }
                }
            }

            return false;
        }

    }
}
