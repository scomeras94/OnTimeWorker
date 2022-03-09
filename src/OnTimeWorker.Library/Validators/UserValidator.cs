using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.Validators
{
    public class UserValidator
    {
        public bool ValidateInsert(User user)
        {
            if (user == null)
                return false;
            if (user.name == "" || user.name == null)
                return false;
            if (user.pwd == "" || user.pwd == null)
                return false;

            return true;
        }

        public bool ValidateUpdate(User user)
        {
            if (user == null)
                return false;
            if (user.id <= 0)
                return false;
            if (user.name == "" || user.name == null)
                return false;
            if (user.pwd == "" || user.pwd == null)
                return false;

            return true;
        }

        public bool ValidateDelete(User user)
        {
            if (user == null)
                return false;
            if (user.id <= 0)
                return false;

            return true;
        }
    }
}
