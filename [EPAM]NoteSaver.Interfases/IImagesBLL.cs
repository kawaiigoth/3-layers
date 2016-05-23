using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.BLL
{
    public interface IImagesBLL
    {
        IEnumerable<ImageDTO> GetAll();
        ImageDTO GetById(int id);
        int GetID(Guid name);
        bool Create(ImageDTO img);
        bool Update(ImageDTO img);
    }
}
