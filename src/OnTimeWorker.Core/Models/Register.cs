using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class Register
    {
        public int id { get; set; }
        public Worker worker { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public bool stop { get; set; }
    }
}
