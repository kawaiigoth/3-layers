using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;

namespace _EPAM_UserList.DAL.Collection
{
    public class DAL : IUserListDAL
    {
        public static List<UserDTO> Users = new List<UserDTO>();

        public bool Create(UserDTO user)
        {
            Users.Add(user);
            return true;
        }

        public bool Delete(Guid id)
        {
            var temp = Users.FirstOrDefault(x => x.Id == id);
            if (temp == null)
            {
                return false;
            }
            Users.Remove(temp);
            return true;
        }

        public UserDTO Get(Guid id)
        {
            var temp = Users.FirstOrDefault(x => x.Id == id);
            return temp;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            foreach (var item in Users)
            {
                yield return item;
            }
        }

        public bool Update(UserDTO user)
        {
            var temp = Users.FirstOrDefault(x => x.Id == user.Id);
            if (temp == null)
            {
                return false;
            }
            temp.Name = user.Name;
            temp.DateOfBirth = user.DateOfBirth;
            return true;
        }

        public void Load()
        {

        }

        public void Save()
        {

        }

        public bool RemoveAward(Guid UserID, Guid AwardID)
        {
            throw new NotImplementedException();
        }

        public bool AddAward(Guid UserID, Guid AwardID)
        {
            throw new NotImplementedException();
        }
    }
}
