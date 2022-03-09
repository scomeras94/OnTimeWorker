using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.BackServices;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Filters;
using System.Collections.Generic;

namespace Oteocs_business_management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterDataController : ControllerBase
    {
        private RegisterDataService _registerDataService = new RegisterDataService();

        [HttpGet]
        public IEnumerable<RegisterData> Get()
        {
            return _registerDataService.Get();
        }

        [HttpPost("insert")]
        public bool Insert(RegisterData registerData)
        {
            return _registerDataService.Insert(registerData);
        }

        [HttpPost("update")]
        public bool Update(RegisterData registerData)
        {
            return _registerDataService.Update(registerData);
        }

        [HttpPost("delete")]
        public bool Delete(RegisterData registerData)
        {
            return _registerDataService.Delete(registerData);
        }

        // Custom Routes
        [HttpPost("GetAllYearsFromWorkerDataRegisters")]
        public IEnumerable<string> GetDistinctYearsFromAllRegisters(Worker worker)
        {
            return _registerDataService.GetDistinctYearsFromAllRegisters(worker);
        }

        [HttpPost("GetAllMonthsFromYear")]
        public IEnumerable<Month> GetDistinctMonthsFromYear(GetDistinctMonthsFromYear aux)
        {
            return _registerDataService.GetDistinctMonthsFromYear(new Worker { id = aux.workerId }, aux.selectedYear);
        }

        [HttpPost("GetFilteredDataRegisters")]
        public IEnumerable<RegisterData> GetFilteredRegisters(GetFilteredRegisters aux)
        {
            return _registerDataService.GetFilteredDataRegisters(new Worker { id = aux.workerId }, aux.selectedYear, aux.selectedMonth);
        }

        //
        [HttpPost("GetDataRegisterById")]
        public RegisterData GetDataRegisterById(RegisterData registerData)
        {
            return _registerDataService.GetDataRegisterById(registerData);
        }
    }
}
