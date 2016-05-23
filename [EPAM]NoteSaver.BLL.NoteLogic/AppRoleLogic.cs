using _EPAM_UserList.Interfases.BLL;
using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;
using System.Configuration;
using _EPAM_UserList.BLL.UserLogic;
using Logs;
using _EPAM_UserList.Interfases;

namespace _EPAM_UserList.BLL.UserLogic
{
    public class AppRoleLogic : IAppRoleUserListBLL
    {

        private IAppRoleUserListDAL DAL;

        public AppRoleLogic()
        {
             DAL = new DAL.DB.AppRoleDAL();
        }

        public RoleDTO Get(string name)
        {
            try
            {
                return DAL.Get(name);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            try
            {
                return DAL.GetAll().ToArray();
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }
    }
}
