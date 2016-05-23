using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.DAL.DB
{
    public class DBconnectException : Exception
    {
        public DBconnectException()
        {

        }
        public DBconnectException(string message):base(message)
        {

        }

        public DBconnectException(string message, Exception inner):base(message,inner)
        {

        }
    }
}
