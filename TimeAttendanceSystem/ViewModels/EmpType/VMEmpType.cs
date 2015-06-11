using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMEmpType.Commands;

namespace TimeAttendanceSystem.ViewModels.VMEmpType
{
    class VMEmpType : ObservableObject
    {
        #region Intialization
        public EmpType _selectedEmpType;
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
        private ObservableCollection<EmpType> _listOfEmpTypes;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public EmpType selectedEmpType
        {
            get
            {
                return _selectedEmpType;
            }
            set
            {
                this.isEnabled = false;
                _selectedEmpType = value;
                base.OnPropertyChanged("selectedEmpType");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<EmpType> listOfEmpTypes
        {
            get { return _listOfEmpTypes; }

            set
            {
                listOfEmpTypes = value;
                OnPropertyChanged("listOfEmpTypes");
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
        public VMEmpType()
        {
            entity = new TAS2013Entities();
            _selectedEmpType = new EmpType();
            _listOfEmpTypes = new ObservableCollection<EmpType>(entity.EmpTypes.ToList());
            _selectedEmpType = entity.EmpTypes.ToList().FirstOrDefault();
            this._AddCommand = new AddCommand(_selectedEmpType);
            this._EditCommand = new EditCommand(this);
            this._DeleteCommand = new DeleteCommand(_selectedEmpType);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommand(this);
            base.OnPropertyChanged("_listOfEmpTypes");
        }
        #endregion
    }
}
