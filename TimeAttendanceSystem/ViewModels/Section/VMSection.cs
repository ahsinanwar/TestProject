using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection
{
    class VMSection: ObservableObject
    {
        #region Intialization
        public Section _selectedSec;
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
        private ObservableCollection<Section> _listOfSecs;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Section selectedSec
        {
            get
            {
                return _selectedSec;
            }
            set
            {
                this.isEnabled = false;
                _selectedSec = value;
                base.OnPropertyChanged("selectedSec");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Section> listOfSecs
        {
            get { return _listOfSecs; }

            set
            {
                listOfSecs = value;
                OnPropertyChanged("listOfSecs");
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
        public VMSection()
        {
            entity = new TAS2013Entities();
            _selectedSec = new Section();
            _listOfSecs = new ObservableCollection<Section>(entity.Sections.ToList());
            _selectedSec = entity.Sections.ToList().FirstOrDefault();
            this._AddCommand = new AddCommand(_selectedSec);
            this._EditCommand = new EditCommand(this);
            this._DeleteCommand = new DeleteCommand(_selectedSec);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommand(this);
            base.OnPropertyChanged("_listOfSecs");
        }
        #endregion
    }
}
