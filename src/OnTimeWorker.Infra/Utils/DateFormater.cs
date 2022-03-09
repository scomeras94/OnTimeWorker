using System;

namespace OnTimeWorker.Infra.Utils
{
    public class DateFormater
    {
        public static string GetDateNowInSqlFormat()
        {
            string[] split = DateTime.Now.ToString().Split(" ")[0].Trim().Split("/");
            return $"{split[2]}-{split[1]}-{split[0]}";
        }

        public static string GetBackwardDate(string date)
        {
            string[] split = date.Split("-");
            return $"{split[2]}-{split[1]}-{split[0]}";
        }
    }
}
