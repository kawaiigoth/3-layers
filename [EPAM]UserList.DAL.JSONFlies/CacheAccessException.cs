using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.DAL.JSONFlies
{
    class CacheAccessException : Exception
    {
        public CacheAccessException()
        {

        }
        public CacheAccessException(string message):base(message)
        {

        }

        public CacheAccessException(string message, Exception inner):base(message,inner)
        {

        }
    }
}
