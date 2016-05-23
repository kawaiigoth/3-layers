using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using _EPAM_UserList.Entites;
using System.Configuration;
using System.Data.SqlClient;
using Logs;

namespace _EPAM_UserList.DAL.DB
{
    public class AppUserDAL : IAppUserListDAL
    {
        string UsersConnectionString = ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;

        public bool AddRole(AppUserDTO user, RoleDTO role)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.UsersRoles (Username, Role) VALUES(@userName, @roleName) ";
                command.Parameters.AddWithValue("@userName", user.Name);
                command.Parameters.AddWithValue("@roleName", role.Name);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
            {
                user.Roles.Add(role);
                return true;              
            }

            else
            {
                DBconnectException e = new DBconnectException("Can't add role to user");
                LogType.AddLog(e);
                return false;
            }
        }

        public bool Create(AppUserDTO user)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.AppUsers (Username, Password) VALUES(@userName, @userPassword) ";
                command.Parameters.AddWithValue("@userName", user.Name);
                command.Parameters.AddWithValue("@userPassword", user.Password);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
            {
                summary = 0;
                using (var connection = new SqlConnection(UsersConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO dbo.UsersRoles (Username, Role) VALUES(@userName, 'user') ";
                    command.Parameters.AddWithValue("@userName", user.Name);
                    connection.Open();
                    summary = command.ExecuteNonQuery();
                }
                if (summary > 0)
                    return true;
                else
                {
                    DBconnectException e = new DBconnectException("Can't add role to user");
                    LogType.AddLog(e);
                    return false;
                }
            }

            else
            {
                DBconnectException e = new DBconnectException("Can't create user");
                LogType.AddLog(e);
                return false;
            }
        }

        public bool Delete(string name)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.AppUsers WHERE Username=@name;";
                command.Parameters.AddWithValue("@name", name);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't delete user");
                LogType.AddLog(e);
                return false;
            }
        }

        public AppUserDTO Get(string name)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Username, Password FROM dbo.AppUsers WHERE Username= @name ;";
                fcommand.Parameters.AddWithValue("@name", name);
                connection.Open();
                AppUserDTO user = null;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pass = (Byte[])reader["Password"];

                        user = new AppUserDTO(name, pass);
                        scommand.CommandText = "SELECT Role FROM  dbo.UsersRoles u WHERE u.Username= @name";
                        scommand.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                var roleName = (string)new_reader["Role"];
                                user.Roles.Add(new RoleDTO(roleName));
                            }
                        }
                    }
                }

                return user;
            }
        }

        public IEnumerable<AppUserDTO> GetAll()
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Username, Password FROM dbo.AppUsers";
                connection.Open();
                AppUserDTO user = null;
                List<AppUserDTO> Users = new List<AppUserDTO>();
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pass = (Byte[])reader["Password"];
                        var name = reader["Username"] as string;
                        user = new AppUserDTO(name, pass);
                        scommand.CommandText = "SELECT Role FROM  dbo.UsersRoles u WHERE  u.Username=@name";
                        scommand.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                var roleName = new_reader["Role"] as string;
                                user.Roles.Add(new RoleDTO(roleName));
                            }
                        }
                        Users.Add(user);
                        scommand.Parameters.Clear();
                    }
                }

                return Users;
            }
        }

        public bool IsInRole(AppUserDTO user, RoleDTO role)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var name = user.Name;
                var Role = role.Name;
                fcommand.CommandText = "SELECT Username, Role FROM dbo.UsersRoles WHERE dbo.UsersRoles.Username=@name AND dbo.UsersRoles.Role=@role";
                fcommand.Parameters.AddWithValue("@name", name);
                fcommand.Parameters.AddWithValue("@role", Role);
                connection.Open();
                int summary=0;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        summary++;

                    }
                }
                if (summary > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public bool RemoveRole(AppUserDTO user, RoleDTO role)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.UsersRoles WHERE Username = @userName AND Role =  @roleName ";
                command.Parameters.AddWithValue("@userName", user.Name);
                command.Parameters.AddWithValue("@roleName", role.Name);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
            {
                user.Roles.Remove(role);
                return true;
            }

            else
            {
                DBconnectException e = new DBconnectException("Can't remove role from user");
                LogType.AddLog(e);
                return false;
            }
        }

        public bool Update(AppUserDTO user)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE dbo.AppUsers  SET  Password = @password WHERE Username = @name ";
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@name", user.Name);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't update user");
                LogType.AddLog(e);
                return false;
            }
        }
    }
}
