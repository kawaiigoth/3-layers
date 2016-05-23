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

namespace _EPAM_UserList.BLL.UserListLogic
{
    public class UserListLogic : IUserListBLL
    {
        private IUserListDAL DAL;
        public UserListLogic()
        {
            string mode;

            try
            {
                mode = ConfigurationManager.AppSettings["DataMode"];
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new ConfigurationFileExeption("Configuration file error.", e);
            }

            switch (mode)
            {
                case "JSONFiles":
                    {
                        DAL = new _EPAM_UserList.DAL.JSONFiles.DAL();
                    }
                    break;
                case "Collection":
                    {
                        DAL = new _EPAM_UserList.DAL.Collection.DAL();
                    }
                    break;
                case "DB":
                    {
                        DAL = new _EPAM_UserList.DAL.DB.UserDAL();
                    }
                    break;
            }
           

        }

        public bool Create(UserDTO user)
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

        public bool RemoveAward(Guid UserID, Guid AwardID)
        {
            try
            {
                return DAL.RemoveAward(UserID,AwardID);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }

        }

        public bool AddAward(Guid UserID, Guid AwardID)
        {
            try
            {
                return DAL.AddAward(UserID, AwardID);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }

        }

        public bool Delete(Guid id)
        {
            try
            {
                return DAL.Delete(id);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }

        }

        public UserDTO Get(Guid id)
        {
            try
            {
                return DAL.Get(id);
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }

        }

        public IEnumerable<UserDTO> GetAll()
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

        //public IEnumerable<UserDTO> GetAllByCreteria(Func<UserDTO,bool> crit)
        //{
        //    return DAL.GetAll().Where(crit).ToArray();
        //}

        //public IEnumerable<UserDTO> GetAllHigherThan(int y)
        //{
        //    return DAL.GetAll()
        //        .Where(x => x.Age >= y).ToArray();
        //}

        //public IEnumerable<UserDTO> GetAllSortByName()
        //{
        //    return DAL.GetAll().OrderBy(x => x.Name);
        //}

        public bool Update(UserDTO user)
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

        //public void Load()
        //{
        //    DAL.Load();
        //}

        public void Save()
        {
            try
            {
                DAL.Save();
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw e;
            }

        }
    }
}
