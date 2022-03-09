using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.BackServices;
using OnTimeWorker.Core.Models;
using System.Collections.Generic;

namespace Oteocs_business_management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController : ControllerBase
    {
        private WorkerService _workerService = new WorkerService();

        [HttpGet]
        public IEnumerable<Worker> Get()
        {
            return _workerService.Get();
        }
        [HttpPost("insert")]
        public bool Insert(Worker worker)
        {
            return _workerService.Insert(worker);
        }
        [HttpPost("update")]
        public bool Update(Worker worker)
        {
            return _workerService.Update(worker);
        }
        [HttpPost("delete")]
        public bool Delete(Worker worker)
        {
            return _workerService.Delete(worker);
        }

        // Custom routes
        [HttpPost("GetStatusByWorkerId")]
        public WorkerStatus GetStatusByWorkerId(Worker worker)
        {
            return _workerService.GetStatusByWorkerId(worker);
        }

        [HttpPost("UpdateWorkingStatus")]
        public bool UpdateWorkingStatus(Worker worker)
        {
            return _workerService.UpdateWorkingStatus(worker);
        }
    }
}
