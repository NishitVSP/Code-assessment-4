using System;
using System.Threading;

namespace Alarm_L12  
{
    // Publisher class
    public class Alarm
    {
        public event EventHandler RaiseAlarm;

        public void Start(DateTime targetTime)
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                if (now >= targetTime)  // More robust time comparison
                {
                    OnRaiseAlarm(EventArgs.Empty);
                    break;
                }
                Console.WriteLine(" Tick ..");
                Thread.Sleep(1000);
            }
        }

        protected virtual void OnRaiseAlarm(EventArgs e)
        {
            RaiseAlarm?.Invoke(this, e);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter time (HH:MM:SS): ");
            DateTime targetTime = GetValidatedTime();

            Alarm alarm = new Alarm();
            alarm.RaiseAlarm += RingAlarm; // Subscribe

            Console.WriteLine("Waiting for alarm...");
            alarm.Start(targetTime);
        }

        static DateTime GetValidatedTime()
        {
            DateTime targetTime;
            // validate input
            while (true)
            {
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out targetTime))
                {
                    // Add future time check (lab requirement footnote 2)
                    if (targetTime > DateTime.Now) break;
                    Console.Write("Time must be in future. Re-enter (HH:MM:SS): ");
                }
                else
                {
                    Console.Write("Invalid format. Re-enter (HH:MM:SS): ");
                }
            }
            return targetTime;
        }

        static void RingAlarm(object sender, EventArgs e)
        {
            Console.WriteLine("ALARM! Wake up!");
            Console.ReadKey();  
        }
    }
}
