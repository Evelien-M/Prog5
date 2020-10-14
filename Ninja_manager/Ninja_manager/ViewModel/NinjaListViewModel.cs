using GalaSoft.MvvmLight.Command;
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



        private NinjaEditView _newNinjaView;

        public NinjaListViewModel()
        {
            this.AddNewItemCommand = new RelayCommand(CreateNewItem, true);
        }


        private void CreateNewItem()
        {
            this._newNinjaView = new NinjaEditView();
            this._newNinjaView.Show();
        }
    }
}
