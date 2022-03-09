using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class RegisterData
    {
        public int id { get; set; }
        public Worker worker { get; set; }
        public string date { get; set; }
        public Register register_start { get; set; }
        public string time_start { get; set; }
        public Register register_stop { get; set; }
        public string time_stop { get; set; }
        public string total_time { get; set; }
        public string comments { get; set; }
        public bool edited { get; set; }
        public string time_start_modified { get; set; }
        public string time_stop_modified { get; set; }
        public string total_time_modified { get; set; }
    }
}
