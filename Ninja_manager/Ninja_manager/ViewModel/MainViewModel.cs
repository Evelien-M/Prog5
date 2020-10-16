using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.View.Crud_Ninja;
using System.Windows.Input;

namespace Ninja_manager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand GoToNinjaCrudCommand { get; set; }
        public ICommand GoToGearCrudCommand { get; set; }
        
        private NinjaListView _ninjaCrudView;
        public MainViewModel()
        {
            this.GoToNinjaCrudCommand = new RelayCommand(GoToNinjaCrud);
            this.GoToGearCrudCommand = new RelayCommand(GoToGearCrud);
        }


        private void GoToNinjaCrud()
        {
            this._ninjaCrudView = new NinjaListView();
            this._ninjaCrudView.Show();
        }

        private void GoToGearCrud()
        {

        }
    }
}