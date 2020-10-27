using Ninja_manager.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.ViewModel.Shop
{
    public class GearItemViewModel
    {
        public Gear Gear { get; private set; }
        public string ImageLink { get; private set; }
        public GearItemViewModel(Gear gear)
        {
            this.Gear = gear;
            
            if (gear != null && gear.Image != null)
                this.ImageLink = Path.Combine(GearImageManagement.SourceFolder, gear.Image);
        }
    }
}
