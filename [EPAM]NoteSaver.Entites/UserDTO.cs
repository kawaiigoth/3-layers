using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Entites
{
    public class UserDTO
    {
        public int Image { get; set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int years = today.Year - DateOfBirth.Year;
                if (DateOfBirth > today.AddYears(-years))
                    years--;
                return years;
            }

        }
        //[JsonIgnore]
        public List<AwardDTO> Awards { get; set; }

        public override string ToString()
        {
            string format = " Имя : {0}, возраст: {1}, награжден(а): {2}";
            string awards = "";
            foreach (var item in Awards)
            {
                awards += item.Name + ", ";
            }
            string s = String.Format(format, Name, Age, awards);
            return s;
        }
        //Show Constructor
        public UserDTO(string name, DateTime dob, Guid id, int image)
        {
            Image = image;
            this.Name = name;
            Id = id;
            DateOfBirth = dob;
            Awards = new List<AwardDTO>();
        }
        //Create Constructor
        public UserDTO(string name, DateTime dob, int image = 0)
        {

            Image = image;
            this.Name = name;
            Id = Guid.NewGuid();
            DateOfBirth = dob;
            Awards = new List<AwardDTO>();
        }
    }
}
