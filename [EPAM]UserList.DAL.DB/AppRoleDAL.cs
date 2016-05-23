using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using _EPAM_UserList.Entites;
using System.Configuration;
using System.Data.SqlClient;

namespace _EPAM_UserList.DAL.DB
{
    public class AppRoleDAL : IAppRoleUserListDAL
    {
        string UsersConnectionString = ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;

        public RoleDTO Get(string name)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Role FROM dbo.Roles WHERE Role=@name ;";
                fcommand.Parameters.AddWithValue("@name", name);
                connection.Open();
                RoleDTO role = null;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        role = new RoleDTO(name);
                        scommand.CommandText = "SELECT a.Username, u.Username, a.Password FROM dbo.AppUsers a, dbo.UsersRoles u WHERE a.Username = u.Username AND u.Role=@name";
                        scommand.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                var username = (string)new_reader["Username"];
                                var password = (Byte[])new_reader["Password"];
                                role.Users.Add(new AppUserDTO(username,password));
                            }
                        }
                    }
                }

                return role;
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Role FROM dbo.Roles ;";
                connection.Open();
                RoleDTO role = null;
                List<RoleDTO> Roles = new List<RoleDTO>();
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["Role"] as string;

                        role = new RoleDTO(name);
                        scommand.CommandText = "SELECT a.Username, u.Username, a.Password FROM dbo.AppUsers a, dbo.UsersRoles u WHERE a.Username = u.Username AND u.Role=@name";
                        scommand.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                var username = (string)new_reader["Username"];
                                var password = (Byte[])new_reader["Password"];
                                role.Users.Add(new AppUserDTO(username, password));
                            }
                        }
                        Roles.Add(role);
                    }
                }

                return Roles;
            }
        }
    }
}
