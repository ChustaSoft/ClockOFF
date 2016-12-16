using System;

namespace ClockOFF.IServices
{
    public interface ISwitcherService
    {
        bool TurnComputerOffInXMinutes(int aMinutes);
        bool TurnComputerOffAtProgrammedTime(TimeSpan aTime);
    }
}
