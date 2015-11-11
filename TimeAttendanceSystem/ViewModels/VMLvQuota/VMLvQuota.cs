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
        private LvQuota _selectedLvQuota;
        private LvQuota _selectedLvQuotaEmp;
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
        private ObservableCollection<LvQuota> _listOfLvQuotas;
        private ObservableCollection<Emp> _listOfLvQuotaEmps;
        public ObservableCollection<Emp> ListOfLvQuotaEmps
        {

            get { return _listOfLvQuotaEmps; }
            set
            {

                _listOfLvQuotaEmps = value;
                
                base.OnPropertyChanged("isEnabled");
            }
        }

        public LvQuota selectedLvQuota
        {
            get
            {
                return _selectedLvQuota;
            }
            set
            {
                this.isEnabled = false;
                _selectedLvQuota = value;
                if (_selectedLvQuota != null)
                {
                    _listOfLvQuotaEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.EmpID == _selectedLvQuota.EmpID));
                    base.OnPropertyChanged("ListOfDesgEmps");
                    base.OnPropertyChanged("selectedLvQuota");
                    base.OnPropertyChanged("isEnabled");
                }
                
            }
        }

        public ObservableCollection<LvQuota> listOfLvQuotas
        {
            get { return _listOfLvQuotas; }

            set
            {
                listOfLvQuotas = value;
                OnPropertyChanged("listOfLvQuotas");
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
            _selectedLvQuota = new LvQuota();

            _listOfLvQuotas = new ObservableCollection<LvQuota>(entity.LvQuotas.ToList());
            _selectedLvQuota = entity.LvQuotas.ToList().FirstOrDefault();
            
            _listOfLvQuotaEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.EmpID == _selectedLvQuota.EmpID));
            this._AddCommand = new AddCommandLvQuota(_selectedLvQuota);
            this._EditCommand = new EditCommandLvQuota(this);
            this._DeleteCommand = new DeleteCommandLvQuota(_selectedLvQuota);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLvQuota(this);
            base.OnPropertyChanged("_listOfLvQuotas");
            base.OnPropertyChanged("_listOfLvQuotaEmps");
            base.OnPropertyChanged("_selectedLvQuota");
        }
        #endregion
    }
}
