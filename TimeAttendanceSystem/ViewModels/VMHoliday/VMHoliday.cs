using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMHoliday.Commands;

namespace TimeAttendanceSystem.ViewModels.VMHoliday
{
    class VMHoliday : ObservableObject
    {
      #region Intialization
        public Holiday _selectedHoliday;
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
        private ObservableCollection<Holiday> _listOfHolidays;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Holiday selectedHoliday
        {
            get
            {
                return _selectedHoliday;
            }
            set
            {
                this.isEnabled = false;
                _selectedHoliday = value;
                base.OnPropertyChanged("selectedHoliday");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Holiday> listOfHolidays
        {
            get { return _listOfHolidays; }

            set
            {
                listOfHolidays = value;
                OnPropertyChanged("listOfHolidays");
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
        public VMHoliday()
        {
            entity = new TAS2013Entities();
            _selectedHoliday = new Holiday();
            
            _listOfHolidays = new ObservableCollection<Holiday>(entity.Holidays.ToList());
            _selectedHoliday = entity.Holidays.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandHol(_selectedHoliday);
            this._EditCommand = new EditCommandHol(this);
            this._DeleteCommand = new DeleteCommandHol(_selectedHoliday);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandHol(this);
            base.OnPropertyChanged("_listOfHolidays");
        }
        #endregion  
    }
}
