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
    public class NinjaRepository
    {

        public List<Ninja> Get()
        {
            var list = new List<Ninja>();
            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Ninja.ToList();   
            }      
            return list;
        }

        public List<InventoryViewModel> GetInventory(int ninjaId)
        {
            var list = new List<InventoryViewModel>();
            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                var templist = db.Inventory.Include(i => i.Category1).Include(j => j.Gear).Include(k => k.Gear.GearStat).Include(k => k.Gear.GearStat.Select(s => s.Stat)).Where(w => w.Id_Ninja == ninjaId).OrderBy(o => o.Category1.Order);
                list = templist.ToList().Select(s => new InventoryViewModel(s)).ToList();
            }
            return list;
        }

        public bool AddOrUpdate(Ninja ninja, List<InventoryViewModel> inventory, bool isNew)
        {
            try
            {
                using (Ninja_managerEntities db = new Ninja_managerEntities())
                {
                    if (isNew) // make new model, to make it easier to insert the new data
                    {
                        var n = new Ninja { Id = ninja.Id, Gold = ninja.Gold, Name = ninja.Name };
                        db.Ninja.AddOrUpdate(n);
                    }
                    else
                    {
                        db.Ninja.AddOrUpdate(ninja);
                    }
                    foreach(var i in inventory)
                    {
                        var j = i.Inventory;
                        if (isNew)
                        {
                            var b = new Inventory { Id_Ninja = j.Id_Ninja, Category = j.Category, Id_Gear = j.Id_Gear};
                            db.Inventory.AddOrUpdate(b);
                        } 
                        else
                        {
                          db.Inventory.AddOrUpdate(j);
                        }
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
                    var pointless = db.Ninja.FirstOrDefault(i => i.Id == ninja.Id);
                    if (pointless != null)
                    {
                        var inv = db.Inventory.Where(w => w.Id_Ninja == pointless.Id);

                        foreach (var i in inv)
                            db.Inventory.Remove(i);

                        db.Ninja.Remove(pointless);
                        db.SaveChanges();
                    }
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
