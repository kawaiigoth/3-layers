using _EPAM_UserList.Interfases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_UserList.Entites;
using Newtonsoft.Json;
using System.IO;
using _EPAM_UserList.DAL.JSONFlies;
using Logs;

namespace _EPAM_UserList.DAL.JSONFiles
{
    public class DAL : IUserListDAL
    {

        public DAL()
        {
            Users = new List<UserDTO>();
            Load();
        }
        public static List<UserDTO> Users;

        public bool Create(UserDTO user)
        {
            try
            {
                Users.Add(user);
                return true;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't save data to cache!", e);
                
            }

        }

        public bool Delete(Guid id)
        {
            try
            {
                var temp = Users.FirstOrDefault(x => x.Id == id);
                if (temp == null)
                {
                    return false;
                }
                Users.Remove(temp);
                return true;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't save data to cache!", e);
                
            }

        }

        public UserDTO Get(Guid id)
        {
            try
            {
                var temp = Users.FirstOrDefault(x => x.Id == id);
                return temp;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't save data to cache!", e);
                
            }

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
            try
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
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't save data to cache!", e);
                
            }

        }

        public void Save()
        {
            string json;
            try
            {
                json = JsonConvert.SerializeObject(Users,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
                );
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new JsonException("Can't save to json file", e);
                
            }
            
            using (StreamWriter writer = new StreamWriter("users.json"))
            {
                writer.Write(json);
                writer.Flush();
                writer.Close();
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists("users.json"))
                    Users = JsonConvert.DeserializeObject<List<UserDTO>>(File.ReadAllText("users.json"));
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new JsonException("Can't load to json", e);
                
            }
           
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
