using OnTimerWorker.Library.Validators;
using OnTimeWorker.Core.Interfaces.Repositories;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.Repositories;
using OnTimeWorker.Infra.Utils;
using System;
using System.Collections.Generic;

namespace OnTimerWorker.Library.BackServices
{
    public class RegisterService : IRegisterRepository
    {
        private RegisterRepository _registerRepository = new RegisterRepository();
        private RegisterDataService _registerDataService = new RegisterDataService();
        private RegisterValidator _validator = new RegisterValidator();

        public List<Register> Get()
        {
            try
            {
                return _registerRepository.Get();
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(Register register)
        {
            try
            {
                if (!_validator.ValidateInsert(register))
                    return false;

                //auxRegister.date = DateFormater.FormatDateTimeForMySQL();
                if (!_registerRepository.Insert(register))
                {
                    return false;
                }

                if (register.stop)
                {
                    List<Register> registers = GetRegistersByWorkerId(register.worker);
                    Register stop_register = registers[registers.Count - 1];
                    Register start_register = registers[registers.Count - 2];

                    if (!start_register.stop && stop_register.stop)
                    {
                        if (start_register.date == stop_register.date)
                        {
                            RegisterData registerData = new RegisterData
                            {
                                worker = register.worker,
                                date = DateFormater.GetBackwardDate(start_register.date),
                                register_start = start_register,
                                time_start = start_register.time,
                                register_stop = stop_register,
                                time_stop = stop_register.time,
                                total_time = TimeFormater.GetDifferenceTime(start_register, stop_register),
                                comments = "",
                                edited = false
                            };

                            _registerDataService.Insert(registerData);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Register register)
       {
            try
            {
                if (!_validator.ValidateUpdate(register))
                    return false;

                return _registerRepository.Update(register);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Register register)
        {
            try
            {
                if (!_validator.ValidateDelete(register))
                    return false;

                return _registerRepository.Delete(register);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //
        public List<Register> GetRegistersByWorkerId(Worker worker)
        {
            try
            {
                return _registerRepository.GetRegistersByWorkerId(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Register> GetRegistersByWorkerIdCurrent(Worker worker)
        {
            try
            {
                return _registerRepository.GetRegistersByWorkerIdCurrent(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
