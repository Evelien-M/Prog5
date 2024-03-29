﻿using Ninja_manager.ViewModel.Crud_Ninja;
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
    }
}
