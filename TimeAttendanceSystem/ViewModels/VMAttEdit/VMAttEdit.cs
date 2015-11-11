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
        //public AttData _selectedAttData;
        public AttData _attDataShow;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        private string _selectedEmpNo;
        private Emp _selectedEmp = new Emp();
        private DateTime _selectedDate = new DateTime();
        public AttData AttDataShow
        {
            get { return _attDataShow; }
            set {
                if (value != null)
                {
                    if (context.AttDatas.Where(aa => aa.AttDate == value.AttDate && aa.EmpDate == value.EmpDate).Count() > 0)
                        _attDataShow = value;
                    //listOfAttData = new ObservableCollection<AttData>(context.AttDatas.Where(aa => aa.AttDate == _attDataShow.AttDate).ToList());


                    if (AttDataShow.AttDate != null)
                    {
                        if (AttDataShow.TimeIn == null)
                            AttDataShow.TimeIn = (DateTime)AttDataShow.AttDate;
                        if (AttDataShow.TimeOut == null)
                            AttDataShow.TimeOut = (DateTime)AttDataShow.AttDate;
                        
                    }
                    OnPropertyChanged("AttDataShow");
                    OnPropertyChanged("listOfAttData"); 
                }
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

        public string selectedEmpNo
        {
            get
            {
                return _selectedEmpNo;
            }
            set
            {
                _selectedEmpNo = value;
                selectedEmp = context.Emps.FirstOrDefault(aa => aa.EmpNo == value);
                base.OnPropertyChanged("selectedEmpNo");
            }
        }
        public Emp selectedEmp
        {
            get
            {
                return _selectedEmp;
            }
            set
            {
                _selectedEmp = value;
                base.OnPropertyChanged("selectedEmp");
            }
        }

        public DateTime selectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                base.OnPropertyChanged("selectedDate");
            }
        }
        private ObservableCollection<AttData> _listOfAttData;
        private ObservableCollection<DutyCode> _listOfDutyCodes;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        public ICommand _SearchCommand { get; set; }
        TAS2013Entities context;

        //public AttData selectedAttData
        //{
        //    get
        //    {
        //        return _selectedAttData;
        //    }
        //    set
        //    {
        //        this.isEnabled = false;
        //        _selectedAttData = value;
        //        AttDataShow = new AttData();
        //        AttDataShow = value;
        //        base.OnPropertyChanged("selectedAttData");
        //        base.OnPropertyChanged("AttDataShow");
        //        base.OnPropertyChanged("isEnabled");
        //    }
        //}

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
            context = new TAS2013Entities();
            Emp sd = new Emp();
            _attDataShow = new AttData();

            //_listOfAttData = new ObservableCollection<AttData>(context.AttDatas.Where(aa => aa.AttDate == _attDataShow.AttDate).OrderBy(aa => aa.AttDate).ToList());
            DateTime LatestProcessedDate = context.AttDatas.Max(r => r.AttDate).Value;
            _listOfAttData = new ObservableCollection<AttData>(context.AttDatas.Where(cs => cs.AttDate == LatestProcessedDate).ToList());

            _attDataShow = _listOfAttData.FirstOrDefault();
            //_selectedAttData = new AttData();

           
            //_selectedAttData = context.AttDatas.ToList().FirstOrDefault();


           //  DateTime date = (DateTime)AttDataShow.AttDate;

             //_listOfAttData = new ObservableCollection<AttData>(context.AttDatas.Where(aa => aa.AttDate == _selectedAttData.AttDate).ToList());
             
            
            _listOfDutyCodes = new ObservableCollection<DutyCode>(context.DutyCodes.ToList());
            this._EditCommand = new EditCommandAttData(this);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandAttEdit(this);
            this._SearchCommand = new SearchCommandAttEdit(this);

            selectedDate = (DateTime)AttDataShow.AttDate;
            selectedEmp = AttDataShow.Emp;
            selectedEmpNo = selectedEmp.EmpNo;

            base.OnPropertyChanged("_attDataShow");
            base.OnPropertyChanged("_listOfAttData");
            base.OnPropertyChanged("_listOfdutyCodes");
        }
        #endregion
    }
}
