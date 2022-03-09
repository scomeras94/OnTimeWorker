using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.Validators
{
    public class RegisterValidator
    {
        public bool ValidateInsert(Register register)
        {
            if (register == null)
                return false;
            if (register.worker.id <= 0)
                return false;
            if (register.date == "" || register.date == null)
                return false;
            if (register.time == "" || register.time == null)
                return false;

            return true;
        }

        public bool ValidateUpdate(Register register)
        {
            if (register == null)
                return false;
            if (register.id <= 0)
                return false;
            if (register.date == "" || register.date == null)
                return false;
            if (register.time == "" || register.time == null)
                return false;

            return true;
        }

        public bool ValidateDelete(Register register)
        {
            if (register == null)
                return false;
            if (register.id <= 0)
                return false;
            return true;
        }
    }
}
