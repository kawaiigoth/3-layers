using _EPAM_UserList.Entites;
using _EPAM_UserList.Interfases.DAL;
using Logs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
//KARTINKI GETOM KAK V PRIMERE!  OLLO NA GITHUB VSE!
namespace _EPAM_UserList.DAL.DB
{
    public class AwardDAL : IAwardListDAL
    {
        string UsersConnectionString = ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;

        public bool Create(AwardDTO award)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.Awards_Table (Guid, Name, image_id) VALUES(@awardId, @awardName, @awardImage)";
                command.Parameters.AddWithValue("@awardId", award.Id);
                command.Parameters.AddWithValue("@awardName", award.Name);
                command.Parameters.AddWithValue("@awardImage", award.Image);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't create award-model");
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
                command.CommandText = "DELETE FROM dbo.Awards_Table WHERE Guid=@id;";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't delete award-model");
                LogType.AddLog(e);
                return false;
            }

        }

        

        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        public AwardDTO Get(Guid id)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Guid, Name, image_id FROM dbo.Awards_Table WHERE Guid=@id ;";
                fcommand.Parameters.AddWithValue("@id", id);
                connection.Open();
                AwardDTO award = null;
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
                        award = new AwardDTO(name, guid, image);
                        scommand.CommandText = "SELECT [Guid], [Name], [image_id], [Date] FROM dbo.Users_Table u, dbo.UsersAwards a WHERE a.user_id = u.Guid AND a.award_id=@guid";
                        scommand.Parameters.AddWithValue("@guid", guid);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                guid = (Guid)new_reader["Guid"];
                                name = (string)new_reader["Name"];
                                image = (int)new_reader["image_id"];
                                sdate = (DateTime)new_reader["Date"];
                                award.Users.Add(new UserDTO(name, sdate, guid, image));

                            }
                        }
                    }
                }

                return award;
            }

        }

        public IEnumerable<AwardDTO> GetAll()
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                List<AwardDTO> Awards = new List<AwardDTO>();
                fcommand.CommandText = "SELECT Guid, Name, image_id FROM dbo.Awards_Table";
                connection.Open();
                AwardDTO award = null;
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
                        award = new AwardDTO(name, guid, image);
                        scommand.CommandText = "SELECT [Guid], [Name], [image_id], [Date] FROM dbo.Users_Table u, dbo.UsersAwards a WHERE a.user_id = u.Guid AND a.award_id=@guid";
                        scommand.Parameters.AddWithValue("@guid", guid);
                        using (SqlDataReader new_reader = scommand.ExecuteReader())
                        {
                            while (new_reader.Read())
                            {
                                guid = (Guid)new_reader["Guid"];
                                name = (string)new_reader["Name"];
                                image = (int)new_reader["image_id"];
                                sdate = (DateTime)new_reader["Date"];
                                award.Users.Add(new UserDTO(name, sdate, guid, image));

                            }
                        }
                        Awards.Add(award);
                        scommand.Parameters.Clear();
                    }
                }

                return Awards;
            }
        }

        public int Count()
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Guid FROM dbo.Awards_Table";
                connection.Open();
                int count = 0;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public bool Update(AwardDTO award)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE dbo.Awards_Table  SET Name = @awardName, image_id = @awardImage WHERE Guid = @awardId; ";
                command.Parameters.AddWithValue("@awardName", award.Name);
                command.Parameters.AddWithValue("@awardImage", award.Image);
                command.Parameters.AddWithValue("@awardId", award.Id);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't update award-model");
                LogType.AddLog(e);
                return false;
            }

        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
