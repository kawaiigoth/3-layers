using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Logs;

namespace _EPAM_UserList.DAL.DB
{
    public class UserDAL : IUserListDAL
    {
        string UsersConnectionString = ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;

        public bool Create(UserDTO user)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.Users_Table (Guid, Name, Date, image_id) VALUES(@UserId, @UserName, @UserBD, @UserImg) ";
                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@userBD", user.DateOfBirth);
                command.Parameters.AddWithValue("@UserImg", user.Image);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't create user-model");
                LogType.AddLog(e);
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM dbo.Users_Table WHERE Guid= @ID ;";
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't delete user-model");
                LogType.AddLog(e);
                return false;
            }
        }

        public UserDTO Get(Guid id)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Guid, Name, image_id, Date FROM dbo.Users_Table WHERE Guid= @ID ;";
                fcommand.Parameters.AddWithValue("@ID", id);
                connection.Open();
                UserDTO user = null;
                Guid guid;
                string name;
                int image;
                DateTime sdate;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        guid = (Guid)reader["Guid"];
                        name = (string)reader["Name"];
                        image = (int)reader["image_id"];
                        sdate = (DateTime)reader["Date"];
                        user = new UserDTO(name, sdate, guid, image);
                        scommand.CommandText = "SELECT [Guid], [Name], [image_id] FROM dbo.Awards_Table a, dbo.UsersAwards u WHERE a.Guid = u.award_id AND u.user_id= @guid";
                        scommand.Parameters.AddWithValue("@guid", guid);

                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                guid = (Guid)new_reader["Guid"];
                                name = (string)new_reader["Name"];
                                image = (int)new_reader["image_id"];
                                user.Awards.Add(new AwardDTO(name, guid, image));

                            }
                        }
                    }
                }

                return user;
            }

        }

        public IEnumerable<UserDTO> GetAll()
        {

            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                List<UserDTO> Users = new List<UserDTO>();
                fcommand.CommandText = "SELECT Guid, Name, image_id, Date FROM dbo.Users_Table;";
                connection.Open();
                UserDTO user = null;
                Guid guid;
                string name;
                int image;
                DateTime sdate;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        guid = (Guid)reader["Guid"];
                        name = (string)reader["Name"];
                        image = (int)reader["image_id"];
                        sdate = (DateTime)reader["Date"];
                        user = new UserDTO(name, sdate, guid, image);
                        scommand.CommandText = "SELECT [Guid], [Name], [image_id] FROM dbo.Awards_Table a, dbo.UsersAwards u WHERE a.Guid = u.award_id AND u.user_id= @guid";
                        scommand.Parameters.AddWithValue("@guid", guid);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                guid = (Guid)new_reader["Guid"];
                                name = (string)new_reader["Name"];
                                image = (int)new_reader["image_id"];
                                user.Awards.Add(new AwardDTO(name, guid, image));

                            }
                        }
                        Users.Add(user);
                        scommand.Parameters.Clear();
                    }
                }

                return Users;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(UserDTO user)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE dbo.Users_Table  SET Name = @UserName, image_id = @UserImg, Date = @UserDate WHERE Guid = @UserID ";
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@UserImg", user.Image);
                command.Parameters.AddWithValue("@UserDate", user.DateOfBirth);
                command.Parameters.AddWithValue("@UserID", user.Id);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                using (var connection = new SqlConnection(UsersConnectionString))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO dbo.ErrorLog (Text) VALUES('Can't update user') ";
                    connection.Open();
                    summary = command.ExecuteNonQuery();
                }
                return false;
            }
        }

        bool IUserListDAL.RemoveAward(Guid UserID, Guid AwardID)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();                  
                command.CommandText = "DELETE FROM dbo.UsersAwards WHERE user_id= @UserID AND award_id = @AwardID ;";
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@AwardID", AwardID);
                connection.Open();
                summary = command.ExecuteNonQuery();

            }           
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't remove award from user");
                LogType.AddLog(e);
                return false;
            }


        }

        bool IUserListDAL.AddAward(Guid UserID, Guid AwardID)
        {
            int summary = 0;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                var tcommand = connection.CreateCommand();              
                connection.Open();               
                tcommand.CommandText = "SELECT user_id, award_id FROM dbo.UsersAwards WHERE user_id= @UserID AND award_id = @AwardID ;";
                tcommand.Parameters.AddWithValue("@UserID", UserID);
                tcommand.Parameters.AddWithValue("@AwardID", AwardID);
                using (SqlDataReader reader = tcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        summary++;

                    }
                }
                
                if (summary == 0)
                {
                    command.CommandText = "INSERT INTO dbo.UsersAwards (user_id, award_id) VALUES ( @UserID, @AwardID );";
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@AwardID", AwardID);
                    summary = 0;
                    summary = command.ExecuteNonQuery();
                }

                else return false;

            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't add award to user");
                LogType.AddLog(e);
                return false;
            }


        }
    }
}
