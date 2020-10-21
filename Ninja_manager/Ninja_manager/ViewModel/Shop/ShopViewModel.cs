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
    public class ShopViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand BuyItemCommand { get; set; }
        public int Gold { get; private set; }
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
        public List<Gear> GearList
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
        public Gear SelectedGear 
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

        private List<Gear> _gearList;
        private Category _selectedCategory;
        private Gear _selectedGear;
        private GearRepository _gearRepsotory;
        private GearItemView _gearItemView;
        private NinjaEditViewModel _ninjaEdit;
        private bool _canExecuteBuyItem = false;
        private string _succesMessage;
        private string _errorMessage;

        public ShopViewModel(NinjaEditViewModel ninjaEdit)
        {
            this._ninjaEdit = ninjaEdit;
            this.Gold = ninjaEdit.Gold;
            var repo = new CategoryRepository();
            this.Categories = repo.GetCategories();
            this._gearRepsotory = new GearRepository();

            this.BuyItemCommand = new RelayCommand(BuyItem);
        }

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
            this._ninjaEdit.InventoryList.Update(this.SelectedGear);
            // this._ninjaEdit.AddGear(this.SelectedGear);
        }
        public bool CanExecuteBuyGearItem()
        {
            if (this._selectedGear != null)
            {
                if (this._selectedGear.Price <= this.Gold)
                {
                    this.ErrorMessage = "";
                    this.CanExecuteBuyItem = true;
                    return true;
                }
            }

            this.ErrorMessage = "You don't have enough gold.";
            this.CanExecuteBuyItem = false;
            return false;
        }
    }
}
