using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninja_manager.Helper;
using Ninja_manager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Ninja_manager.ViewModel.Crud_Gear
{
    public class GearEditViewModel : ViewModelBase
    {
        public ICommand SaveGearCommand { get; set; }
        public ICommand ResetGearCommand { get; set; }

        public ICommand UploadFileCommand { get; set; }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; base.RaisePropertyChanged(); this.CanExecuteSaveGear(); }
        }
        public int Price
        {
            get { return this._price; }
            set { this._price = value; base.RaisePropertyChanged(); this.CanExecuteSaveGear(); }
        }
        public string Category
        {
            get { return this._category; }
            set { this._category = value; base.RaisePropertyChanged(); this.CanExecuteSaveGear(); }
        }
        public string ImagePath
        {
            get { return this._image; }
            set { this._image = value; base.RaisePropertyChanged(); }
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
        public bool CanExecuteSave
        {
            get { return this._canExecuteSave; }
            set { this._canExecuteSave = value; base.RaisePropertyChanged(); }
        }

        public ObservableCollection<GearStat> Stats { get; set; }

        public List<string> Categories { get; private set; }

        private string _name;
        private int _price;
        private string _category;
        private string _image;

        private GearListViewModel _gearList;
        private Gear _gear;
        private GearRepository _gearRepository;

        private string _succesMessage;
        private string _errorMessage;
        private bool _canExecuteSave;
        private bool _isNew = false;
        private bool _imageAdded;

        public GearEditViewModel(GearListViewModel gearList)
        {
            this._gearList = gearList;
            this._gear = gearList.SelectedGear;

            if (this._gear.Name.Length == 0)
                this._isNew = true;

            this._gearRepository = new GearRepository();
            var repo = new CategoryRepository();
            this.Categories = repo.GetCategoryNames();
            
            this.ResetGear();

            this.SaveGearCommand = new RelayCommand(SaveGear);
            this.ResetGearCommand = new RelayCommand(ResetGear);
            this.UploadFileCommand = new RelayCommand(UploadFile);
        }


        private void SaveGear()
        {
            this._gear.Name = this.Name;
            this._gear.Price = this.Price;
            this._gear.GearStat = this.Stats; 
            this._gear.Category = this.Category;
            this._gear.Image = this.ImagePath != null ? this._gear.Id + Path.GetExtension(this.ImagePath) : null; 

            if (this._gearRepository.AddOrUpdate(this._gear, this.Stats.ToList(), this._isNew))
            {
                if (this._isNew)
                {
                    this._gearList.GearList.Add(this._gear);
                    this._isNew = false;
                    this.SuccesMessage = "Gear " + this.Name + " succesfully added!";
                }
                else
                {
                    this._gearList.GearList.Update(this._gear);
                    this.SuccesMessage = "Gear " + this.Name + " succesfully updated!";
                }
                this.SaveImage();
            }
            else
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "An error occured with the database!";
            }
        }
        private void SaveImage()
        {
            // check if there has a file been uploaded 
            if (this._imageAdded)
            {
                GearImageManagement.DeleteImage(this._gear.Id);  // removes the previous uploaded file if necessary
                GearImageManagement.SaveImage(this._gear.Id, this.ImagePath); 
            }
        }
        private bool CanExecuteSaveGear()
        {
            if (this.Name.Length <= 3)
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "Name has to be more than 3 characters!";
                this.CanExecuteSave = false;
                return false;
            }
            if (this.Category == null)
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "A category is required!";
                this.CanExecuteSave = false;
                return false;
            }
            if (this.Price < 0)
            {
                this.SuccesMessage = "";
                this.ErrorMessage = "The price is not valid!";
                this.CanExecuteSave = false;
                return false;
            }

            this.ErrorMessage = "";
            this.CanExecuteSave = true;
            return true;
        }

        private void ResetGear()
        {
            this.Name = this._gear.Name;
            this.Price = this._gear.Price;
            var list = this._gearRepository.GetStats(this._gear.Id);
            this.Stats = new ObservableCollection<GearStat>(list);
            this.Category = this._gear.Category;
            this.ImagePath = this._gear.Image != null ? Path.Combine(GearImageManagement.SourceFolder, this._gear.Image) : null;
            this._imageAdded = false;
        }

        private void UploadFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.PNG;*.JPG;*.JPEG;*.GIF)|*.PNG;*.JPG;*.JPEG;*.GIF|All files (*.*)|*.*";
      
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.ImagePath = fileDialog.FileName;
                this._imageAdded = true;
            }
            else
            {
                this.ImagePath = null; // removes the image
            }

        }
        
    }
}
