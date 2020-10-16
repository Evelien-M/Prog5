using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                list = db.Ninja.Include(l => l.Inventory).Include(a => a.Inventory.Select(s => s.Category1)).ToList();   
            }      
            return list;
        }

        public bool AddOrUpdateNinja(Ninja ninja)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    db.Ninja.AddOrUpdate(ninja);
                    foreach(var i in ninja.Inventory)
                    {
                      db.Inventory.AddOrUpdate(i);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
