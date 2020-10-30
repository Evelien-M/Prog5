using Ninja_manager.ViewModel.Crud_Ninja;
using Ninja_manager.ViewModel.Shop;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

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
        public List<GearStat> GetStats(int id)
        {
            var list = new List<GearStat>();

            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.GearStat.Include(i => i.Stat).Where(w => w.Id_Gear == id).ToList();
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

        public List<GearItemViewModel> GetByCategory(string name)
        {
            var list = new List<GearItemViewModel>();

            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                var templist = db.Gear.Include(i => i.GearStat).Include(j => j.GearStat.Select(s => s.Stat)).Where(w => w.Category == name).ToList();
                list = templist.Select(s => new GearItemViewModel(s)).ToList();
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
