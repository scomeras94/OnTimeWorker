using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.BackServices;
using OnTimeWorker.Core.Models;
using System.Threading.Tasks;

namespace Oteocs_business_management_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService = new LoginService();

        [HttpPost("login")]
        public async Task<Worker> LoginAsync(User user)
        {
            Worker loggedWorker = await _loginService.LoginAsync(user);
            return loggedWorker;
        }
    }
}
