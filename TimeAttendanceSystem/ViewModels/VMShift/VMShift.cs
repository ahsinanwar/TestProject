using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShift
{
    class VMShift :ObservableObject
    {
        #region Intialization
        public Shift _selectedShift;
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
        private ObservableCollection<Shift> _listOfShifts;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Shift selectedShift
        {
            get
            {
                return _selectedShift;
            }
            set
            {
                this.isEnabled = false;
                _selectedShift = value;
                base.OnPropertyChanged("selectedShift");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Shift> listOfShifts
        {
            get { return _listOfShifts; }

            set
            {
                listOfShifts = value;
                OnPropertyChanged("listOfShifts");
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
        public VMShift()
        {
            entity = new TAS2013Entities();
            _selectedShift = new Shift();
            _listOfShifts = new ObservableCollection<Shift>(entity.Shifts.ToList());
            _selectedShift = entity.Shifts.ToList().FirstOrDefault();
            this._AddCommand = new AddCommand(_selectedShift);
            this._EditCommand = new EditCommand(this);
            this._DeleteCommand = new DeleteCommand(_selectedShift);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommand(this);
            base.OnPropertyChanged("_listOfShifts");
        }
        #endregion  
    }
}
