using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Crud_Gear
{
    public class GearEditViewModel : ViewModelBase
    {
        public ICommand SaveNinjaCommand { get; set; }
        public ICommand ResetNinjaCommand { get; set; }

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
        public int? Strength
        {
            get { return this._strength; }
            set { this._strength = value; base.RaisePropertyChanged(); }
        }
        public int? Agility
        {
            get { return this._agility; }
            set { this._agility = value; base.RaisePropertyChanged(); }
        }
        public int? Intelligence
        {
            get { return this._intelligence; }
            set { this._intelligence = value; base.RaisePropertyChanged(); }
        }
        public string Category
        {
            get { return this._category; }
            set { this._category = value; base.RaisePropertyChanged(); }
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

        public List<string> Categories { get; private set; }

        private string _name;
        private int _price;
        private int? _strength;
        private int? _agility;
        private int? _intelligence;
        private string _category;

        private GearListViewModel _gearList;
        private Gear _gear;
        private GearRepository _gearRepository;

        private string _succesMessage;
        private string _errorMessage;
        private bool _canExecuteSave;
        private bool _isNew = false;

        public GearEditViewModel(GearListViewModel gearList)
        {
            this._gearList = gearList;
            this._gear = gearList.SelectedGear;

            if (this._gear.Name == null)
                this._isNew = true;

            this.Name = this._gear.Name;
            this.Price = this._gear.Price;
            this.Strength = this._gear.Strength;
            this.Agility = this._gear.Agility;
            this.Intelligence = this._gear.Intelligence;

            this._gearRepository = new GearRepository();
            var repo = new CategoryRepository();
            this.Categories = repo.GetCategoryNames();

            this.SaveNinjaCommand = new RelayCommand(SaveGear);
        }


        public void SaveGear()
        {
            this._gear.Name = this.Name;
            this._gear.Price = this.Price;
            this._gear.Intelligence = this.Intelligence;
            this._gear.Strength = this.Strength;
            this._gear.Agility = this.Agility;
            this._gear.Category = this.Category;

            
            if (this._gearRepository.AddOrUpdate(this._gear))
            {
                if (this._isNew)
                {
                    this._gearList.GearList.Add(this._gear);
                    this._isNew = false;
                    this.SuccesMessage = "Gear " + this.Name + " succesfully added!";
                }
                else
                {
                    this._gearList.GearList.Update(this._gear);
                    this.SuccesMessage = "Gear " + this.Name + " succesfully updatet!";
                }
            }
            else
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "An error occured with the database!";
            }
        }

        public void ResetGear()
        {

        }
    }
}
