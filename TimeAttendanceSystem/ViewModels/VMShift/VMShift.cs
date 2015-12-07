using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using TimeAttendanceSystem.QueryBuilders;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMShift.Commands;

namespace TimeAttendanceSystem.ViewModels.VMShift
{
    class VMShift :ObservableObject
    {
        #region Intialization
        private Shift _selectedShift;
        private Boolean _isEnabled = false;
        private Boolean _isAdding = false;
        private ObservableCollection<DaysName> _listOfDays;
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
        public ObservableCollection<DaysName> ListOfDays
        {
            get
            {
                return _listOfDays;
            }
            set
            {
                _listOfDays = value;
                base.OnPropertyChanged("ListOfDays");
            }
        }
        private ObservableCollection<Shift> _listOfShifts;
        private ObservableCollection<Emp> _listOfShiftEmps;
        private ObservableCollection<RosterType> _listOfRosterType;
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
                User _user = GlobalClasses.Global.user;
                QueryBuilderForSection queryForSection = new QueryBuilderForSection();
                string query = queryForSection.MakeCustomizeQuerySection(_user);
                _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(query).AsQueryable().Where(aa => aa.ShiftID == selectedShift.ShiftID));
                base.OnPropertyChanged("listOfShiftEmps");
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
        public ObservableCollection<RosterType> listOfRosterType
        {
            get { return _listOfRosterType; }

            set
            {
                listOfRosterType = value;
                OnPropertyChanged("listOfRosterType");
            }
        }
        public ObservableCollection<Emp> listOfShiftEmps
        {
            get { return _listOfShiftEmps; }

            set
            {
                _listOfShiftEmps = value;
               
                OnPropertyChanged("listOfShiftEmps");
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
            _listOfDays = new ObservableCollection<DaysName>(entity.DaysNames.ToList());
            _listOfRosterType = new ObservableCollection<RosterType>(entity.RosterTypes.ToList());
            User _user = GlobalClasses.Global.user;
            QueryBuilderForSection queryForSection = new QueryBuilderForSection();
            string query = queryForSection.MakeCustomizeQuerySection(_user);
            _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(query).AsQueryable().Where(aa => aa.ShiftID == selectedShift.ShiftID));
            this._AddCommand = new AddCommandShift(_selectedShift);
            this._EditCommand = new EditCommandShift(this);
            this._DeleteCommand = new DeleteCommandShift(_selectedShift);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandShift(this);
            base.OnPropertyChanged("_listOfShifts");
            base.OnPropertyChanged("_listOfShiftEmps");
        }
        #endregion  
    }
}
