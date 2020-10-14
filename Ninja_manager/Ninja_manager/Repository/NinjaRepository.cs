using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.Repository
{
    public class NinjaRepository
    {

        public List<Ninja> GetNinjas()
        {
            var list = new List<Ninja>();
            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Ninja.ToList();
                var a = db.Category.ToList();
                
            }
            
            return list;
        }
    }
}
