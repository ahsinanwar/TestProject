using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMLvApplication.Commands;

namespace TimeAttendanceSystem.ViewModels.VMLvApplication
{
    class VMLvApplication: ObservableObject
    {
        #region Intialization
        private ObservableCollection<CombinedEmpAndLvApps> _listOfEmpsAndLvApps;
        private CombinedEmpAndLvApps _selectedEmpAndLvApp;
        private ObservableCollection<LvType> _listOfLvTypes;
        public CombinedEmpAndLvApps selectedEmpAndLvApp
        {
            get
            {
               
                return _selectedEmpAndLvApp;
                
            }
            set
            {

                _selectedEmpAndLvApp = value;
                
                base.OnPropertyChanged("selectedEmpAndLvApp");
              

            }
        }
        public ObservableCollection<LvType> listOfLvTypes
        {
            get { return _listOfLvTypes; }
            set
            {
                _listOfLvTypes = value;
                base.OnPropertyChanged("listOfLvTypes");
            }
        }
        public ObservableCollection<CombinedEmpAndLvApps> listOfEmpsAndLvApps
         {
            get
            {
                return _listOfEmpsAndLvApps;
            }
            set
            {
                this.isEnabled = false;
                _listOfEmpsAndLvApps = value;
                base.OnPropertyChanged("isEnabled");
                base.OnPropertyChanged("listOfEmpsAndLvApps");

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
        public VMLvApplication()
        {
            entity = new TAS2013Entities();
            _selectedEmpAndLvApp = new CombinedEmpAndLvApps();
            _listOfEmpsAndLvApps = new ObservableCollection<CombinedEmpAndLvApps>();
            _listOfLvTypes = new ObservableCollection<LvType>(entity.LvTypes.ToList());
           
            List<LvApplication> LvAppDatacollection = entity.LvApplications.ToList();// 1 2 3
            foreach (LvApplication value in LvAppDatacollection)
                _listOfEmpsAndLvApps.Add(new CombinedEmpAndLvApps(entity.Emps.FirstOrDefault(aa => aa.EmpID == value.EmpID), value,entity.LvTypes.FirstOrDefault(aa => aa.LvTypeID == value.TypeID)));
            _selectedEmpAndLvApp = _listOfEmpsAndLvApps.FirstOrDefault();
            this._AddCommand = new AddCommandLvApp(_selectedEmpAndLvApp);
            this._EditCommand = new EditCommandLvApp(this);
            this._DeleteCommand = new DeleteCommandLvApp(_selectedEmpAndLvApp);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLvApp(this);
            base.OnPropertyChanged("_listOfEmpsAndLvApps");
            base.OnPropertyChanged("_selectedEmpAndLvApp");
        }
        #endregion  
    }
}
