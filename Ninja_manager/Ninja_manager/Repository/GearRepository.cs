using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.Repository
{
    public class GearRepository
    {

        public List<Gear> Get()
        {
            var list = new List<Gear>();

            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Gear.ToList();
            }

            return list;
        }

        public bool AddOrUpdate(Gear gear)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    db.Gear.AddOrUpdate(gear);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Gear> GetByCategory(string name)
        {
            var list = new List<Gear>();

            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Gear.Where(w => w.Category == name).ToList();
            }

            return list;
        }

        public bool Delete(Gear gear)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    var used = db.Inventory.Where(w => w.Gear.Id == gear.Id);
                    foreach(var i in used)
                    {
                        // refund the money
                        var ninja = i.Ninja;
                        ninja.Gold += i.Gear.Price;
                        db.Ninja.AddOrUpdate(ninja);

                        i.Id_Gear = null;
                        i.Gear = null;
                        db.Inventory.AddOrUpdate(i);
                    }
                    var g = db.Gear.FirstOrDefault(i => i.Id == gear.Id);
                    db.Gear.Remove(g);
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
