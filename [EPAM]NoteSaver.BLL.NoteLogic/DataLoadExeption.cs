using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.BLL.UserLogic
{
    class DataLoadExeption : Exception
    {
        public DataLoadExeption()
        {

        }
        public DataLoadExeption(string message):base(message)
        {

        }

        public DataLoadExeption(string message, Exception inner):base(message,inner)
        {

        }
    }
}
