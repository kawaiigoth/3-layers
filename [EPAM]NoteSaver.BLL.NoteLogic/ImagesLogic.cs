using _EPAM_UserList.Interfases.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;
using _EPAM_UserList.Interfases.DAL;

namespace _EPAM_UserList.BLL.UserLogic
{
    public class ImagesLogic : IImagesBLL
    {
        private IImagesDAL DAL = new DAL.DB.ImagesDAL();

        public bool Create(ImageDTO img)
        {
            try
            {
                return DAL.Create(img);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IEnumerable<ImageDTO> GetAll()
        {
            try
            {
                return DAL.GetAll();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ImageDTO GetById(int id)
        {
            try
            {
                return DAL.GetById(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int GetID(Guid name)
        {
            try
            {
                return DAL.GetID(name);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool Update(ImageDTO img)
        {
            try
            {
                return DAL.Update(img);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
