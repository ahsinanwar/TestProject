using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDesignation.Commands;

namespace TimeAttendanceSystem.ViewModels.VMDesignation
{
    class VMDesignation : ObservableObject
    {
         #region Intialization
        public Designation _selectedDesg;
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
        private ObservableCollection<Designation> _listOfDesg;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Designation selectedDesg
        {
            get
            {
                return _selectedDesg;
            }
            set
            {
                this.isEnabled = false;
                _selectedDesg = value;
                base.OnPropertyChanged("selectedDesg");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Designation> listOfDesgs
        {
            get { return _listOfDesg; }

            set
            {
                listOfDesgs = value;
                OnPropertyChanged("listOfDesgs");
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
        public VMDesignation()
        {
            entity = new TAS2013Entities();
            _selectedDesg = new Designation();
            
            _listOfDesg = new ObservableCollection<Designation>(entity.Designations.ToList());
            _selectedDesg = entity.Designations.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandDesg(_selectedDesg);
            this._EditCommand = new EditCommandDesg(this);
            this._DeleteCommand = new DeleteCommandDesg(_selectedDesg);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandDesg(this);
            base.OnPropertyChanged("_listOfDesg");
        }
        #endregion
    }
}
