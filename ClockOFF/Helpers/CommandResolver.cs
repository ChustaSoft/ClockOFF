using System;

namespace ClockOFF.Helpers
{
    public class CommandResolver
    {
        public static string SHUTDOWN = "shutdown -s -t";

        public static bool SendCommand(string aCommand, string aParamether) {
            try
            {
                System.Diagnostics.Process.Start("cmd", "/C " + aCommand + " " + aParamether);
                return true;
            }
            catch (Exception) {
                return false;
            }            
        }       
         
    }
}
