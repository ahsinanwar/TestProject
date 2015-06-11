using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvQuota
{
    class VMLvQuota: ObservableObject
    {
        #region Intialization
        public LvQuota _selectedLvQuota;
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
        private ObservableCollection<LvQuota> _listOfLvQuotas;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

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
                base.OnPropertyChanged("selectedLvQuota");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<LvQuota> listOfLvQuotas
        {
            get { return _listOfLvQuotas; }

            set
            {
                listOfLvQuotas = value;
                OnPropertyChanged("_listOfLvQuotas");
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
            this._AddCommand = new AddCommand(_selectedLvQuota);
            this._EditCommand = new EditCommand(this);
            this._DeleteCommand = new DeleteCommand(_selectedLvQuota);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommand(this);
            base.OnPropertyChanged("_listOfQuotas");
        }
        #endregion  
    }
}
