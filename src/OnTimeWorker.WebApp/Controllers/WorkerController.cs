using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.FrontServices;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Control;
using OnTimeWorker.Core.Models.ViewModels;
using OnTimeWorker.Infra.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTimeWorker.WebApp.Controllers
{
    public class WorkerController : Controller
    {
        private static WorkerViewModel _model = new WorkerViewModel();
        private static UserClient _userClient = new UserClient();
        private static WorkerClient _workerClient = new WorkerClient();
        private static ValidatorService _validatorService = new ValidatorService();

        private static bool _edited;

        // Index
        public async Task<IActionResult> Index(Worker worker)
        {
            // Check if worker is informed
            if ((worker == null || worker.id <= 0) && !_edited)
                return RedirectToAction("Index", "Login", new { navError = true });

            // Assign worker to model
            _model.worker = worker;

            // Await API response to get the user of the current worker
            User user = await _userClient.GetUserByWorker(_model.worker);

            // Check response and add to list the registers
            if (user == null)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 1, program = "WorkerController", text = "Index()", message = "No se encontró usuario del empleado" });
            }

            // Assign user to model
            _model.worker.user = user;

            return View(_model);
        }

        // GoTo edit views
        public IActionResult EditForm(Worker worker)
        {
            try
            {
                return View(_model);
            } catch (Exception ex) {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 2, program = "WorkerController", text = "EditForm()", message = ex.Message });
            }

            return View("Index", _model.worker);
        }

        public IActionResult PwdForm(Worker worker)
        {
            try
            {
                return View(_model);
            }
            catch (Exception ex)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 3, program = "WorkerController", text = "PwdForm()", message = ex.Message });
            }

            return View("Index", _model.worker);
        }

        // Update process
        public async Task<IActionResult> UpdateWorker(Worker worker)
        {
            // Initialize error and success lists
            _model.errors = new List<Error>();
            _model.success = new List<Success>();

            // Validate errors from new worker values
            _model.errors = _validatorService.WorkerUpdateValidate(worker);

            // Check if there are errors in the list
            if (_model.errors != null && _model.errors.Count > 0)
                return RedirectToAction("EditForm", "Worker", _model.worker);

            // Await API response to update worker
            bool response = await _workerClient.UpdateWorker(worker);

            // Check response, know if worker have updated
            if (response)
            {
                // Assign data to model
                _model.worker.name = worker.name;
                _model.worker.secondName = worker.secondName;
                _model.worker.phone = worker.phone;
                _model.worker.email = worker.email;

                // Add success item
                _model.success.Add(new Success { tittle = "Proceso correcto", message = ("Modificados los datos del trabajador " + _model.worker.identityDocument) });
            } else
            {
                // Add error item
                _model.errors.Add(new Error { code = 4, program = "WorkerController", text = "UpdateWorker()", message = "No se pudo actualizar al trabajador" });
            }

            // Turn on _edited switch of the CONTROLLER
            _edited = true;

            return RedirectToAction("Index", "Worker", _model.worker);
        }

        public async Task<IActionResult> UpdatePwd(Worker worker)
        {
            // Initialize error and success lists
            _model.errors = new List<Error>();
            _model.success = new List<Success>();

            // Validate errors from new worker values
            _model.errors = _validatorService.PwdUpdateValidate(worker.user);

            // Check if there are errors in the list
            if (_model.errors != null && _model.errors.Count > 0)
                return RedirectToAction("PwdForm", "Worker", _model.worker);

            // Await API response to update user
            bool response = await _userClient.UpdateUser(worker.user);

            // Check response, know if user have updated
            if (response)
            {
                // Assign data to model
                _model.worker.user.pwd = worker.user.pwd;

                // Add success item
                _model.success.Add(new Success{ tittle = "Proceso correcto", message = "Modificada contraseña correctamente" });
            } else {
                // Add error item
                _model.errors.Add(new Error { code = 6, program = "WorkerController", text = "UpdatePwd()", message = "No se pudo actualizar el usuario" });
            }

            // Turn on _edited switch of the CONTROLLER
            _edited = true;

            return RedirectToAction("Index", "Worker", _model.worker);
        }
    }
}
