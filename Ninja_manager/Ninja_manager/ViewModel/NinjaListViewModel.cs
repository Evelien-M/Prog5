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

        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public List<Ninja> NinjaList { get; private set; }

        public Ninja SelectedNinja { get; set; }

        private NinjaEditView _ninjaEditView;
        private NinjaRepository _ninjaRepository;
        private List<string> _gearcategories;
        private int _newId;

        public NinjaListViewModel()
        {
            this.AddItemCommand = new RelayCommand(AddItem, true);
            this.EditItemCommand = new RelayCommand(EditItem, true);
            this._ninjaRepository = new NinjaRepository();

            var repo = new CategoryRepository();
            this._gearcategories = repo.GetCategories();
            
            this.NinjaList = this._ninjaRepository.GetNinjas();
            this._newId = this.NinjaList.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault() + 1;
        }


        private void AddItem()
        {
            this.SelectedNinja = new Ninja() { Id = this._newId, Name = "" , Gold = 500 };
            var list = new List<Inventory>();
            foreach(var cat in this._gearcategories)
            {
                var inv = new Inventory() { Id_Ninja = this._newId, Category = cat, Ninja = this.SelectedNinja };
                list.Add(inv);
            }
            
            this._ninjaEditView = new NinjaEditView();
            this._ninjaEditView.Show();
        }
        private void EditItem()
        {
            this._ninjaEditView = new NinjaEditView();
            this._ninjaEditView.Show();
        }
    }
}
