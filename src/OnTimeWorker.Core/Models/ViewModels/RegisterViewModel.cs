using OnTimeWorker.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTimeWorker.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        //
        public Worker worker { get; set; }
        public List<RegisterData> dataRegisters { get; set; }
        public bool print { get; set; }

        //
        public List<string> yearsOfRegisters { get; set; }
        public string yearSelected { get; set; }
        public List<Month> monthsOfRegisters { get; set; }
        public Month monthSelected { get; set; }
        
        //
        public bool paginar { get; set; }
        public int pagNum { get; set; }
        public List<RegisterData> pagDataRegisters { get; set; }

        //
        public List<Error> errors { get; set; }
        public List<Success> success { get; set; }
    }
}
