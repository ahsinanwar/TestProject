using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMCategory.Commands;

namespace TimeAttendanceSystem.ViewModels.VMCategory
{
    class VMCategory: ObservableObject
    {
        #region Intialization
        public Category _selectedCat;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        public Boolean isAdding
        {
            get { return _isAdding; }
            set
            {
                _isAdding = value;
                base.OnPropertyChanged("isAdding");

            }
        }
        public Boolean isEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                base.OnPropertyChanged("isEnabled");
            }
        }
        private ObservableCollection<Category> _listOfCats;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Category selectedCat
        {
            get
            {
                return _selectedCat;
            }
            set
            {
                this.isEnabled = false;
                _selectedCat = value;
                base.OnPropertyChanged("selectedCat");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Category> listOfCats
        {
            get { return _listOfCats; }

            set
            {
                listOfCats = value;
                OnPropertyChanged("listOfCats");
            }
        }
        #endregion

        #region ICommands
        public ICommand EditCommand
        {
            get
            {
                return _EditCommand;
            }
        }

        public ICommand AddCommand
        {

            get
            {
                return _AddCommand;
            }

        }
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }

        }
        public ICommand DeleteCommand
        {
            get
            {
                return _DeleteCommand;
            }

        }
        #endregion

        #region constructor
        public VMCategory()
        {
            entity = new TAS2013Entities();
            _selectedCat = new Category();
            
            _listOfCats = new ObservableCollection<Category>(entity.Categories.ToList());
            _selectedCat = entity.Categories.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandCat(_selectedCat);
            this._EditCommand = new EditCommandCat(this);
            this._DeleteCommand = new DeleteCommandCat(_selectedCat);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandCat(this);
            base.OnPropertyChanged("_listOfCats");
        }
        #endregion
    }
}
