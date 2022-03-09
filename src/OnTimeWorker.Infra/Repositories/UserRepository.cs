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
    public class UserRepository : IUserRepository
    {
        private List<User> GetUserFromDataTable(DataTable dt)
        {
            try
            {
                List<User> typeList = new List<User>();
                foreach (DataRow i in dt.Rows)
                {
                    User nextType = new User();

                    nextType.id = Int32.Parse(i["id"].ToString());
                    nextType.name = i["name"].ToString();
                    nextType.pwd = i["pwd"].ToString();
                    nextType.isAdmin = (bool)i["isAdmin"];
                    nextType.registrationDate = DateFormater.GetBackwardDate(i["registrationDate"].ToString());

                    typeList.Add(nextType);
                }
                return typeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> Get()
        {          
            try
            {
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT * from user", Connection.GetConnection());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<User> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList;
            } catch (Exception)
            {
                return null;
            }
        }
        public bool Insert(User user)
        {
            try
            {
                Connection.OpenConnection();
                string query = "INSERT INTO user(name, pwd, isAdmin, registrationDate) VALUES(?,?,?,?)";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("name", user.name));
                cmd.Parameters.Add(new MySqlParameter("pwd", user.pwd));
                cmd.Parameters.Add(new MySqlParameter("isAdmin", user.isAdmin));
                cmd.Parameters.Add(new MySqlParameter("registrationDate", user.registrationDate));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(User user)
        {
            try
            {
                Connection.OpenConnection();
                string query = "UPDATE user SET name = ?, pwd = ? WHERE id = ?";
                MySqlCommand cmd = new MySqlCommand(query, Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("name", user.name));
                cmd.Parameters.Add(new MySqlParameter("pwd", user.pwd));
                cmd.Parameters.Add(new MySqlParameter("id", user.id));
                cmd.ExecuteNonQuery();

                Connection.CloseConnection();
                return true;
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
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM user WHERE id = ?", Connection.GetConnection());

                cmd.Parameters.Add(new MySqlParameter("id", user.id));
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
        public User GetUserByName(User user)
        {
            try
            {
                Connection.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("SELECT * from user where name = ?", Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("name", user.name));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<User> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList[0];
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
                Connection.OpenConnection();
                //MySqlCommand cmd = new MySqlCommand("select * from user inner join worker on worker.userId = user.id WHERE worker.name = ? AND worker.secondName = ?", Connection.GetConnection());
                MySqlCommand cmd = new MySqlCommand("select * from user inner join worker on worker.userId = user.id WHERE worker.id = ?", Connection.GetConnection());
                cmd.Parameters.Add(new MySqlParameter("id", worker.id));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return null;

                List<User> typeList = GetUserFromDataTable(dt);

                Connection.CloseConnection();
                return typeList[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
