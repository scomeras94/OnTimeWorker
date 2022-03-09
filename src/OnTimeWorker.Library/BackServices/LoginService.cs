using OnTimeWorker.Core.Models;
using System;
using System.Threading.Tasks;

namespace OnTimerWorker.Library.BackServices
{
    public class LoginService
    {
        private UserService _userService = new UserService();
        private WorkerService _workerService = new WorkerService();

        public async Task<Worker> LoginAsync(User user)
        {
            try
            {
                User findUser = _userService.GetUserByName(user);
                if (findUser == null)
                    return null;
                //return ErrorGenerator.ReturnNewError(1, "LoginService.cs", "Login", "Usuario no encontrado");


                if (findUser.pwd != user.pwd)
                    return null;
                //return ErrorGenerator.ReturnNewError(2, "LoginService.cs", "Login", "Contraseña incorrecta");


                Worker getWorker = (Worker) _workerService.GetWorkerByUserId(new Worker() { user = findUser });
                if (getWorker == null)
                    return null;
                    //return ErrorGenerator.ReturnNewError(3, "LoginService.cs", "Login", "Empleado no encontrado");

                return getWorker;
            } catch (Exception ex)
            {
                return null;
                //return ErrorGenerator.ReturnNewError(4, "LoginService.cs", "Login -> try-catch", $"{ex}");
            }
        }
    }
}
