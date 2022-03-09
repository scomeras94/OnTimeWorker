using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;

namespace OnTimeWorker.Core.Interfaces.Repositories
{
    public interface IWorkerRepository
    {
        public List<Worker> Get();
        public bool Insert(Worker worker);
        public bool Update(Worker worker);
        public bool Delete(Worker worker);

        //
        public Worker GetWorkerByUserId(Worker worker);
        public WorkerStatus GetStatusByWorkerId(Worker worker);
        public bool UpdateWorkingStatus(Worker worker);
    }
}
