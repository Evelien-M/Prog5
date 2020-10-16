using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel
{
    public class NinjaEditViewModel : ViewModelBase
    {

        public ICommand SaveNinjaCommand { get; set; }
        public ICommand ResetNinjaCommand { get; set; }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; base.RaisePropertyChanged(); }
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
        public List<Inventory> Inventory { get; private set; }

        private Ninja _ninja;
        private string _name;
        private int _gold;
        private string _succesMessage;
        private string _errorMessage;
        private NinjaRepository _ninjaRepository;
        public NinjaEditViewModel(Ninja ninja)
        {
            this._ninja = ninja;

            this.Inventory = ninja.Inventory.OrderBy(o => o.Category1.Order).ToList();

            this._ninjaRepository = new NinjaRepository();

            this.SaveNinjaCommand = new RelayCommand(SaveNinja, CanExecuteSaveNinja);
            this.ResetNinjaCommand = new RelayCommand(ResetNinja);

            this.Name = ninja.Name;
            this.Gold = ninja.Gold;
        }

        private void SaveNinja()
        {
            this._ninja.Name = this.Name;

            if (this._ninjaRepository.AddOrUpdateNinja(this._ninja) && this._ninja.Name.Length == 0)
            {
                this.SuccesMessage = "Ninja " + this.Name + " succesfully added!";
            }
            else if (this._ninjaRepository.AddOrUpdateNinja(this._ninja) && this._ninja.Name.Length != 0)
            {
                this.SuccesMessage = "Ninja " + this.Name + " succesfully updatet!";
            }
            else
            {
                this.ErrorMessage = "Something went wrong!";
            }
        }

        private void ResetNinja()
        {
            this.Name = this._ninja.Name;
        }

        private bool CanExecuteSaveNinja()
        {
            return true;

            // TODO
            if (this.Name.Length > 3)
            {
                this.ErrorMessage = "";
                return true;
            }

            this.SuccesMessage = "";
            this.ErrorMessage = "Name has to be more than 3 characters!";
            return false;
        }
    }
}
