using GalaSoft.MvvmLight.Command;
using Ninja_manager.Repository;
using Ninja_manager.View.Crud_Ninja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ninja_manager.ViewModel
{
    public class NinjaListViewModel
    {

        public ICommand AddNewItemCommand { get; set; }
        public List<Ninja> NinjaList { get; private set; }


        private NinjaEditView _newNinjaView;
        private NinjaRepository _ninjaRepository;

        public NinjaListViewModel()
        {
            this.AddNewItemCommand = new RelayCommand(CreateNewItem, true);
            this._ninjaRepository = new NinjaRepository();
            this.NinjaList = this._ninjaRepository.GetNinjas();
        }


        private void CreateNewItem()
        {
            this._newNinjaView = new NinjaEditView();
            this._newNinjaView.Show();
        }
    }
}
