using System;

namespace TheKitchen
{
    public static class Converters
    {

        public static TimeSpan GetTimeSpan(string time)
        {
            int hours = 0;
            int minutes;
            if (int.TryParse(time.Split('h')[0], out int tempHours))
            {
                hours = tempHours;
                minutes = int.Parse(time.Split('h')[1].Replace("m", ""));
            }
            else
            {
                minutes = int.Parse(time.Split("m")[0]);
            }
            return new TimeSpan(hours, minutes, 0);
        }

        public static string GetTimeString(TimeSpan timeSpan)
        {
            string[] components = timeSpan.ToString().Split(':');
            string time = "";
            if (Double.Parse(components[0]) > 9)
            {
                time = components[0] + "h ";
            }
            else if (Double.Parse(components[0]) > 0)
            {
                time = components[0].Replace("0", "") + "h ";
            }
            time += components[1] + "m";
            return time;
        }
    }
}