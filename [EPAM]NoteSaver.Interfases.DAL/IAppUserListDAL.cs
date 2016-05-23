using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.DAL
{
    public interface IAppUserListDAL
    {
        IEnumerable<AppUserDTO> GetAll();
        AppUserDTO Get(string name);
        bool Create(AppUserDTO user);
        bool Update(AppUserDTO user);
        bool Delete(string name);
        bool IsInRole(AppUserDTO user, RoleDTO role);
        bool AddRole(AppUserDTO user, RoleDTO role);
        bool RemoveRole(AppUserDTO user, RoleDTO role);
    }
}
