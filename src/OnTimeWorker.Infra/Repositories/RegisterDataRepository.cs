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
    public class RegisterDataRepository : IRegisterDataRepository
    {
        private List<RegisterData> GetUserFromDataTable(DataTable dt)
        {
            try
            {
                List<RegisterData> typeList = new List<RegisterData>();
                foreach (DataRow i in dt.Rows)
                {
                    RegisterData registerType = new RegisterData();
                    registerType.id = Int32.Parse(i["id"].ToString());
                    registerType.date = DateFormater.GetBackwardDate(i["date"].ToString());
                    registerType.time_start = i["time_start"].ToString();
                    registerType.time_stop = i["time_stop"].ToString();
                    registerType.total_time = i["total_time"].ToString();
                    registerType.comments = i["comments"].ToString();
                    registerType.edited = (bool) i["edited"];
                    registerType.time_start_modified = i["time_start_modified"].ToString();
                    registerType.time_stop_modified = i["time_stop_modified"].ToString();
                    registerType.total_time_modified = i["total_time_modified"].ToString();

                    User userType = new User();
                    userType.id = Int32.Parse(i["userId"].ToString());
                    userType.name = i["userName"].ToString();
                    userType.pwd = i["pwd"].ToString();
                    userType.isAdmin = (bool)i["isAdmin"];
                    userType.registrationDate = DateFormater.GetBackwardDate(i["userRegistrationDate"].ToString());

                    WorkerStatus workerStatus = new WorkerStatus();
                    workerStatus.id = Int32.Parse(i["status"].ToString());
                    workerStatus.active = (bool)i["wsActive"];
                    workerStatus.working = (bool)i["wsWorking"];

                    Worker workerType = new Worker();
                    workerType.identityDocument = i["identityDocument"].ToString();
                    workerType.id = Int32.Parse(i["workerId"].ToString());
                    workerType.name = i["name"].ToString();
                    workerType.secondName = i["secondName"].ToString();
                    workerType.phone = Int32.Parse(i["phone"].ToString());
                    workerType.email = i["email"].ToString();
                    workerType.registrationDate = DateFormater.GetBackwardDate(i["registrationDate"].ToString());
                    workerType.user = userType;
                    workerType.status = workerStatus;

                    Register startRegister = new Register();
                    startRegister.id = Int32.Parse(i["register_start_id"].ToString());
                    startRegister.worker = workerType;
                    startRegister.date = DateFormater.GetBackwardDate(i["strRegisterDate"].ToString());
                    startRegister.time = i["strRegisterTime"].ToString();
                    startRegister.stop = (bool) i["strRegisterStop"];

                    Register stopRegister = new Register();
                    stopRegister.id = Int32.Parse(i["register_stop_id"].ToString());
                    stopRegister.worker = workerType;
                    stopRegister.date = DateFormater.GetBackwardDate(i["stpRegisterDate"].ToString());
                    stopRegister.time = i["stpRegisterTime"].ToString();
                    stopRegister.stop = (bool)i["stpRegisterStop"];

                    registerType.worker = workerType;
                    registerType.register_start = startRegister;
                    registerType.register_stop = stopRegister;

                    typeList.Add(registerType);
                }
                return typeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //
        public List<RegisterData> Get()
        {          
            try
            {
                Connection.OpenConnection();
                string query = "SELECT registerdata.*, ";
                query += "worker.identityDocument, worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status,";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "r1.date as strRegisterDate, r1.time as strRegisterTime, r1.stop as strRegisterStop, ";
                query += "r2.date as stpRegisterDate, r2.time as stpRegisterTime, r2.stop as stpRegisterStop, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM registerdata ";
                query += "INNER JOIN worker on worker.id = registerdata.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN register r1 on r1.id = registerdata.register_start_id ";
                query += "INNER JOIN register r2 on r2.id = registerdata.register_stop_id ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<RegisterData> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            } catch (Exception ex)
            {
                return null;
            }
        }
        public bool Insert(RegisterData registerData)
        {
            try
            {
                Connection.OpenConnection();
                string query = "INSERT into registerdata (workerId, date, register_start_id, time_start, register_stop_id, time_stop, total_time, comments, edited) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("workerId", registerData.worker.id));
                cmd.Parameters.Add(new MySqlParameter("date", registerData.date));
                cmd.Parameters.Add(new MySqlParameter("register_start_id", registerData.register_start.id));
                cmd.Parameters.Add(new MySqlParameter("time_start", registerData.time_start));
                cmd.Parameters.Add(new MySqlParameter("register_stop_id", registerData.register_stop.id));
                cmd.Parameters.Add(new MySqlParameter("time_stop", registerData.time_stop));
                cmd.Parameters.Add(new MySqlParameter("total_time", registerData.total_time));
                cmd.Parameters.Add(new MySqlParameter("comments", registerData.comments));
                cmd.Parameters.Add(new MySqlParameter("edited", registerData.edited));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(RegisterData registerData)
        {
            try
            {
                Connection.OpenConnection();
                string query = "UPDATE registerdata SET comments = ?, edited = ?, time_start_modified = ?, time_stop_modified = ?, total_time_modified = ? WHERE id = ?";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("comments", registerData.comments));
                cmd.Parameters.Add(new MySqlParameter("edited", registerData.edited));
                cmd.Parameters.Add(new MySqlParameter("time_start_modified", registerData.time_start));
                cmd.Parameters.Add(new MySqlParameter("time_stop_modified", registerData.time_stop));
                cmd.Parameters.Add(new MySqlParameter("total_time_modified", registerData.total_time_modified));      
                cmd.Parameters.Add(new MySqlParameter("id", registerData.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
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
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM registerdata WHERE id = ?", Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("id", registerData.id));
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
        public List<RegisterData> GetFilteredDataRegisters(Worker worker, string year, string month)
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT registerdata.*, ";
                query += "worker.identityDocument, worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status,";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "r1.date as strRegisterDate, r1.time as strRegisterTime, r1.stop as strRegisterStop, ";
                query += "r2.date as stpRegisterDate, r2.time as stpRegisterTime, r2.stop as stpRegisterStop, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM registerdata ";
                query += "INNER JOIN worker on worker.id = registerdata.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN register r1 on r1.id = registerdata.register_start_id ";
                query += "INNER JOIN register r2 on r2.id = registerdata.register_stop_id ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                query += "WHERE registerdata.workerId = ? ";
                query += "HAVING EXTRACT(year from date) = ? AND EXTRACT(month from date) = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("register.workerId", worker.id));
                cmd.Parameters.Add(new MySqlParameter("year", year));
                cmd.Parameters.Add(new MySqlParameter("month", month));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<RegisterData> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<string> GetDistinctYearsFromAllRegisters(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT DISTINCT EXTRACT(year from date) as year from registerdata where registerdata.workerId = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("registerdata.workerId", worker.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<string> years = new List<string>();
                foreach (DataRow i in dt.Rows)
                {
                    years.Add(i["year"].ToString());
                }

                Connection.CloseConnection();
                return years;
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
                Connection.OpenConnection();
                string query = "SELECT DISTINCT EXTRACT(month from date) as month, EXTRACT(year from date) as year ";
                query = query + "from registerdata ";
                query = query + "where registerdata.workerId = ? ";
                query = query + "having year = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("registerdata.workerId", worker.id));
                cmd.Parameters.Add(new MySqlParameter("year", year));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Month> months = new List<Month>();
                foreach (DataRow i in dt.Rows)
                {
                    months.Add(new Month(i["month"].ToString()));
                }

                Connection.CloseConnection();
                return months;
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
                Connection.OpenConnection();
                string query = "SELECT registerdata.*, ";
                query += "worker.identityDocument, worker.name, worker.secondName, worker.phone, worker.email, worker.userId, worker.registrationDate, worker.status,";
                query += "user.name as userName, user.pwd, user.isAdmin, user.registrationDate as userRegistrationDate, ";
                query += "r1.date as strRegisterDate, r1.time as strRegisterTime, r1.stop as strRegisterStop, ";
                query += "r2.date as stpRegisterDate, r2.time as stpRegisterTime, r2.stop as stpRegisterStop, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM registerdata ";
                query += "INNER JOIN worker on worker.id = registerdata.workerId ";
                query += "INNER JOIN user on user.id = worker.userId ";
                query += "INNER JOIN register r1 on r1.id = registerdata.register_start_id ";
                query += "INNER JOIN register r2 on r2.id = registerdata.register_stop_id ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                query += "WHERE registerData.id = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("registerData.id", registerData.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<RegisterData> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
