using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.DAL
{
    public interface IUserListDAL
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(Guid id);
        bool Create(UserDTO user);
        bool RemoveAward(Guid UserID, Guid AwardID);
        bool AddAward(Guid UserID, Guid AwardID);
        bool Update(UserDTO user);
        bool Delete(Guid id);
        //void Load();
        void Save();
    }
}
