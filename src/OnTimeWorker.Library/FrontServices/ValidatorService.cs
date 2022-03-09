using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTimerWorker.Library.FrontServices
{
    public class ValidatorService
    {
        private static readonly CheckerService _checkerService = new CheckerService();

        //
        public List<Error> WorkerUpdateValidate(Worker worker)
        {
            Error eId = _checkerService.CheckId(worker.id);
            Error eIdentity = _checkerService.CheckIdentityDocument(worker.identityDocument);
            Error eName = _checkerService.CheckName(worker.name);
            Error eSecondName = _checkerService.CheckSecondName(worker.secondName);
            Error ePhone = _checkerService.CheckPhone(worker.phone);
            Error eMail = _checkerService.CheckEmail(worker.email);

            return _checkerService.DiscardNullErrors(new List<Error>() { eId, eIdentity, eName, eSecondName, ePhone, eMail });
        }
        public List<Error> PwdUpdateValidate(User user)
        {
            Error eId = _checkerService.CheckId(user.id);       
            Error eUserName = _checkerService.CheckIdentityDocument(user.name);// User name == Worker identityDoc, the check is the same
            Error ePwd = _checkerService.CheckPwd(user.pwd);

            return _checkerService.DiscardNullErrors(new List<Error>() { eId, eUserName, ePwd});
        }
        public List<Error> DataARegisterUpdateValidate(RegisterData registerData)
        {
            Error eDate = _checkerService.CheckDate(registerData.date);
            Error eStartTime = _checkerService.CheckTime(registerData.time_start);
            Error eStopTime = _checkerService.CheckTime(registerData.time_stop);

            return _checkerService.DiscardNullErrors(new List<Error>() { eDate, eStartTime, eStopTime });
        }
    }
}
