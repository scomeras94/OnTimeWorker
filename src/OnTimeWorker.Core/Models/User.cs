using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pwd { get; set; }
        public bool isAdmin { get; set; }
        public string registrationDate { get; set; }
    }
}
