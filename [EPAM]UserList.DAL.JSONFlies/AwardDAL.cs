using _EPAM_UserList.DAL.JSONFlies;
using _EPAM_UserList.Entites;
using _EPAM_UserList.Interfases.DAL;
using Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.DAL.JSONFiles
{
    public class AwardDAL : IAwardListDAL
    {
        public AwardDAL()
        {
            Awards = new List<AwardDTO>();
            Load();
        }

        public static List<AwardDTO> Awards;

        public bool Create(AwardDTO award)
        {
            try
            {
                Awards.Add(award);
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
                var temp = Awards.FirstOrDefault(x => x.Id == id);
                if (temp == null)
                {
                    return false;
                }
                Awards.Remove(temp);
                return true;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't delete data from cache!", e);
            }
            
        }

        public bool Exists(string name)
        {
            try
            {
                foreach (var item in Awards)
                {
                    if (item.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't access to cache!", e);
            }
            
        }

        public AwardDTO Get(Guid id)
        {
            try
            {
                var temp = Awards.FirstOrDefault(x => x.Id == id);
                return temp;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't access to cache!", e);
            }

        }

        public IEnumerable<AwardDTO> GetAll()
        {           
                foreach (var item in Awards)
                {
                    yield return item;
                }         
        }

        public int Count()
        {
            try
            {
                return Awards.Count();
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't access to cache!", e);
            }
        }

        public bool Update(AwardDTO award)
        {
            try
            {
                var temp = Awards.FirstOrDefault(x => x.Id == award.Id);
                if (temp == null)
                {
                    return false;
                }
                temp.Name = award.Name;
                return true;
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new CacheAccessException("Can't access to cache!", e);
            }

        }

        public void Save()
        {
            string json;
            try
            {
                json = JsonConvert.SerializeObject(Awards,
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
           
            using (StreamWriter writer = new StreamWriter("awards.json"))
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
                if (File.Exists("awards.json"))
                    Awards = JsonConvert.DeserializeObject<List<AwardDTO>>(File.ReadAllText("awards.json"));
            }
            catch (Exception e)
            {
                LogType.AddLog(e);
                throw new JsonException("Can't load json file.",e);
            }
            
        }
    }
}
