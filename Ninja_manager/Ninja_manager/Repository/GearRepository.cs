using Ninja_manager.Helper;
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
                var allStats = db.Stat;
                foreach (var i in allStats)
                    list.Add(new GearStat { Id_Gear = id, Stat = i, Stat_Name = i.Name });
          
                var currentStats = db.GearStat.Include(i => i.Stat).Where(w => w.Id_Gear == id).ToList();
               
                // left joins the stats table into the gearstats table, so that the gear stats can gets the 0 values for each stat
                foreach (var i in currentStats)
                    list.Update(i);     
            }

            return list;
        }

        public bool AddOrUpdate(Gear gear, List<GearStat> stats, bool isNew)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    if (isNew) // make new model, to make it easier to insert the new data
                    {
                        var g = new Gear { Id = gear.Id, Name = gear.Name, Image = gear.Image, Price = gear.Price, Category = gear.Category };
                        db.Gear.AddOrUpdate(g);
                    }
                    else
                    {
                        db.Gear.AddOrUpdate(gear);
                    }
                    foreach (var i in stats)
                    {
                        if (i.Amount == 0)
                        {
                            // removes the 0 values from stat, so it won't be displayed
                            var remove = db.GearStat.FirstOrDefault(f => f.Id_Gear == gear.Id && f.Stat_Name == i.Stat_Name);
                            if (remove != null)
                                db.GearStat.Remove(remove);
                            
                            continue;
                        }

                        if (isNew)
                        {
                            var s = new GearStat { Id_Gear = i.Id_Gear, Stat_Name = i.Stat_Name, Amount = i.Amount };
                            db.GearStat.AddOrUpdate(s);
                        }
                        else
                        {
                            db.GearStat.AddOrUpdate(i);
                        }
                    }
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
                    // makes list of every ninja who has bought the gear
                    var used = db.Inventory.Where(w => w.Gear.Id == gear.Id);
                    foreach(var i in used)
                    {
                        // refund the money
                        var ninja = i.Ninja;
                        ninja.Gold += i.Gear.Price;
                        db.Ninja.AddOrUpdate(ninja);

                        // removes the gear from the inventory
                        i.Id_Gear = null;
                        i.Gear = null;
                        db.Inventory.AddOrUpdate(i);
                    }
                    var g = db.Gear.FirstOrDefault(i => i.Id == gear.Id);
                    var stats = db.GearStat.Where(w => w.Id_Gear == gear.Id);
                    
                    foreach(var s in stats)
                        db.GearStat.Remove(s);

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
