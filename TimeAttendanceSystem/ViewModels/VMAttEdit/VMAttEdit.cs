using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMAttEdit.Commands;

namespace TimeAttendanceSystem.ViewModels.VMAttEdit
{
    public class VMAttEdit:ObservableObject
    {
         #region Intialization
        public AttData _selectedAttData;
        public AttData _attDataShow;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        public AttData AttDataShow
        {
            get { return _attDataShow; }
            set {
                AttData temp = new AttData();
                if (entity.AttDatas.Where(aa => aa.AttDate == value.AttDate && aa.EmpDate == value.EmpDate).Count() > 0)
                    _attDataShow = entity.AttDatas.Where(aa => aa.AttDate == value.AttDate && aa.EmpDate == value.EmpDate).FirstOrDefault();
                _attDataShow = value;
                listOfAttData = new ObservableCollection<AttData>(entity.AttDatas.Where(aa => aa.AttDate == _attDataShow.AttDate).ToList());
                OnPropertyChanged("AttDataShow");
                OnPropertyChanged("listOfAttData");

            }
        
        }
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
        private ObservableCollection<AttData> _listOfAttData;
        private ObservableCollection<DutyCode> _listOfDutyCodes;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        public ICommand _SearchCommand { get; set; }
        TAS2013Entities entity;

        public AttData selectedAttData
        {
            get
            {
                return _selectedAttData;
            }
            set
            {
                this.isEnabled = false;
                _selectedAttData = value;
                AttDataShow = new AttData();
                AttDataShow = value;
                base.OnPropertyChanged("selectedAttData");
                base.OnPropertyChanged("AttDataShow");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<AttData> listOfAttData
        {
            get { return _listOfAttData; }

            set
            {
                _listOfAttData = value;
                OnPropertyChanged("listOfAttData");
            }
        }
        public ObservableCollection<DutyCode> listOfDutyCodes
        {
            get { return _listOfDutyCodes; }

            set
            {
                _listOfDutyCodes = value;
                OnPropertyChanged("listOfDutyCodes");
            }
        }
        #endregion

        #region ICommands
        public ICommand SearchCommand
        {
            get { return _SearchCommand; }
            
        
        }
        public ICommand EditCommand
        {
            get
            {
                return _EditCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }

        }
        #endregion

        #region constructor
        public VMAttEdit()
        {
            entity = new TAS2013Entities();
            Emp sd = new Emp();
            _attDataShow = new AttData();
            _selectedAttData = new AttData();

           
            _selectedAttData = entity.AttDatas.ToList().FirstOrDefault();
           //  DateTime date = (DateTime)AttDataShow.AttDate;
             _listOfAttData = new ObservableCollection<AttData>(entity.AttDatas.Where(aa => aa.AttDate == _selectedAttData.AttDate).ToList());
            _attDataShow = entity.AttDatas.ToList().FirstOrDefault();
            _listOfDutyCodes = new ObservableCollection<DutyCode>(entity.DutyCodes.ToList());
            this._EditCommand = new EditCommandAttData(this);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandAttEdit(this);
            this._SearchCommand = new SearchCommandAttEdit(this);
            base.OnPropertyChanged("_attDataShow");
            base.OnPropertyChanged("_listOfAttData");
            base.OnPropertyChanged("_listOfdutyCodes");
        }
        #endregion
    }
}
