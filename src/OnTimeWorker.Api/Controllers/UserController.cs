using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.BackServices;
using OnTimeWorker.Core.Models;
using System.Collections.Generic;

namespace Oteocs_business_management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService = new UserService();

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.Get();
        }

        [HttpPost("insert")]
        public bool Insert(User user)
        {
            return _userService.Insert(user);
        }

        [HttpPost("update")]
        public bool Update(User user)
        {
            return _userService.Update(user);
        }

        [HttpPost("delete")]
        public bool Delete(User user)
        {
            return _userService.Delete(user);
        }

        // Custom routes
        [HttpPost("GetUserByWorkerName")]
        public User GetUserByWorker(Worker worker)
        {
            return _userService.GetUserByWorker(worker);
        }
    }
}
