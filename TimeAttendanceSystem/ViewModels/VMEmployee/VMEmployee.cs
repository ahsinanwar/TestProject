using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMEmployee.Commands;

namespace TimeAttendanceSystem.ViewModels.VMEmployee
{
    class VMEmployee: ObservableObject
    {
        #region Intialization
        public Emp _selectedEmp;
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
        private ObservableCollection<Emp> _listOfEmps;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;
        public Emp selectedEmp
        {
            get
            {
                return _selectedEmp;
            }
            set
            {
                this.isEnabled = false;
                _selectedEmp = value;
                base.OnPropertyChanged("selectedEmp");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Emp> listOfEmps
        {
            get { return _listOfEmps; }

            set
            {
                listOfEmps = value;
                OnPropertyChanged("listOfEmps");
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
        public VMEmployee()
        {
            entity = new TAS2013Entities();
            _selectedEmp = new Emp();
            _listOfEmps = new ObservableCollection<Emp>(entity.Emps.ToList());
            _selectedEmp = entity.Emps.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandEmp(_selectedEmp);
            this._EditCommand = new EditCommandEmp(this);
            this._DeleteCommand = new DeleteCommandEmp(_selectedEmp);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandEmp(this);
            base.OnPropertyChanged("_listOfEmps");
        }
        #endregion
    }
}
