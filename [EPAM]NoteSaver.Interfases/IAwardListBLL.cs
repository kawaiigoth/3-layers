using _EPAM_UserList.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _EPAM_UserList.Interfases.BLL
{
    public interface IAwardListBLL
    {
        //IEnumerable<AwardDTO> GetAllSortByName();
        //IEnumerable<AwardDTO> GetAllByCreteria(Func<AwardDTO, bool> crit);        
        IEnumerable<AwardDTO> GetAll();
        AwardDTO Get(Guid id);
        bool Create(AwardDTO note);
        bool Update(AwardDTO note);
        bool Delete(Guid id);
        bool Exists(string name);
        //void Load();
        //int Count();
        void Save();
    }
}
