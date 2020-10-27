using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Crud_Gear;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Crud_Gear
{
    public class GearListViewModel : ViewModelBase
    {
        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ObservableCollection<Gear> GearList { get; private set; }

        public Gear SelectedGear { get; set; }

        private GearEditView _gearEditView;
        private GearRepository _gearRepository;
        private List<Category> _gearcategories;
        private int _newId;

        public GearListViewModel()
        {
            this.AddItemCommand = new RelayCommand(AddItem);
            this.EditItemCommand = new RelayCommand(EditItem);
            this.DeleteItemCommand = new RelayCommand(DeleteItem);
            this._gearRepository = new GearRepository();

            var repo = new CategoryRepository();
            this._gearcategories = repo.GetCategories();

            this.GearList = new ObservableCollection<Gear>(this._gearRepository.Get());
            this._newId = this.GearList.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault() + 1;
        }

        private void AddItem()
        {
            if (this._gearEditView != null)
                if (!this._gearEditView.ClosePrompt())
                    return;

            this.SelectedGear = new Gear() { Name = "", Id = this._newId};

            this._gearEditView = new GearEditView();
            this._gearEditView.Show();
        }
        private void EditItem()
        {
            if (this._gearEditView != null)
                if (!this._gearEditView.ClosePrompt())
                    return;

            if (this.SelectedGear != null)
            {
                this._gearEditView = new GearEditView();
                this._gearEditView.Show();
            }
        }
        private void DeleteItem()
        {
            if (this.SelectedGear != null)
            {
                string sMessageBoxText = "Are you sure you want to delete " + this.SelectedGear.Name + "?";
                string sCaption = "Deleting " + this.SelectedGear.Name;

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                if (rsltMessageBox.Equals(MessageBoxResult.Yes))
                {
                    int id = this.SelectedGear.Id;
                    if (this._gearRepository.Delete(this.SelectedGear))
                    {
                        this.GearList.Remove(this.SelectedGear);
                        GearImageManagement.DeleteImage(id);
                    }
                }
            }
        }

    }
}
