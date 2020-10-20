/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Ninja_manager"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Ninja_manager.ViewModel.Crud_Gear;
using Ninja_manager.ViewModel.Crud_Ninja;
using Ninja_manager.ViewModel.Shop;

namespace Ninja_manager.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private ShopViewModel _shop;
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<NinjaEditViewModel>();
            SimpleIoc.Default.Register<NinjaListViewModel>();

            SimpleIoc.Default.Register<GearEditViewModel>();
            SimpleIoc.Default.Register<GearListViewModel>();

            SimpleIoc.Default.Register<ShopViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public NinjaEditViewModel NinjaEdit
        {
            get
            {
                return new NinjaEditViewModel(this.NinjaList);
            }
        }

        public NinjaListViewModel NinjaList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NinjaListViewModel>();
            }
        }

        public GearEditViewModel GearEdit
        {
            get
            {
                return new GearEditViewModel(this.GearList);
            }
        }

        public GearListViewModel GearList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GearListViewModel>();
            }
        }

        public ShopViewModel Shop
        {
            get
            {
                this._shop = new ShopViewModel(this.NinjaEdit);
                return this._shop;
            }
        }

        public Gear GearItem
        {
            get
            {
                return this._shop.SelectedGear;
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}