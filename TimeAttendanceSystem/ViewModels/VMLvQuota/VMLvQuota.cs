using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMLvQuota.Commands;

namespace TimeAttendanceSystem.ViewModels.VMLvQuota
{
    class VMLvQuota : ObservableObject
    { 
         #region Intialization
        private LvConsumed _selectedLvConsumed;
        private Boolean _isEnabled = false;
        private Boolean _isAdding = false;
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
        
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;
        private ObservableCollection<LvConsumed> _listOfLvConsumed;
        private ObservableCollection<Emp> _listOfLvConsumedEmps;
        public ObservableCollection<Emp> ListOfLvConsumedEmps
        {

            get { return _listOfLvConsumedEmps; }
            set
            {

                _listOfLvConsumedEmps = value;
                
                base.OnPropertyChanged("isEnabled");
            }
        }

        public LvConsumed selectedLvConsumed
        {
            get
            {
                return _selectedLvConsumed;
            }
            set
            {
                this.isEnabled = false;
                _selectedLvConsumed = value;
                if (_selectedLvConsumed != null)
                {
                    //_listOfDesgEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.DesigID == _selectedLvConsumed.DesignationID));
                    //base.OnPropertyChanged("ListOfDesgEmps");
                    base.OnPropertyChanged("selectedLvConsumed");
                    base.OnPropertyChanged("isEnabled");
                }
                
            }
        }

        public ObservableCollection<LvConsumed> listOfLvConsumed
        {
            get { return _listOfLvConsumed; }

            set
            {
                listOfLvConsumed = value;
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
        public VMLvQuota()
        {
            entity = new TAS2013Entities();
            _selectedLvConsumed = new LvConsumed();

            _listOfLvConsumed = new ObservableCollection<LvConsumed>(entity.LvConsumeds.ToList());
            _selectedLvConsumed = entity.LvConsumeds.ToList().FirstOrDefault();
            _listOfLvConsumedEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.EmpID == _selectedLvConsumed.EmpID));
            this._AddCommand = new AddCommandLvQuota(_selectedLvConsumed);
            this._EditCommand = new EditCommandLvQuota(this);
            this._DeleteCommand = new DeleteCommandLvQuota(_selectedLvConsumed);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLvQuota(this);
            base.OnPropertyChanged("_listOfLvConsumed");
            base.OnPropertyChanged("_listOfLvConsumedEmps");
        }
        #endregion
    }
}
