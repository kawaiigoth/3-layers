using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Logs
{
    public class LogType
    {
        static string log_file;
        static LogType()
        {

            try
            {
                log_file = ConfigurationManager.AppSettings["log_location"];
            }
            catch (Exception)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    w.WriteLine("  :");
                    w.WriteLine("Config Error!");
                    w.WriteLine("-------------------------------");
                    log_file = "log.txt";
                }
            }
        }

        public static void AddLogTxt(Exception e)
        {


            using (StreamWriter w = File.AppendText(log_file))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", e.Message);
                w.WriteLine("-------------------------------");
            }

        }
        public static void AddLog(Exception e)
        {
            int result;
            string UsersConnectionString = ConfigurationManager.ConnectionStrings["UsersTableDB"].ConnectionString;
            using (var connection = new System.Data.SqlClient.SqlConnection(UsersConnectionString))
            {
                
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO dbo.ErrorLog (Text) VALUES(@Text) ";
                command.Parameters.AddWithValue("@Text", e.Message);
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            if (result == 0)
            {
                AddLogTxt(e);
            }
        }

    }
}
