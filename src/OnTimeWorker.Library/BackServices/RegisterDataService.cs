using OnTimeWorker.Core.Interfaces.Repositories;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.Repositories;
using OnTimeWorker.Infra.Utils;
using System;
using System.Collections.Generic;

namespace OnTimerWorker.Library.BackServices
{
    public class RegisterDataService : IRegisterDataRepository
    {
        private RegisterDataRepository _registerDataRepository = new RegisterDataRepository();

        //
        public List<RegisterData> Get()
        {
            try
            {
                return _registerDataRepository.Get();
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(RegisterData registerData)
        {
            try
            {
                //if (!ValidateInsert(user))
                   // return false;
                //user.registrationDate = DateFormater.FormatDateTimeForMySQL();
                return _registerDataRepository.Insert(registerData);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(RegisterData registerData)
        {
            try
            {
                //if (!ValidateUpdate(user))
                //return false;

                Register start = new Register { date = registerData.date, time = registerData.time_start, stop = false };
                Register stop = new Register { date = registerData.date, time = registerData.time_stop, stop = true };

                registerData.total_time_modified = TimeFormater.GetDifferenceTime(start, stop);

                return _registerDataRepository.Update(registerData);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(RegisterData registerData)
        {
            try
            {
                //if (!ValidateDelete(user))
                    //_registerDataRepository false;
                return _registerDataRepository.Delete(registerData);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //
        public List<string> GetDistinctYearsFromAllRegisters(Worker worker)
        {
            try
            {
                return _registerDataRepository.GetDistinctYearsFromAllRegisters(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<Month> GetDistinctMonthsFromYear(Worker worker, string year)
        {
            try
            {
                return _registerDataRepository.GetDistinctMonthsFromYear(worker, year);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<RegisterData> GetFilteredDataRegisters(Worker worker, string year, string month)
        {
            try
            {
                return _registerDataRepository.GetFilteredDataRegisters(worker, year, month);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //
        public RegisterData GetDataRegisterById(RegisterData registerData)
        {
            try
            {
                return _registerDataRepository.GetDataRegisterById(registerData);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
