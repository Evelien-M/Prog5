using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using Ninja_manager.View.Crud_Ninja;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ninja_manager.ViewModel.Crud_Ninja
{
    public class NinjaListViewModel : ViewModelBase
    {

        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ObservableCollection<Ninja> NinjaList { get; private set; }

        public Ninja SelectedNinja { get; set; }

        public NinjaEditView _ninjaEditView;
        private NinjaRepository _ninjaRepository;

        public NinjaListViewModel()
        {
            this.AddItemCommand = new RelayCommand(AddItem);
            this.EditItemCommand = new RelayCommand(EditItem);
            this.DeleteItemCommand = new RelayCommand(DeleteItem);
            this._ninjaRepository = new NinjaRepository();


            this.NinjaList = new ObservableCollection<Ninja>(this._ninjaRepository.GetNinjas());
        }


        private void AddItem()
        {
            if (this._ninjaEditView != null)
                if (!this._ninjaEditView.ClosePrompt())
                    return;

            var rng = new Random();
            var gold = rng.Next(40, 80) * 10;
            var newId = this.NinjaList.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault() + 1;
            this.SelectedNinja = new Ninja() { Id = newId, Name = "" , Gold = gold };

            this._ninjaEditView = new NinjaEditView();
            this._ninjaEditView.Show();
        }
        private void EditItem()
        {
            if (this._ninjaEditView != null)
                if (!this._ninjaEditView.ClosePrompt())
                    return;

            if (this.SelectedNinja != null)
            {
                this._ninjaEditView = new NinjaEditView();
                this._ninjaEditView.Show();
            }
        }
        private void DeleteItem()
        {
            if (this.SelectedNinja != null)
            {
                string sMessageBoxText = "Are you sure you want to delete " + this.SelectedNinja.Name + "?";
                string sCaption = "Deleting " + this.SelectedNinja.Name;

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                if (rsltMessageBox.Equals(MessageBoxResult.Yes))
                {
                    if (this._ninjaRepository.Delete(this.SelectedNinja))
                    {
                        this.NinjaList.Remove(this.SelectedNinja);
                    }
                } 
            }
        }
    }
}
