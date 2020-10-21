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
                list = db.Ninja.ToList();   
            }      
            return list;
        }

        public List<Inventory> GetInventory(int ninjaId)
        {
            var list = new List<Inventory>();
            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Inventory.Include(i => i.Category1).Include(j => j.Gear).Where(w => w.Id_Ninja == ninjaId).OrderBy(o => o.Category1.Order).ToList();
            }
            return list;
        }

        public bool AddOrUpdate(Ninja ninja)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    db.Ninja.AddOrUpdate(ninja);
                    foreach(var i in ninja.Inventory)
                    {
                        if(db.Entry(i.Category1).State != EntityState.Detached)
                            db.Category.Attach(i.Category1);

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

        public bool Delete(Ninja ninja)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    for (var i = 0; i < ninja.Inventory.Count; i++)
                        db.Entry(ninja.Inventory.ToList()[i]).State = EntityState.Deleted;

                    db.Ninja.Remove(ninja);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
