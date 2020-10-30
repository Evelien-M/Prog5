using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Shop;
using Ninja_manager.ViewModel.Other;
using Ninja_manager.ViewModel.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Crud_Ninja
{
    public class NinjaEditViewModel : ViewModelBase
    {

        public ICommand SaveNinjaCommand { get; set; }
        public ICommand ResetNinjaCommand { get; set; }
        public ICommand ShopNinjaGearCommand { get; set; }
        public ICommand SellItemCommand { get; set; }
        public string Name
        {
            get { return this._name; }
            set { this._name = value; base.RaisePropertyChanged(); this.CanExecuteSaveNinja(); }
        }
        public int Gold
        {
            get { return this._gold; }
            set { this._gold = value; base.RaisePropertyChanged(); }
        }
        public string SuccesMessage
        {
            get { return this._succesMessage; }
            set { this._succesMessage = value; base.RaisePropertyChanged(); }
        }
        public string ErrorMessage
        {
            get { return this._errorMessage; }
            set { this._errorMessage = value; base.RaisePropertyChanged(); }
        }
        public bool CanExecuteSave
        {
            get { return this._canExecuteSave; }
            set { this._canExecuteSave = value; base.RaisePropertyChanged(); }
        }
        public List<StatViewModel> TotalStat
        {
            get { return this._totalStat; }
            set { this._totalStat = value; base.RaisePropertyChanged(); }
        }

        public ObservableCollection<InventoryViewModel> InventoryList { get; private set; }
        public ShopViewModel ShopViewModel { get; set; }
        private NinjaListViewModel _ninjaList;
        private bool _canExecuteSave;
        private string _name;
        private int _gold;
        private string _succesMessage;
        private string _errorMessage;
        private NinjaRepository _ninjaRepository;
        private Ninja _ninja;
        private bool _isNew = false;
        private ShopView _shopView;
        private List<StatViewModel> _totalStat;

        public NinjaEditViewModel(NinjaListViewModel ninjaList)
        {
            this._ninjaList = ninjaList;
            this._ninja = ninjaList.SelectedNinja;

            this._ninjaRepository = new NinjaRepository();

            this._isNew = this._ninja.Name.Length == 0;
            this.InventoryList = new ObservableCollection<InventoryViewModel>();

            this.Reset();

            this.SaveNinjaCommand = new RelayCommand(Save);
            this.ResetNinjaCommand = new RelayCommand(Reset);
            this.ShopNinjaGearCommand = new RelayCommand(ShopNinjaGear);
            this.SellItemCommand = new RelayCommand<GearItemViewModel>(RemoveFromInventory);
        }

  
        private void Save()
        {
            this._ninja.Name = this.Name;
            this._ninja.Gold = this.Gold;

            if (this._ninjaRepository.AddOrUpdate(this._ninja, this.InventoryList.ToList(), this._isNew))
            {
                if (this._isNew)
                {
                    this._ninjaList.NinjaList.Add(this._ninja);
                    this._isNew = false;
                    this.SuccesMessage = "Ninja " + this.Name + " succesfully added!";
                }
                else
                {
                    this._ninjaList.NinjaList.Update(this._ninja);
                    this.SuccesMessage = "Ninja " + this.Name + " succesfully updated!";
                }
            }
            else
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "An error occured with the database!";
            }
        }

        private void Reset()
        {
            this.InventoryList.Clear();
            IEnumerable<InventoryViewModel> list;
            if (this._isNew)
                list = this._ninja.Inventory.Select(s => new InventoryViewModel(s));
            else
                list = this._ninjaRepository.GetInventory(this._ninja.Id);

            foreach (var i in list)
                this.InventoryList.Add(i);

            this.Name = this._ninja.Name;
            this.Gold = this._ninja.Gold;
            this.CalcStats();

            if (this.ShopViewModel != null)
                this.ShopViewModel.Update(this);
        }

        private bool CanExecuteSaveNinja()
        {
            if (this.Name.Length > 3)
            {
                this.ErrorMessage = "";
                this.CanExecuteSave = true;
                return true;
            }

            this.SuccesMessage = "";
            this.ErrorMessage = "Name has to be more than 3 characters!";
            this.CanExecuteSave = false;
            return false;
        }

        private void ShopNinjaGear()
        {
            if (this._shopView != null)
                this._shopView.Close();

            this._shopView = new ShopView();
            this._shopView.Show();
        }


        public void AddToInventory(Gear gear)
        {
            this.Gold -= gear.Price;
            this.InventoryList.Update(gear.Category, gear);
            this.CalcStats();

            if (this.ShopViewModel != null)
                this.ShopViewModel.Update(this);
        }

        public void RemoveFromInventory(GearItemViewModel gear)
        {
            if (gear != null)
            {
                var i = gear.Gear;
                this.Gold += i.Price;
                this.InventoryList.Update(i.Category, null);
                this.CalcStats();

                if (this.ShopViewModel != null)
                    this.ShopViewModel.Update(this);
            }
        }

        private void CalcStats()
        {
            List<StatViewModel> totalStat = new List<StatViewModel>();

            foreach(var inv in this.InventoryList)
            {
                var j = inv.Inventory;
                if (j.Gear != null)
                {
                    var stats = j.Gear.GearStat;
                    
                    if (stats != null)
                    {
                        foreach(var s in stats)
                        {
                            totalStat.AddOrUpdate(s);
                        }
                    }
                }
            }
            this.TotalStat = totalStat;
        }
    }

      
}
