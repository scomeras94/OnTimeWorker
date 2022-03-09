using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.Validators
{
    public class WorkerValidator
    {
        public bool ValidateInsert(Worker worker)
        {
            if (worker == null)
                return false;
            if (worker.name == "" || worker.name == null)
                return false;
            if (worker.secondName == "" || worker.secondName == null)
                return false;
            if (worker.email == "" || worker.email == null)
                return false;
            if (worker.phone.ToString().Length != 9)
            {
                return false;
            }
            else
            {
                try
                {
                    Int32.Parse(worker.phone.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
            }
            if (worker.user == null)
                return false;
            if (worker.user.id <= 0)
                return false;

            return true;
        }

        public bool ValidateUpdate(Worker worker)
        {
            if (worker == null)
                return false;
            if (worker.id <= 0)
                return false;
            if (worker.name == "" || worker.name == null)
                return false;
            if (worker.secondName == "" || worker.secondName == null)
                return false;
            if (worker.email == "" || worker.email == null)
                return false;
            if (worker.phone.ToString().Length != 9)
            {
                return false;
            }
            else
            {
                try
                {
                    Int32.Parse(worker.phone.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ValidateDelete(Worker worker)
        {
            if (worker == null)
                return false;
            if (worker.id <= 0)
                return false;
            return true;
        }
    }
}
