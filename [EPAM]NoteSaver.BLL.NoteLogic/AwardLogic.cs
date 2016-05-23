using _EPAM_UserList.Entites;
using _EPAM_UserList.Interfases.BLL;
using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.BLL.UserLogic
{
    public class AwardLogic : IAwardListBLL
    {
        private IAwardListDAL DAL;
        public AwardLogic()
        {
            string mode;
           
            try
            {
                mode = ConfigurationManager.AppSettings["DataMode"];
            }
            catch (Exception e)
            {
                throw new ConfigurationFileExeption("Configuration file error.", e);
            }

            switch (mode)
            {                
                case "JSONFiles":
                    {
                        DAL = new DAL.JSONFiles.AwardDAL();
                    }
                    break;
                case "DB":
                    {
                        DAL = new _EPAM_UserList.DAL.DB.AwardDAL();
                    }
                    break;
            }

        }

        public bool Exists(string name)
        {
            try
            {
                return DAL.Exists(name);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public bool Create(AwardDTO user)
        {
            try
            {
                return DAL.Create(user);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //public int Count()
        //{
        //    try
        //    {
        //        return DAL.Count();
        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }

        //}

        public bool Delete(Guid id)
        {
            try
            {
                return DAL.Delete(id);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public AwardDTO Get(Guid id)
        {
            try
            {
                return DAL.Get(id);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public IEnumerable<AwardDTO> GetAll()
        {
            return DAL.GetAll().ToArray();
        }

        //public IEnumerable<AwardDTO> GetAllByCreteria(Func<AwardDTO, bool> crit)
        //{
        //    return DAL.GetAll().Where(crit).ToArray();
        //}
        
        //public IEnumerable<AwardDTO> GetAllSortByName()
        //{
        //    return DAL.GetAll().OrderBy(x => x.Name);
        //}

        public bool Update(AwardDTO user)
        {
            try
            {
                return DAL.Update(user);
            }
            catch (Exception e)
            {

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

                throw e;
            }

        }
    }
}
