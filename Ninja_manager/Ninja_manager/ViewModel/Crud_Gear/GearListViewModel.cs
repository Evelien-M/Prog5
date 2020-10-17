using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Repository;
using Ninja_manager.View.Crud_Gear;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Crud_Gear
{
    public class GearListViewModel : ViewModelBase
    {
        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ObservableCollection<Gear> GearList { get; private set; }

        public Ninja SelectedNinja { get; set; }

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

            this.GearList = new ObservableCollection<Gear>(this._gearRepository.GetGear());
            this._newId = this.GearList.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault() + 1;
        }

        private void AddItem()
        {
            throw new NotImplementedException();
        }
        private void EditItem()
        {
            throw new NotImplementedException();
        }
        private void DeleteItem()
        {
            throw new NotImplementedException();
        }

    }
}
