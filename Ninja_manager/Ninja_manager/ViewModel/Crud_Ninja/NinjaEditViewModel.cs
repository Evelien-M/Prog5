using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Shop;
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
        public int TotalStrength
        {
            get { return this._totalStrength; }
            set { this._totalStrength = value; base.RaisePropertyChanged(); }
        }
        public int TotalAgility
        {
            get { return this._totalAgility; }
            set { this._totalAgility = value; base.RaisePropertyChanged(); }
        }
        public int TotalIntelligence
        {
            get { return this._totalIntelligence; }
            set { this._totalIntelligence = value; base.RaisePropertyChanged(); }
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
        private int _totalStrength;
        private int _totalAgility;
        private int _totalIntelligence;

        public NinjaEditViewModel(NinjaListViewModel ninjaList)
        {
            this._ninjaList = ninjaList;
            this._ninja = ninjaList.SelectedNinja;

            this._ninjaRepository = new NinjaRepository();

            this.InitInventory();
            this.CalcStats();

            this.SaveNinjaCommand = new RelayCommand(SaveNinja);
            this.ResetNinjaCommand = new RelayCommand(ResetNinja);
            this.ShopNinjaGearCommand = new RelayCommand(ShopNinjaGear);
            this.SellItemCommand = new RelayCommand<GearItemViewModel>(RemoveFromInventory);

            this.Name = this._ninja.Name;
            this.Gold = this._ninja.Gold;
        }

        private void InitInventory()
        {

            if (this._ninja.Name.Length == 0)
            {
                this._isNew = true;
                var list = this._ninja.Inventory.Select(s => new InventoryViewModel(s));
                this.InventoryList = new ObservableCollection<InventoryViewModel>(list);
            }
            else
            {
                var list = this._ninjaRepository.GetInventory(this._ninja.Id);
                this.InventoryList = new ObservableCollection<InventoryViewModel>(list);
            }
        }

        private void SaveNinja()
        {
            this._ninja.Name = this.Name;
            this._ninja.Gold = this.Gold;

            if (this._ninjaRepository.AddOrUpdate(this._ninja, this.InventoryList.ToList()))
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
                    this.SuccesMessage = "Ninja " + this.Name + " succesfully updatet!";
                }
            }
            else
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "An error occured with the database!";
            }
        }

        private void ResetNinja()
        {
            this.InventoryList.Clear();
            
            if (!this._isNew)
            {
                var list = this._ninjaRepository.GetInventory(this._ninja.Id);
                foreach (var i in list)
                    this.InventoryList.Add(i);
            }
            else
            {
                var repo = new CategoryRepository();
                var categories = repo.GetCategories();
                foreach (var cat in categories)
                {
                    var inv = new Inventory() { Id_Ninja = this._ninja.Id, Category = cat.Name, Category1 = cat };
                    var model = new InventoryViewModel(inv);
                    this.InventoryList.Add(model);
                }
            }

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
            int s = 0;
            int a = 0;
            int i = 0;

            foreach(var inv in this.InventoryList)
            {
                var j = inv.Inventory;
                if (j.Gear != null)
                {
                    s += j.Gear.Strength != null ? (int)j.Gear.Strength : 0;
                    a += j.Gear.Agility != null ? (int)j.Gear.Agility : 0;
                    i += j.Gear.Intelligence != null ? (int)j.Gear.Intelligence : 0;
                }
            }
            this.TotalStrength = s;
            this.TotalAgility = a;
            this.TotalIntelligence = i;
        }
    }

      
}
