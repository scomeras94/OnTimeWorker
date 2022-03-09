using OnTimeWorker.Core.Models;
using System;
using System.Collections.Generic;

namespace OnTimeWorker.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public List<User> Get();
        public bool Insert(User user);
        public bool Update(User user);
        public bool Delete(User user);

        //
        public User GetUserByName(User user);
        public User GetUserByWorker(Worker worker);
    }
}
