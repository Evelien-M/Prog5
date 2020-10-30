using Ninja_manager.ViewModel.Crud_Ninja;
using Ninja_manager.ViewModel.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ninja_manager.Helper
{
    static class ObservableExtension
    {
        public static void Update(this ObservableCollection<Ninja> observer, Ninja ninja)
        {
            var item = observer.FirstOrDefault(i => i.Id == ninja.Id);
            int j = observer.IndexOf(item);
            observer[j] = ninja;
            CollectionViewSource.GetDefaultView(observer).Refresh();
        }
        public static void Update(this ObservableCollection<Gear> observer, Gear gear)
        {
            var item = observer.FirstOrDefault(i => i.Id == gear.Id);
            int j = observer.IndexOf(item);
            observer[j] = gear;
            CollectionViewSource.GetDefaultView(observer).Refresh();
        }
        public static void Update(this ObservableCollection<InventoryViewModel> observer, string category,  Gear gear)
        {
            var item = observer.FirstOrDefault(i => i.Inventory.Category == category);
            int j = observer.IndexOf(item);
            observer[j].Inventory.Gear = gear;
            observer[j].Inventory.Id_Gear = gear != null ? gear.Id : (int?)null;
            observer[j] = new InventoryViewModel(observer[j].Inventory);
            CollectionViewSource.GetDefaultView(observer).Refresh();
        }

        public static void AddOrUpdate(this List<StatViewModel> list, GearStat stat)
        {
            var item = list.FirstOrDefault(i => i.Name == stat.Stat_Name);
            if (item == null)
            {
                var newitem = new StatViewModel { Name = stat.Stat_Name, Amount = stat.Amount, Colour = stat.Stat.Colour };
                list.Add(newitem);
            }   
            else
            {
                int j = list.IndexOf(item);
                list[j].Amount += stat.Amount;
            }
        }
        public static void Update(this List<GearStat> list, GearStat stat)
        {
            var item = list.FirstOrDefault(i => i.Stat_Name == stat.Stat_Name);
            if (item != null)
            {
                int j = list.IndexOf(item);
                list[j].Amount += stat.Amount;
            }
        }
    }
}
