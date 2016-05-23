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
    public class AppUserLogic : IAppUserListBLL
    {
        private IAppUserListDAL DAL;

        public AppUserLogic()
        {
            DAL =  new DAL.DB.AppUserDAL();
        }

        public bool AddRole(AppUserDTO user, RoleDTO role)
        {
            try
            {
                return DAL.AddRole(user,role);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public bool Create(AppUserDTO user)
        {
            try
            {
                return DAL.Create(user);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public bool Delete(string name)
        {
            try
            {
                return DAL.Delete(name);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public bool Exists(string username)
        {
            var user = DAL.Get(username);
            if(user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public AppUserDTO Get(string name)
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

        public IEnumerable<AppUserDTO> GetAll()
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

        public bool IsInRole(AppUserDTO user, RoleDTO role)
        {
            try
            {
                return DAL.IsInRole(user,role);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public bool RemoveRole(AppUserDTO user, RoleDTO role)
        {
            try
            {
                return DAL.RemoveRole(user, role);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }

        public bool Update(AppUserDTO user)
        {
            try
            {
                return DAL.Update(user);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }
        }
    }
}
