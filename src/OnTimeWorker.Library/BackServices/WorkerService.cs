using OnTimerWorker.Library.Validators;
using OnTimeWorker.Core.Interfaces.Repositories;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.Repositories;
using OnTimeWorker.Infra.Utils;
using System;
using System.Collections.Generic;

namespace OnTimerWorker.Library.BackServices
{
    public class WorkerService : IWorkerRepository
    {
        private WorkerRepository _workerRepository = new WorkerRepository();
        private WorkerValidator _validator = new WorkerValidator();

        public List<Worker> Get()
        {
            try
            {
                return _workerRepository.Get();
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(Worker worker)
        {
            try
            {
                if (!_validator.ValidateInsert(worker))
                    return false;

                worker.registrationDate = DateFormater.GetDateNowInSqlFormat();
                return _workerRepository.Insert(worker);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Worker worker)
       {
            try
            {
                if (!_validator.ValidateUpdate(worker))
                    return false;

                return _workerRepository.Update(worker);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Worker worker)
        {
            try
            {
                if (!_validator.ValidateDelete(worker))
                    return false;

                return _workerRepository.Delete(worker);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //
        public Worker GetWorkerByUserId(Worker worker)
        {
            try
            {
                return _workerRepository.GetWorkerByUserId(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public WorkerStatus GetStatusByWorkerId(Worker worker)
        {
            try
            {
                return _workerRepository.GetStatusByWorkerId(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool UpdateWorkingStatus(Worker worker)
        {
            // Get Last Status modified on database
            WorkerStatus currentStatus = _workerRepository.GetStatusByWorkerId(worker);
            worker.status = currentStatus;

            // Get a bool Variable of next status. working = true --> next = false
            bool changeWorking = false;
            if (worker.status.working)
            {
                changeWorking = false;
            } else
            {
                changeWorking = true;
            }

            worker.status.working = changeWorking;

            bool update = _workerRepository.UpdateWorkingStatus(worker);

            return update;
        }
    }
}
