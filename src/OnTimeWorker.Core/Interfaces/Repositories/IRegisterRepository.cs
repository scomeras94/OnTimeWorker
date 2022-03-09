using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;

namespace OnTimeWorker.Core.Interfaces.Repositories
{
    public interface IRegisterRepository
    {
        public List<Register> Get();
        public bool Insert(Register register);
        public bool Update(Register register);
        public bool Delete(Register register);

        //
        public List<Register> GetRegistersByWorkerId(Worker worker);
        public List<Register> GetRegistersByWorkerIdCurrent(Worker worker);
    }
}
