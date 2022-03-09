using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class Worker
    {
        public int id { get; set; }
        public string identityDocument { get; set; }
        public string name { get; set; }
        public string secondName { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public User user { get; set; }
        public WorkerStatus status { get; set; }
        public string registrationDate { get; set; }
    }
}
