using OnTimeWorker.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTimeWorker.Core.Models.ViewModels
{
    public class WorkerViewModel
    {
        public Worker worker { get; set; }

        //
        public List<Error> errors { get; set; }
        public List<Success> success { get; set; }
    }
}
