using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.BLL
{
    public interface IUserListBLL
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(Guid id);
        bool Create(UserDTO note);
        bool Update(UserDTO note);
        bool RemoveAward(Guid UserID, Guid AwardID);
        bool AddAward(Guid UserID, Guid AwardID);
        bool Delete(Guid id);
       // void Load();
        void Save();
    }
}
