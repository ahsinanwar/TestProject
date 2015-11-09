using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TASDownloadService.Helper;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMReader.Commands;

namespace TimeAttendanceSystem.ViewModels.VMReader
{
    class VMReader: ObservableObject
    {
        #region Intialization
        public Reader _selectedRdr;
        public Boolean _isEnabled = false;
        Downloader downloadhelper = new Downloader();
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
        private ObservableCollection<Emp> _listOfShiftEmps;
        public ObservableCollection<Emp> listOfShiftEmps
        {
            get { return _listOfShiftEmps; }

            set
            {
                _listOfShiftEmps = value;

                OnPropertyChanged("listOfShiftEmps");
            }
        
        
        }
        private ObservableCollection<Reader> _listOfRdrs;
        private ObservableCollection<Emp> _listOfRdrEmps;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }

        public ObservableCollection<Emp> listOfRdrEmps
        {
            get { return _listOfRdrEmps; }

            set
            {
                _listOfRdrEmps = value;
                OnPropertyChanged("listOfRdrEmps");
            }
        }
        TAS2013Entities entity;

        public Reader selectedRdr
        {
            get
            {
                return _selectedRdr;
            }
            set
            {
                this.isEnabled = false;
                _selectedRdr = value;

                GetUsrsFromSelectedReader();

            }
        }

        public ObservableCollection<Reader> listOfRdrs
        {
            get { return _listOfRdrs; }

            set
            {
                listOfRdrs = value;
                OnPropertyChanged("listOfRdrs");
            }
        }

        private ObservableCollection<Location> _listOfLocs;
        public ObservableCollection<Location> listOfLocs
        {
            get { return _listOfLocs; }

            set
            {
                listOfLocs = value;
                OnPropertyChanged("listOfLocs");
            }
        }

        private ObservableCollection<RdrDutyCode> _listOfDutyCodes;
        public ObservableCollection<RdrDutyCode> listOfDutyCodes
        {
            get { return _listOfDutyCodes; }

            set
            {
                listOfDutyCodes = value;
                OnPropertyChanged("listOfDutyCodes");
            }
        }

        private ObservableCollection<ReaderType> _listOfRdrTypes;
        public ObservableCollection<ReaderType> listOfRdrTypes
        {
            get { return _listOfRdrTypes; }

            set
            {
                listOfRdrTypes = value;
                OnPropertyChanged("listOfRdrTypes");
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
        public void GetUsrsFromSelectedReader()

        {
            listOfShiftEmps = new ObservableCollection<Emp>();
            if (_selectedRdr != null)
            {
                //the function returns a list of FPids fetched from the Reader, if the reader 
                //doesnt connect the list is returned with a single element name "NA"
                //if we get NA a pop should show saying can't connect to the reader

                List<string> listOfFpID = downloadhelper.GetFpIDFromDevice(_selectedRdr);
                if (listOfFpID.Count() == 1 && listOfFpID[0] == "NA")

                    PopUp.popUp("Reader", "The Reader " + _selectedRdr.RdrName + " is unpingable", NotificationType.Warning);

                else
                {
                    foreach (string fpid in listOfFpID)
                    {
                        Emp emp = new Emp();
                        int FpId = Convert.ToInt32(fpid);
                        emp = entity.Emps.Where(aa => aa.FpID == FpId).FirstOrDefault();
                        if (emp != null)
                            listOfRdrEmps.Add(emp);


                    }

                    base.OnPropertyChanged("listOfRdrEmps");
                    base.OnPropertyChanged("selectedRdr");
                    base.OnPropertyChanged("isEnabled");
                }

            }

        
        }
        #region constructor
        public VMReader()
        {
            entity = new TAS2013Entities();
            _selectedRdr = new Reader();
            _listOfRdrs = new ObservableCollection<Reader>(entity.Readers.ToList());
            _selectedRdr = entity.Readers.ToList().FirstOrDefault();

            _listOfRdrEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.ReaderID == _selectedRdr.RdrID).ToList());
            GetUsrsFromSelectedReader();
            _listOfLocs = new ObservableCollection<Location>(entity.Locations.ToList());
            _listOfDutyCodes = new ObservableCollection<RdrDutyCode>(entity.RdrDutyCodes.ToList());
            _listOfRdrTypes = new ObservableCollection<ReaderType>(entity.ReaderTypes.ToList());
            this._AddCommand = new AddCommandRdr(_selectedRdr);
            this._EditCommand = new EditCommandRdr(this);
            this._DeleteCommand = new DeleteCommandRdr(_selectedRdr);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandRdr(this);
            base.OnPropertyChanged("_listOfRdrs");
            base.OnPropertyChanged("_listOfLocs");
            base.OnPropertyChanged("_listOfDutyCodes");
            base.OnPropertyChanged("_listOfRdrTypes");
        }
        #endregion  
    }
}
