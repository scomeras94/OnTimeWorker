using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.BackServices;
using OnTimeWorker.Core.Models;
using System.Collections.Generic;

namespace Oteocs_business_management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private RegisterService _registerService = new RegisterService();

        [HttpGet]
        public IEnumerable<Register> Get()
        {
            return _registerService.Get();
        }

        [HttpPost("insert")]
        public bool Insert(Register register)
        {
            return _registerService.Insert(register);
        }

        [HttpPost("update")]
        public bool Update(Register register)
        {
            return _registerService.Update(register);
        }

        [HttpPost("delete")]
        public bool Delete(Register register)
        {
            return _registerService.Delete(register);
        }

        // Custom routes
        [HttpPost("getByWorkerCurrent")]
        public IEnumerable<Register> GetByWorkerIdCurrent(Worker worker)
        {
            return _registerService.GetRegistersByWorkerIdCurrent(worker);
        }
    }
}
