using Ninja_manager.ViewModel.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.ViewModel.Crud_Ninja
{
    public class InventoryViewModel
    {
        public Inventory Inventory { get; private set; }
        public GearItemViewModel GearItemViewModel { get; private set; }
        public InventoryViewModel(Inventory inventory)
        {
            this.Inventory = inventory;
            this.GearItemViewModel = new GearItemViewModel(inventory.Gear);
        }
    }
}
