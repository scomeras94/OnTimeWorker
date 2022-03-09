using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;

namespace OnTimeWorker.Core.Interfaces.Repositories
{
    public interface IRegisterDataRepository
    {
        public List<RegisterData> Get();
        public bool Insert(RegisterData register);
        public bool Update(RegisterData register);
        public bool Delete(RegisterData register);

        //
        public List<string> GetDistinctYearsFromAllRegisters(Worker worker);
        public List<Month> GetDistinctMonthsFromYear(Worker worker, string year);
        public List<RegisterData> GetFilteredDataRegisters(Worker worker, string year, string month);

        //
        public RegisterData GetDataRegisterById(RegisterData registerData);
    }
}
