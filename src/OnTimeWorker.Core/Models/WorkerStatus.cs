using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class WorkerStatus
    {
        public int id { get; set; }
        public bool active { get; set; }
        public bool working { get; set; }
    }
}
