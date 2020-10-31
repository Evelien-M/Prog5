using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Shop;
using Ninja_manager.ViewModel.Crud_Ninja;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Shop
{
    public class ShopViewModel : ViewModelBase
    {
        public ICommand BuyItemCommand { get; set; }
        public List<Category> Categories { get; private set; }

        public Category SelectedCategory 
        {
            get
            {
                return this._selectedCategory;
            }
            set
            {
                this._selectedCategory = value;
                this.UpdateGearList();
            }
        }
        public List<GearItemViewModel> GearList
        {
            get
            {
                return this._gearList;
            }
            set
            {
                this._gearList = value;
                base.RaisePropertyChanged();
            }
        }
        public GearItemViewModel SelectedGear 
        {
            get
            {
                return this._selectedGear;
            }
            set
            {
                this._selectedGear = value;
                this.UpdateGearItem();
                this.CanExecuteBuyGearItem();
            }
        }

        public GearItemView GearItemView
        {
            get
            {
                return this._gearItemView;
            }
            set
            {
                this._gearItemView = value;
                base.RaisePropertyChanged();
            } 
        }
        public int Gold
        {
            get { return this._gold; }
            set { this._gold = value; base.RaisePropertyChanged(); }
        }
        public bool CanExecuteBuyItem
        {
            get { return this._canExecuteBuyItem; }
            set { this._canExecuteBuyItem = value; base.RaisePropertyChanged(); }
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

        private List<GearItemViewModel> _gearList;
        private Category _selectedCategory;
        private GearItemViewModel _selectedGear;
        private GearRepository _gearRepsotory;
        private GearItemView _gearItemView;
        private NinjaEditViewModel _ninjaEdit;
        private bool _canExecuteBuyItem = false;
        private string _succesMessage;
        private string _errorMessage;
        private int _gold;

        public ShopViewModel(NinjaEditViewModel ninjaEdit)
        {
            this._ninjaEdit = ninjaEdit;
            this.Gold = ninjaEdit.Gold;
            this.ErrorMessage = "";
            var repo = new CategoryRepository();
            this.Categories = repo.GetAllCategories();
            this._gearRepsotory = new GearRepository();
            this._ninjaEdit.ShopViewModel = this;
            this.BuyItemCommand = new RelayCommand(BuyItem);
        }
        // updates the gearlist if category is selected
        private void UpdateGearList()
        {
            this.GearList = this._gearRepsotory.GetByCategory(this.SelectedCategory.Name);
            this.GearItemView = null;
        }
        private void UpdateGearItem()
        {
            this.GearItemView = new GearItemView();
        }

        public void BuyItem()
        {
            this._ninjaEdit.AddToInventory(this.SelectedGear.Gear);
        }
        public bool CanExecuteBuyGearItem()
        {
            if (this._selectedGear != null)
            {
                // checks if user has already item from selected category
                var invItem = this._ninjaEdit.InventoryList.FirstOrDefault(i => i.Inventory.Category == this._selectedCategory.Name);
                if (invItem.Inventory.Id_Gear != null)
                {
                    this.ErrorMessage = "You already have " + invItem.Inventory.Category + " item";
                    this.CanExecuteBuyItem = false;
                    return false;
                }
                // checks if user has enough gold
                if (this._selectedGear.Gear.Price > this.Gold)
                    {
                    this.ErrorMessage = "You don't have enough gold.";
                    this.CanExecuteBuyItem = false;
                    return false;
                }
                // reset error message, user is able te buy the item
                this.ErrorMessage = "";
                this.CanExecuteBuyItem = true;
                return true;
            }



            this.ErrorMessage = "";
            this.CanExecuteBuyItem = false;
            return false;
        }

        public void Update(NinjaEditViewModel ninjaEditViewModel)
        {
            this._ninjaEdit = ninjaEditViewModel;
            this.Gold = ninjaEditViewModel.Gold;
            this.CanExecuteBuyGearItem();
        }
    }
}
