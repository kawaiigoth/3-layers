using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace _EPAM_UserList.Entites
{
    public class AppUserDTO
    {
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public List<RoleDTO> Roles { get; set; }        

        public AppUserDTO(string name, byte[] password)
        {
            Name = name;
            Password = password;
            Roles = new List<RoleDTO>();
        }
    }
}
