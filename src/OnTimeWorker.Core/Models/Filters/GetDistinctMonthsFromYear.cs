using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models.Filters
{
    public class GetDistinctMonthsFromYear
    {
        public int workerId { get; set; }
        public string selectedYear { get; set; }
    }
}
