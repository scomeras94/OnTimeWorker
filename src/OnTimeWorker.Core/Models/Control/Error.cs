using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimeWorker.Core.Models.Control
{
    public class Error
    {
        public int code { get; set; }
        public string program { get; set; }
        public string text { get; set; }
        public string message { get; set; }
    }
}
