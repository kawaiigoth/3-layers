using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.BLL
{
    public class ConfigurationFileExeption : Exception
    {
        public ConfigurationFileExeption()
        {

        }
        public ConfigurationFileExeption(string message):base(message)
        {

        }

        public ConfigurationFileExeption(string message, Exception inner):base(message,inner)
        {

        }
    }
}
