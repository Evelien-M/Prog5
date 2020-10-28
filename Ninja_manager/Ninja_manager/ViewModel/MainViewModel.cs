using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.View.Crud_Gear;
using Ninja_manager.View.Crud_Ninja;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ninja_manager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand GoToNinjaCrudCommand { get; set; }
        public ICommand GoToGearCrudCommand { get; set; }

        public Page PageView
        {
            get
            {
                return this._pageView;
            }
            set
            {
                this._pageView = value;
                base.RaisePropertyChanged();
            }
        }

        private Page _pageView;

        public MainViewModel()
        {
            this.GoToNinjaCrudCommand = new RelayCommand(GoToNinjaCrud);
            this.GoToGearCrudCommand = new RelayCommand(GoToGearCrud);

            this.GoToNinjaCrud();
        }


        private void GoToNinjaCrud()
        {
            this.PageView = new NinjaListView();
        }

        private void GoToGearCrud()
        {
            this.PageView = new GearListView();
        }

    }
}