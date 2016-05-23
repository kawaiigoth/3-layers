using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Entites
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public List<AppUserDTO> Users { get; set; }

        public RoleDTO(string name)
        {
            Name = name;
            Users = new List<AppUserDTO>();
        }

    }
}
