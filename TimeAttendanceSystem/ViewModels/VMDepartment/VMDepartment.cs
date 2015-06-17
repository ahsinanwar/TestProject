using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDepartment.Commands;

namespace TimeAttendanceSystem.ViewModels.VMDepartment
{
    class VMDepartments : ObservableObject
    {
        #region Intialization
        public Department _selectedDept;
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
        private ObservableCollection<Department> _listOfDepts;
        private ObservableCollection<Division> _listOfDivs;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Department selectedDept
        {
            get
            {
                return _selectedDept;
            }
            set
            {
                this.isEnabled = false;
                _selectedDept = value;
                base.OnPropertyChanged("selectedDept");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Department> listOfDepts
        {
            get { return _listOfDepts; }

            set
            {
                listOfDepts = value;
                OnPropertyChanged("listOfDepts");
            }
        }
        public ObservableCollection<Division> listOfDivs
        {
            get { return _listOfDivs; }

            set
            {
                listOfDivs = value;
                OnPropertyChanged("listOfDivs");
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
        public VMDepartments()
        {
            entity = new TAS2013Entities();
            _selectedDept = new Department();
            _listOfDepts = new ObservableCollection<Department>(entity.Departments.ToList());
            _listOfDivs = new ObservableCollection<Division>(entity.Divisions.ToList());
            _selectedDept = entity.Departments.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandDept(_selectedDept);
            this._EditCommand = new EditCommandDept(this);
            this._DeleteCommand = new DeleteCommandDept(_selectedDept);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandDept(this);
            base.OnPropertyChanged("_listOfDepts");
            base.OnPropertyChanged("_listOfDivs");
        }
        #endregion
    }
}
