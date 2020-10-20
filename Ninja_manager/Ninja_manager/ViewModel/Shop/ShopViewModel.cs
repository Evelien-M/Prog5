using GalaSoft.MvvmLight;
using Ninja_manager.Repository;
using Ninja_manager.View.Shop;
using Ninja_manager.ViewModel.Crud_Ninja;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.ViewModel.Shop
{
    public class ShopViewModel : ViewModelBase
    {

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

        private List<Gear> _gearList;
        private Category _selectedCategory;
        private Gear _selectedGear;
        private GearRepository _gearRepsotory;
        private GearItemView _gearItemView;
        private NinjaEditViewModel _ninjaEdit;

        public ShopViewModel(NinjaEditViewModel ninjaEdit)
        {
            this._ninjaEdit = ninjaEdit;
            var repo = new CategoryRepository();
            this.Categories = repo.GetCategories();
            this._gearRepsotory = new GearRepository();
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
    }
}
