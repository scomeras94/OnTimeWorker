using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using OnTimeWorker.Core.Interfaces.Repositories;
using OnTimeWorker.Core.Models;
using OnTimeWorker.Infra.BBDD;
using OnTimeWorker.Infra.Utils;

namespace OnTimeWorker.Infra.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private List<Register> GetWorkersFromDataTable(DataTable dt)
        {
            try
            {
                List<Register> typeList = new List<Register>();
                foreach (DataRow i in dt.Rows)
                {
                    Register registerType = new Register();

                    registerType.id = Int32.Parse(i["id"].ToString());
                    registerType.date = DateFormater.GetBackwardDate(i["date"].ToString());
                    registerType.time = i["time"].ToString();
                    registerType.stop = (bool)i["stop"];

                    Worker workerType = new Worker();
                    workerType.id = Int32.Parse(i["workerId"].ToString());
                    workerType.name = i["name"].ToString();
                    workerType.secondName = i["secondName"].ToString();
                    workerType.phone = Int32.Parse(i["phone"].ToString());
                    workerType.email = i["email"].ToString();
                    workerType.registrationDate = DateFormater.GetBackwardDate(i["registrationDate"].ToString());

                    User userType = new User();
                    userType.id = Int32.Parse(i["userId"].ToString());
                    userType.name = i["userName"].ToString();
                    userType.pwd = i["pwd"].ToString();
                    userType.registrationDate = DateFormater.GetBackwardDate(i["userRegistrationDate"].ToString());
                    userType.isAdmin = (bool)i["isAdmin"];

                    WorkerStatus workerStatus = new WorkerStatus();
                    workerStatus.id = Int32.Parse(i["status"].ToString());
                    workerStatus.active = (bool)i["wsActive"];
                    workerStatus.working = (bool)i["wsWorking"];

                    workerType.status = workerStatus;
                    workerType.user = userType;
                    registerType.worker = workerType;

                    typeList.Add(registerType);
                }

                return typeList;
            }
            catch (Exception)
            {
                return null;
            }
        }
       
        public List<Register> Get()
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT register.*, ";
                query += "worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status, ";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM register ";
                query += "INNER JOIN worker on worker.id = register.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Register> typeList = GetWorkersFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            } catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(Register register)
        {
            try
            {
                Connection.OpenConnection();
                string query = "INSERT INTO register (workerId, date, time, stop) VALUES (?, ?, ?, ?)";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("workerId", register.worker.id));
                cmd.Parameters.Add(new MySqlParameter("date", register.date));
                cmd.Parameters.Add(new MySqlParameter("time", register.time));
                if (register.stop)
                    cmd.Parameters.Add(new MySqlParameter("isFinished", "1"));
                else
                    cmd.Parameters.Add(new MySqlParameter("isFinished", "0"));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();

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
                Connection.OpenConnection();
                string query = "UPDATE register set date = ?, time = ?, stop = ? WHERE workerId = ?";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("date", register.date));
                cmd.Parameters.Add(new MySqlParameter("time", register.time));
                if (register.stop)
                    cmd.Parameters.Add(new MySqlParameter("isFinished", "1"));
                else
                    cmd.Parameters.Add(new MySqlParameter("isFinished", "0"));
                cmd.Parameters.Add(new MySqlParameter("workerId", register.worker.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
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
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM register WHERE id = ?", Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("id", register.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
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
                Connection.OpenConnection();
                string query = "SELECT register.*, ";
                query += "worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status,";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM register ";
                query += "INNER JOIN worker on worker.id = register.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                query += "WHERE register.workerId = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("register.workerId", worker.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Register> typeList = GetWorkersFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
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
                Connection.OpenConnection();
                string query = "SELECT register.*, ";
                query += "worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status, ";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM register ";
                query += "INNER JOIN worker on worker.id = register.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                query += "WHERE register.workerId = ? AND register.date = (SELECT curdate())";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("register.workerId", worker.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Register> typeList = GetWorkersFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
