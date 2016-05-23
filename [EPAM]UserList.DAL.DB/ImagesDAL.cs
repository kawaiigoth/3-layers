using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;
using System.Data.SqlClient;
using Logs;
using System.Data;

namespace _EPAM_UserList.DAL.DB
{
    public class ImagesDAL : IImagesDAL
    {
        string UsersConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;

        public bool Create(ImageDTO img)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.Images (Name, SmallImage, BigImage, Format ) VALUES(@imgName, @imgSData, @imgBData, @imgFormat)";
                command.Parameters.AddWithValue("@imgName", img.Name);
                var small = img.DataSmall == null ? (object)DBNull.Value : img.DataSmall;
                var big = img.DataBig == null ? (object)DBNull.Value : img.DataBig;
                var format = img.Format == null ? (object)DBNull.Value : img.Format;
                command.Parameters.Add("@imgSData", SqlDbType.VarBinary, -1).Value = small;
                command.Parameters.Add("@imgBData", SqlDbType.VarBinary, -1).Value = big;
                command.Parameters.Add("@imgFormat", SqlDbType.VarChar, 50).Value = format;
                //command.Parameters.AddWithValue("@imgSData", img.DataSmall==null ? (object)DBNull.Value : img.DataSmall);
                //command.Parameters.AddWithValue("@imgBData", img.DataBig == null ? (object)DBNull.Value : img.DataBig);
                //command.Parameters.AddWithValue("@imgFormat", img.Format == null ? (object)DBNull.Value : img.Format);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't insert image");
                LogType.AddLog(e);
                return false;
            }
        }

        public IEnumerable<ImageDTO> GetAll()
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Id, Name, SmallImage, BigImage, Format FROM dbo.Images ";
                connection.Open();
                ImageDTO image = null;
                List<ImageDTO> Images = new List<ImageDTO>();
                Guid name;
                byte[] small;
                byte[] big;
                int id;
                string format;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (int)reader["ID"];
                        name = (Guid)reader["Name"];
                        small = reader["SmallImage"] as byte[];
                        big = reader["BigImage"] as byte[];
                        format = reader["Format"] as string;
                        image = new ImageDTO(id, name, format, small, big);
                        Images.Add(image);                    
                    }
                }

                return Images;
            }
        }

        public ImageDTO GetById(int id)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Id, Name, SmallImage, BigImage, Format FROM dbo.Images WHERE Id='"+id+"' ";
                connection.Open();
                ImageDTO image = null;
                Guid name;
                byte[] small;
                byte[] big;
                string format;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (int)reader["ID"];
                        name = (Guid)reader["Name"];
                        small = reader["SmallImage"] as byte[];
                        big = reader["BigImage"] as byte[];
                        format = reader["Format"] as string;
                        image = new ImageDTO(id, name, format, small, big);
                    }
                }

                return image;
            }
        }

        public int GetID(Guid name)
        {
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var fcommand = connection.CreateCommand();
                var scommand = connection.CreateCommand();
                fcommand.CommandText = "SELECT Id FROM dbo.Images WHERE Name='" + name + "' ";
                connection.Open();
                int id = 0;
                using (SqlDataReader reader = fcommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (int)reader["ID"];
                    }
                }
                return id;
            }
        }

        public bool Update(ImageDTO img)
        {
            int summary;
            using (var connection = new SqlConnection(UsersConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE dbo.Images  SET SmallImage = @imgSData, BigImage = @imgBData Format = @imgFormat WHERE Id = @imgId; ";
                var small = img.DataSmall == null ? (object)DBNull.Value : img.DataSmall;
                var big = img.DataBig == null ? (object)DBNull.Value : img.DataBig;
                var format = img.Format == null ? (object)DBNull.Value : img.Format;
                command.Parameters.Add("@imgSData", SqlDbType.VarBinary, -1).Value = small;
                command.Parameters.Add("@imgBData", SqlDbType.VarBinary, -1).Value = big;
                command.Parameters.Add("@imgFormat", SqlDbType.VarChar, 50).Value = format;
                command.Parameters.AddWithValue("@imgId", img.Id);
                connection.Open();
                summary = command.ExecuteNonQuery();
            }
            if (summary > 0)
                return true;
            else
            {
                DBconnectException e = new DBconnectException("Can't change image");
                LogType.AddLog(e);
                return false;
            }
        }
    }
}
