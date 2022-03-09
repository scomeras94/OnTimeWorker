using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.FrontServices;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Control;
using OnTimeWorker.Core.Models.ViewModels;
using OnTimeWorker.Infra.HttpClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnTimeWorker.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private LoginClient _loginClient = new LoginClient();
        private static LoginViewModel _model = new LoginViewModel();

        //
        private static readonly CheckerService _checkerService = new CheckerService();

        public static Worker navBarWorker = new Worker();

        //
        public IActionResult Index(bool navError)
        {
            // Update Navbar with un-logged worker
            LoginController.navBarWorker = null;

            // Reset model
            _model = new LoginViewModel();

            // Check if there is no-login user navbar error
            if (navError)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 1, program = "LoginController", text = "NavBar", message = "Usuario no identificado" });
            }   

            return View(_model);
        }

        public async Task<IActionResult> LoginAsync(User user)
        {
            // Initialice errors list
            _model.errors = new List<Error>();

            // Check input user name value is not empty
            if (_checkerService.CheckName(user.name) != null)        
                _model.errors.Add(new Error { code = 2, program = "LoginController", text = "Login()", message = "Usuario no informado" });

            // Check input user pwd value is not empty
            if (_checkerService.CheckPwd(user.pwd) != null)
                _model.errors.Add(new Error { code = 3, program = "LoginController", text = "Login()", message = "Contraseña no informada" });

            // Check if there are errors
            if (_model.errors != null && _model.errors.Count > 0)
                return View("Index", _model);

            // Get Worker response from API login call, send user/pwd (User model) and get response of obect worker
            Worker response = await _loginClient.LoginAsync(user);

            // Check API response
            if (response == null)
            {
                _model.errors.Add(new Error { code = 2, program = "LoginController", text = "Login()", message = "Usuario o contraseña incorrectos." });
                return View("Index", _model);
            }

            // Log in was success and go to Current->index view.
            _model.user = response.user;

            return RedirectToAction("Index", "Current", response);
        }

        public IActionResult Clean()
        {
            _model = new LoginViewModel();
            return View("Index", _model);
        }
    }
}
