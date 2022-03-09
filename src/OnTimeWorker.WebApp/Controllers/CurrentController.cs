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
    public class CurrentController : Controller
    {
        private static CurrentViewModel _model = new CurrentViewModel();

        private readonly RegisterClient _registerClient = new RegisterClient();
        private readonly UserClient _userClient = new UserClient();
        private readonly WorkerClient _workerClient = new WorkerClient();

        //
        private static readonly PaginattionService _paginattionService = new PaginattionService();

        private static bool _inserted;

        // Index
        public async Task<IActionResult> IndexAsync(Worker worker)
        {
            // Check if worker is informed
            if ((worker == null || worker.id <= 0) && !_inserted)
                return RedirectToAction("Index", "Login", new { navError = true });

            // Update Navbar with logged worker
            LoginController.navBarWorker = worker;

            // Assign data to model
            _model.worker = worker;
            _model.worker.user = await _userClient.GetUserByWorker(_model.worker);

            // Update worker status
            _model.worker.status = await _workerClient.GetStatusByWorkerId(_model.worker);

            // Await API response to get Current Registers For The Worker
            List<Register> response = await _registerClient.GetWorkerRegistersCurrentAsync(_model.worker);

            // Check response and add to list the registers
            _model.currentRegisters = new List<Register>();
            if (response != null)
            {
                _model.currentRegisters = response;
            }

            // Initialice first number page
            _model.pagNum = 1;

            // Check if paginattion is needed  and the range for slice
            _model.paginar = _paginattionService.IsPaginattionNeeded(8, _model.currentRegisters, null);

            // Get the total count for slice the array
            int range = _paginattionService.GetPaginattionRange(8, _model.currentRegisters, null);

            // Get "slice" registers part
            _model.pagCurrentRegisters = _model.currentRegisters.GetRange(0, range);

            // turn off _inserted switch
            _inserted = false;

            return View(_model);
        }

        // Paginar
        public async Task<IActionResult> Paginar()
        {
            try
            {
                // Index = Current number pag multiplify with MAX number of items wich pag
                int index = (_model.pagNum * 8);
                // Count = Total of items without pagination done yet
                int count = _model.currentRegisters.Count();

                // Get total rest of items with new index calculated
                var restOfRegisters = _paginattionService.GetRestOfRegisters(index, count, _model.currentRegisters);

                // Check if paginattion is needed  and the range for slice
                _model.paginar = _paginattionService.IsPaginattionNeeded(8, restOfRegisters, null);

                // Get the total count for slice the array
                int range = _paginattionService.GetPaginattionRange(8, restOfRegisters, null);

                // Get the next dataRegisters to add
                var includedRegisters = restOfRegisters.GetRange(0, range);

                // Add to the current pagDataRegisters the new includedDataRegisters
                _model.pagCurrentRegisters = _paginattionService.AddIncludedRegisters(includedRegisters, _model.pagCurrentRegisters);

                // plus 1 to current pag number
                _model.pagNum += 1;
            }
            catch (Exception ex)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 1, program = "CurrentController", text = "Paginar()", message = ex.Message });
            }

            return View("Index", _model);
        }

        // Insert register
        public async Task<IActionResult> Register()
        {
            // Take DateTime.Now Value
            string dateTime = DateTime.Now.ToString();

            // Create new registrer
            Register register = new Register() { worker = _model.worker, date = GetDateFromDateTime(dateTime), time = GetTimeFromDateTime(dateTime) };
            
            // Check current status of working, and inform stop type
            if (_model.worker.status.working)
                register.stop = true;
            else
                register.stop = false;

            // Get API Call response, insert register
            bool response = await _registerClient.InsertNewRegister(register);

            // Check API response, if register was NOT inserted success
            if (!response)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 2, program = "CurrentController", text = "Paginar()", message = "No se pudo insertar entrada/salida" });
                return RedirectToAction("Index", "Current", _model.worker);
            }

            // Get API Call response, update working status
            bool status = await _workerClient.UpdateWorkingStatus(_model.worker);

            // Check API response, if status was NOT updated success
            if (!status)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 3, program = "CurrentController", text = "Paginar()", message = "No se pudo actualizar el estado del empleado" });
            }

            // Turn on _inserted switch (CONTROLLER)
            _inserted = true;

            return RedirectToAction("Index", "Current", _model.worker);
        }

        // ESTO IRA  CON TIME FORMATER Y  DATE FORMATER DE LA API
        private string GetTimeFromDateTime(string dateTime)
        {
            string[] timeSplit = dateTime.Split(" ");

            return timeSplit[1];
        }

        private string GetDateFromDateTime(string dateTime)
        {
            string[] timeSplit = dateTime.Split(" ");
            string[] dateSplit = timeSplit[0].Split("/");

            return $"{dateSplit[2]}-{dateSplit[1]}-{dateSplit[0]}";
        }
    }
}
