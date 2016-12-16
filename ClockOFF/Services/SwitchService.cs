using ClockOFF.Helpers;
using ClockOFF.IServices;
using System;

namespace ClockOFF.Services
{
    public class SwitchService : ISwitcherService
    {
        public bool TurnComputerOffAtProgrammedTime(TimeSpan aTime)
        {
            var tDateTime = DateTime.Today.Add(aTime);

            if (tDateTime < DateTime.Now)
                tDateTime = tDateTime.AddDays(1);

            var tMinutesToShutdown = tDateTime.Subtract(DateTime.Now).Minutes;
            tMinutesToShutdown += tDateTime.Subtract(DateTime.Now).Hours * 60;

            return TurnComputerOffInXMinutes(tMinutesToShutdown);
        }

        public bool TurnComputerOffInXMinutes(int aMinutes)
        {
            int tmpSeconds = aMinutes * 60;
            return CommandResolver.SendCommand(CommandResolver.SHUTDOWN, tmpSeconds.ToString());                        
        }
    }
}