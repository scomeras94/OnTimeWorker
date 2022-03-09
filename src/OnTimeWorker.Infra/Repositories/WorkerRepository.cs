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
    public class WorkerRepository : IWorkerRepository
    {
        private List<Worker> GetWorkersFromDataTable(DataTable dt)
        {
            try
            {
                List<Worker> typeList = new List<Worker>();
                foreach (DataRow i in dt.Rows)
                {
                    Worker workerType = new Worker();
                    workerType.id = Int32.Parse(i["id"].ToString());
                    workerType.identityDocument = i["identityDocument"].ToString();
                    workerType.name = i["name"].ToString();
                    workerType.secondName = i["secondName"].ToString();
                    workerType.phone = Int32.Parse(i["phone"].ToString());
                    workerType.email = i["email"].ToString();
                    workerType.registrationDate = DateFormater.GetBackwardDate(i["registrationDate"].ToString());

                    User userType = new User();
                    userType.id = Int32.Parse(i["userId"].ToString());
                    userType.name = i["userName"].ToString();
                    userType.pwd = i["userPwd"].ToString();
                    userType.isAdmin = (bool)i["userIsAdmin"];

                    WorkerStatus workerStatus = new WorkerStatus();
                    workerStatus.id = Int32.Parse(i["status"].ToString());
                    workerStatus.active = (bool)i["wsActive"];
                    workerStatus.working = (bool)i["wsWorking"];

                    workerType.status = workerStatus;
                    workerType.user = userType;
                    typeList.Add(workerType);
                }

                return typeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Worker> Get()
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT worker.*, user.name as userName, user.pwd as userPwd, user.isAdmin as userIsAdmin, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM worker ";
                query += "INNER join user on user.id = worker.userId ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Worker> typeList = GetWorkersFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "INSERT into worker (identityDocument, name, secondName, phone, email, userId, registrationDate) VALUES (?, ?, ?, ?, ?, ?, ?);";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("identityDocument", worker.identityDocument));
                cmd.Parameters.Add(new MySqlParameter("name", worker.name));
                cmd.Parameters.Add(new MySqlParameter("secondName", worker.secondName));
                cmd.Parameters.Add(new MySqlParameter("phone", worker.phone));
                cmd.Parameters.Add(new MySqlParameter("email", worker.email));
                cmd.Parameters.Add(new MySqlParameter("userId", worker.user.id));
                cmd.Parameters.Add(new MySqlParameter("registrationDate", worker.registrationDate));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "UPDATE worker set identityDocument = ?, name = ?, secondName = ?, phone = ?, email = ? WHERE id = ?";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("identityDocument", worker.identityDocument));
                cmd.Parameters.Add(new MySqlParameter("name", worker.name));
                cmd.Parameters.Add(new MySqlParameter("secondName", worker.secondName));
                cmd.Parameters.Add(new MySqlParameter("phone", worker.phone));
                cmd.Parameters.Add(new MySqlParameter("email", worker.email));
                cmd.Parameters.Add(new MySqlParameter("id", worker.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM worker WHERE id = ?", Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("id", worker.id));
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
        public Worker GetWorkerByUserId(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT worker.*, user.name as userName, user.pwd as userPwd, user.isAdmin as userIsAdmin, ";
                query += "worker_status.active as wsActive, worker_status.working as wsWorking ";
                query += "FROM worker ";
                query += "INNER join user on user.id = worker.userId ";
                query += "INNER JOIN worker_status ON worker_status.id = worker.status ";
                query += "WHERE user.id = ? ";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("userId", worker.user.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<Worker> typeList = GetWorkersFromDataTable(dt);

                Connection.CloseConnection();
                return typeList[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public WorkerStatus GetStatusByWorkerId(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "SELECT worker_status.* ";
                query += "FROM worker ";
                query += "INNER JOIN worker_status on worker_status.id = worker.status ";
                query += "WHERE worker.id = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("worker.id", worker.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                WorkerStatus wStatus = new WorkerStatus();
                foreach (DataRow i in dt.Rows)
                {
                    wStatus.id = Int32.Parse(i["id"].ToString());
                    wStatus.active = (bool)i["active"];
                    wStatus.working = (bool)i["working"];
                }

                Connection.CloseConnection();
                return wStatus;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool UpdateWorkingStatus(Worker worker)
        {
            try
            {
                Connection.OpenConnection();
                string query = "UPDATE worker_status ";
                query += "INNER JOIN worker on worker.status = worker_status.id ";
                query += "SET worker_status.working = ? ";
                query += "WHERE worker.id = ?";

                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("worker_status.working", worker.status.working));
                cmd.Parameters.Add(new MySqlParameter("worker.id", worker.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
