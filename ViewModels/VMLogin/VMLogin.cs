using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMLogin.Commands;

namespace TimeAttendanceSystem.ViewModels.VMLogin
{
    class VMLogin : ObservableObject
    {
        #region Intialization
        private UserCredentials _uc;
        public UserCredentials Uc
        {
            get { return _uc; }
            set{
                Uc = value;
                base.OnPropertyChanged("Uc");
            }
    
    }
        public ICommand _LoginCommand;
        public ICommand _ExitCommand;
       
       
        private ObservableCollection<Location> _listOfLocs;
        
        TAS2013Entities entity;
       
       
        public ObservableCollection<Location> listOfLocs
        {
            get { return _listOfLocs; }

            set
            {
                listOfLocs = value;
                OnPropertyChanged("listOfLocs");
            }
        }
       
        #endregion

        #region ICommands
        public ICommand loginCommand
        {
            get
            {
                return _LoginCommand;
            }
        }

        public ICommand exitCommand
        {
            get
            {
                return _ExitCommand;
            }
        }

       
        #endregion

        #region constructor
        public VMLogin()
        {
            entity = new TAS2013Entities();
            _uc = new UserCredentials();
            _LoginCommand = new LoginCommand();
            _ExitCommand = new ExitCommand();
            base.OnPropertyChanged("_uc");
           
        }
        #endregion
    }
}
