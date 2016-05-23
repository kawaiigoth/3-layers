using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.DAL
{
    public interface IAwardListDAL
    {
        IEnumerable<AwardDTO> GetAll();
        AwardDTO Get(Guid id);
        bool Create(AwardDTO note);
        bool Update(AwardDTO note);
        bool Delete(Guid id);
        //int Count();
        //void Load();
        void Save();
        bool Exists(string name);
    }
}
