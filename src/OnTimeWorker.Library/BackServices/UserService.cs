using OnTimerWorker.Library.Validators;
using OnTimeWorker.Core.Interfaces.Repositories;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.Repositories;
using OnTimeWorker.Infra.Utils;
using System;
using System.Collections.Generic;

namespace OnTimerWorker.Library.BackServices
{
    public class UserService : IUserRepository
    {
        private UserRepository _userRepository = new UserRepository();
        private UserValidator _validator = new UserValidator();

        public List<User> Get()
        {
            try
            {
                return _userRepository.Get();
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(User user)
        {
            try
            {
                if (!_validator.ValidateInsert(user))
                    return false;

                user.registrationDate = DateFormater.FormatDateTimeForMySQL();
                return _userRepository.Insert(user);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(User user)
        {
            try
            {
                if (!_validator.ValidateUpdate(user))
                    return false;

                return _userRepository.Update(user);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(User user)
        {
            try
            {
                if (!_validator.ValidateDelete(user))
                    return false;

                return _userRepository.Delete(user);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //
        public User GetUserByName(User user)
        {
            try
            {
                return _userRepository.GetUserByName(user);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User GetUserByWorker(Worker worker)
        {
            try
            {
                return _userRepository.GetUserByWorker(worker);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
