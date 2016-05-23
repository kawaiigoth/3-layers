using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Entites
{
    public class AwardDTO
    {
        public int Image { get; set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }

        

        public override string ToString()
        {
            string format = " {0} ";
            string s = String.Format(format, Name);
            return s;
        }
        //[JsonIgnore]
        public List<UserDTO> Users { get; set; }
        public AwardDTO(string name, int image = 0)
        {
            Image = image;
            this.Name = name;
            Id = Guid.NewGuid();
            Users = new List<UserDTO>();
        }

        public AwardDTO(string name, Guid id, int image)
        {
          
            Image = image;
            this.Name = name;
            Id = id;
            Users = new List<UserDTO>();
        }
    }
}
