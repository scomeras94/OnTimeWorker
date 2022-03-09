using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Infra.Utils
{
    public class TimeFormater
    {
        public static string GetDifferenceTime(Register start_register, Register stop_register)
        {
            if (start_register.time.Length == 7)
                start_register.time = $"0{start_register.time}";
            if (stop_register.time.Length == 7)
                stop_register.time = $"0{stop_register.time}";

            DateTime start_time = DateTime.Parse(start_register.time);
            DateTime stop_time = DateTime.Parse(stop_register.time);
            TimeSpan difference_time = stop_time - start_time;

            string hours = difference_time.Hours.ToString();
            string minutes = difference_time.Minutes.ToString();
            string seconds = difference_time.Seconds.ToString();

            if (hours.Length == 1)
                hours = $"0{hours}";
            if (minutes.Length == 1)
                minutes = $"0{minutes}";
            if (seconds.Length == 1)
                seconds = $"0{seconds}";

            //return $"{diferenciaHoras.Hours}:{diferenciaHoras.Minutes}:{diferenciaHoras.Seconds}";
            return $"{hours}:{minutes}:{seconds}";
        }
    }
}
