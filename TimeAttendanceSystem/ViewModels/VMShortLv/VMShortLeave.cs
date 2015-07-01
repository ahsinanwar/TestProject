using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMShortLv.Commands;

namespace TimeAttendanceSystem.ViewModels.VMShortLv
{
    class VMShortLeave :ObservableObject
    {
        #region Intialization
        private ObservableCollection<CombinedEmpAndShortLvcs> _listOfEmpsAndShortLv;
        private CombinedEmpAndShortLvcs _selectedEmpAndShortLv;
        public CombinedEmpAndShortLvcs selectedEmpAndShortLv
        {
            get
            {
                return _selectedEmpAndShortLv;
            }
            set
            {
              
                _selectedEmpAndShortLv = value;
                base.OnPropertyChanged("selectedEmpAndShortLv");


            }
        }
        public ObservableCollection<CombinedEmpAndShortLvcs> listOfEmpsAndShortLv
        {
            get
            {
                return _listOfEmpsAndShortLv;
            }
            set
            {
                this.isEnabled = false;
                _listOfEmpsAndShortLv = value;
                base.OnPropertyChanged("isEnabled");
                base.OnPropertyChanged("listOfEmpsAndShortLv");

            }
        }
        
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
       
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;
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
        public VMShortLeave()
        {
            entity = new TAS2013Entities();
            _selectedEmpAndShortLv = new CombinedEmpAndShortLvcs();
            _listOfEmpsAndShortLv = new ObservableCollection<CombinedEmpAndShortLvcs>();
            List<LvShort> LvShortDatacollection = entity.LvShorts.ToList();// 1 2 3
            foreach (LvShort value in LvShortDatacollection)
                   _listOfEmpsAndShortLv.Add(new CombinedEmpAndShortLvcs(entity.Emps.FirstOrDefault(aa => aa.EmpID == value.EmpID), value));
            _selectedEmpAndShortLv = _listOfEmpsAndShortLv.FirstOrDefault();
            this._AddCommand = new AddCommandLvShort(_selectedEmpAndShortLv);
            this._EditCommand = new EditCommandLvShort(this);
            this._DeleteCommand = new DeleteCommandLvShort(_selectedEmpAndShortLv);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLvShort(this);
            base.OnPropertyChanged("_listOfEmpsAndShortLv");
            base.OnPropertyChanged("_selectedEmpAndShortLv");
        }
        #endregion  
    }
}
