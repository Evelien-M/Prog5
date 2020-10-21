using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Shop;
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
    public class NinjaEditViewModel : ViewModelBase, INotifyPropertyChanged
    {

        public ICommand SaveNinjaCommand { get; set; }
        public ICommand ResetNinjaCommand { get; set; }
        public ICommand ShopNinjaGearCommand { get; set; }
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
        public ObservableCollection<Inventory> InventoryList { get; private set; }
        public Inventory SelectedInventory { get; set; }

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

        public NinjaEditViewModel(NinjaListViewModel ninjaList)
        {
            this._ninjaList = ninjaList;
            this._ninja = ninjaList.SelectedNinja;


            if (this._ninja.Name.Length == 0)
                this._isNew = true;


            this._ninjaRepository = new NinjaRepository();

            var list = this._ninjaRepository.GetInventory(this._ninja.Id);
            this.InventoryList = new ObservableCollection<Inventory>(list);

            this.SaveNinjaCommand = new RelayCommand(SaveNinja);
            this.ResetNinjaCommand = new RelayCommand(ResetNinja);
            this.ShopNinjaGearCommand = new RelayCommand(ShopNinjaGear);

            this.Name = this._ninja.Name;
            this.Gold = this._ninja.Gold;
        }

        private void SaveNinja()
        {
            this._ninja.Name = this.Name;

            if (this._ninjaRepository.AddOrUpdate(this._ninja))
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
            var list = this._ninjaRepository.GetInventory(this._ninja.Id);
            this.InventoryList.Clear();
            foreach (var i in list)
                this.InventoryList.Add(i);

            this.Name = this._ninja.Name;
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


    }

      
}
