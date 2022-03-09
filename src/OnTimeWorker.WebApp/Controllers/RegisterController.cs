using Microsoft.AspNetCore.Mvc;
using OnTimerWorker.Library.FrontServices;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Core.Models.Control;
using OnTimeWorker.Core.Models.Filters;
using OnTimeWorker.Core.Models.ViewModels;
using OnTimeWorker.Infra.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTimeWorker.WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private static RegisterViewModel _model = new RegisterViewModel();
        private static bool _edited;
        //
        private RegisterDataClient _registerDataClient = new RegisterDataClient();
        //
        private static readonly PaginattionService _paginattionService = new PaginattionService();
        private static readonly ValidatorService _validatorService = new ValidatorService();

        // Index
        public async Task<IActionResult> IndexAsync(Worker worker)
        {
            // Check if worker is informed
            if ((worker == null || worker.id <= 0) && ! _edited)
                return RedirectToAction("Index", "Login", new { navError = true });

            // Asign worker to model
            _model.worker = worker;

            // Reset filter parameters
            _model.yearSelected = "";
            _model.monthSelected = null;
            _model.monthsOfRegisters = new List<Month>();

            // Get API call response, get all distinct year that worker have dataRegisters
            List<string> yearsOfRegisters = await _registerDataClient.GetAllYearsFromWorkerDataRegisters(_model.worker);

            // Check API call response, asign years to model
            _model.yearsOfRegisters = new List<string>();
            if (yearsOfRegisters != null)
            {
                _model.yearsOfRegisters = yearsOfRegisters;
            }

            return View(_model);
        }

        // Filtrar (FilterRegistersAsync --> Combo year)
        public async Task<IActionResult> FilterRegistersAsync(string year)
        {
            // Check input year
            if (year == null || year.Length <= 0)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 1, program = "RegisterController", text = "FilterRegisters()", message = "Año no informado o con formato incorrecto" });
                return View("Index", _model);
            }

            // Create filter object
            GetDistinctMonthsFromYear filter = new GetDistinctMonthsFromYear { selectedYear = year, workerId = _model.worker.id };

            // Get API call response, get all distinct months from selected year
            List<Month> monthsOfRegisters = await _registerDataClient.GetAllMonthsFromYear(filter);

            // Check API call response
            if (monthsOfRegisters == null)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 2, program = "RegisterController", text = "FilterRegisters()", message = "No hay meses para el año seleccionado" });
                return View("Index", _model);
            }

            // Assign months to model
            _model.monthsOfRegisters = monthsOfRegisters;

            // Assign selected year
            _model.yearSelected = year;

            // Reset last selected month
            _model.monthSelected = null;

            return View("Index", _model);
        }
        public async Task<IActionResult> GetRegistersFiltered(string month)
        {
            // Check input month
            if (month == null || month.Length <= 0 || month == "-1")
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 3, program = "RegisterController", text = "GetRegistersFiltered()", message = "Mes no informado o con formato incorrecto" });
                return View("Index", _model);
            }

            // Create filter object
            GetFilteredRegisters filter = new GetFilteredRegisters { selectedYear = _model.yearSelected, workerId = _model.worker.id, selectedMonth = month };

            // Get API call response, get all dataRegisters filter by Year and Month
            List<RegisterData> filteredRegisters = await _registerDataClient.GetFilteredDataRegisters(filter);

            // Check API call response
            if (filteredRegisters == null)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 4, program = "RegisterController", text = "GetRegistersFiltered()", message = "No hay fichajes para el mes seleccionado" });
                return View("Index", _model);
            }

            // Assign dataRegisters to model
            _model.dataRegisters = filteredRegisters;

            // Turn on print switch
            _model.print = true;

            // Initialice first number page
            _model.pagNum = 1;

            // Check if paginattion is needed  and the range for slice
            _model.paginar = _paginattionService.IsPaginattionNeeded(15, null, _model.dataRegisters);

            // Get the total count for slice the array
            int range = _paginattionService.GetPaginattionRange(15, null, _model.dataRegisters);

            // Get "slice" dataRegisters part
            _model.pagDataRegisters = _model.dataRegisters.GetRange(0, range);

            return View("Index", _model);
        }

        // Combo Month
        public async Task<IActionResult> SetSelectedMonth(string month)
        {
            // Check input month
            if (month == null || month.Length <= 0 || month == "-1")
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 3, program = "RegisterController", text = "GetRegistersFiltered()", message = "Mes no informado o con formato incorrecto" });
                return View("Index", _model);
            }

            // Asign data to model
            _model.monthSelected = new Month(month);

            return View("Index", _model);
        }

        //Paginar
        public async Task<IActionResult> Paginar()
        {
            try
            {
                // Index = Current number pag multiplify with MAX number of items wich pag
                int index = (_model.pagNum * 15);
                // Count = Total of items without pagination done yet
                int count = _model.dataRegisters.Count();

                // Get all of the rest registers
                var restOfDataRegisters = _paginattionService.GetRestOfDataRegisters(index, count, _model.dataRegisters);

                // Check if paginattion is needed  and the range for slice
                _model.paginar = _paginattionService.IsPaginattionNeeded(15, null, restOfDataRegisters);

                // Get the total count for slice the array
                int range = _paginattionService.GetPaginattionRange(15, null, restOfDataRegisters);
                
                // Get the next dataRegisters to add
                var includedDataRegisters = restOfDataRegisters.GetRange(0, range);

                // Add to the current pagDataRegisters the new includedDataRegisters
                _model.pagDataRegisters = _paginattionService.AddIncludedDataRegisters(includedDataRegisters, _model.pagDataRegisters);

                // Plus 1 to pagNum, next page
                _model.pagNum += 1;

                // Turn on print switch (Controller)
                _model.print = true;
            } catch (Exception ex) {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 7, program = "RegisterController", text = "Paginar()", message = $"Excepcion: {ex.Message}" });
            }

            return View("Index", _model);
        }

        // Editar
        public async Task<IActionResult> RegisterDataEdit(int indexOfDataRegister)
        {
            // Check if the index informed is correct
            if (indexOfDataRegister == -1)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 5, program = "RegisterController", text = "RegisterDataEdit()", 
                    message = $"Indice informado con formato incorrecto: {indexOfDataRegister}" });

                return View("Index", _model);
            }

            // Check if can found a registerData with the informed index
            try
            {
                // Create registerData object with index informed
                RegisterData registerData = _model.dataRegisters[indexOfDataRegister];

                // Check registerData object value
                if (registerData == null)
                {
                    _model.errors = new List<Error>();
                    _model.errors.Add(new Error { code = 6, program = "RegisterController", text = "RegisterDataEdit()",
                        message = $"No se encontró registro a partir del indice: {indexOfDataRegister}" });

                    return View("Index", _model);
                }

                return View(new RegisterDataEditViewModel { registerData = registerData });
            } catch (Exception ex) {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 7, program = "RegisterController", text = "RegisterDataEdit()", message = $"Excepcion: {ex.Message}" });
            }

            return View("Index", _model);
        }
        public async Task<IActionResult> Edit(RegisterData registerData)
        {
            // Check if registeData was edited, and change values of properties
            if ((registerData.time_start == null || registerData.time_start == "") && (registerData.time_stop == null || registerData.time_stop == ""))
            {
                registerData.time_start = registerData.time_start_modified;
                registerData.time_stop = registerData.time_stop_modified;
            }

            RegisterDataEditViewModel editModel = new RegisterDataEditViewModel { registerData = registerData };

            // Validate new values before update
            editModel.errors = _validatorService.DataARegisterUpdateValidate(registerData);

            // Check if there are errors in validate process
            if (editModel.errors != null && editModel.errors.Count > 0)
            {
                return View("RegisterDataEdit", editModel);
            }  

            // turn on edited switch of REGISTERDATA
            registerData.edited = true;

            // Get API call response, update registerData with new values
            bool response = await _registerDataClient.Update(registerData);

            // Check API call response
            if (!response)
            {
                _model.errors = new List<Error>();
                _model.errors.Add(new Error { code = 8, program = "RegisterController", text = "Edit()", message = $"No se pudo actualizar fichaje" });
            }

            // Updated was success, create success object to inform to user
            _model.success = new List<Success>();
            _model.success.Add(new Success { tittle = "Proceso correcto", message = "Modificada entrada/salida del fichaje" });

            // turn on edited switch of MODEL
            _edited = true;

            return RedirectToAction("Index", "Register", _model.worker);
        }
    }
}
