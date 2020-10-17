using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.ViewModel.Crud_Gear
{
    public class GearEditViewModel : ViewModelBase
    {
        public string Name
        {
            get { return this._name; }
            set { this._name = value; base.RaisePropertyChanged(); }
        }
        public int Price
        {
            get { return this._price; }
            set { this._price = value; base.RaisePropertyChanged(); }
        }
        public int Strength
        {
            get { return this._strength; }
            set { this._strength = value; base.RaisePropertyChanged(); }
        }
        public int Agility
        {
            get { return this._agility; }
            set { this._agility = value; base.RaisePropertyChanged(); }
        }
        public int Intelligence
        {
            get { return this._intelligence; }
            set { this._intelligence = value; base.RaisePropertyChanged(); }
        }

        private string _name;
        private int _price;
        private int _strength;
        private int _agility;
        private int _intelligence;

        public GearEditViewModel(GearListViewModel gearList)
        {

        }
    }
}
