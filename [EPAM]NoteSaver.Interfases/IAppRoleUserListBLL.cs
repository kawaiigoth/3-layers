using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases
{
    public interface IAppRoleUserListBLL
    {
        IEnumerable<RoleDTO> GetAll();
        RoleDTO Get(string name);
    }
}
