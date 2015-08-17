using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.ViewModels.VMDatabase.Commands;

namespace TimeAttendanceSystem.ViewModels.VMDatabase
{
    class VMDataBase : ObservableObject
    {
        private String _serverName;
        private String _userName;
        private String _password;
        private String _databaseName;
        public ICommand _SaveCommand { get; set; }
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }

        }
        public String serverName
        {
            get { return _serverName; }
            set { 
                _serverName = value;
                base.OnPropertyChanged("serverName");
  
             }

        
            
        }
        public String userName
            {
            
            get{return _userName;}
                set{
                    _userName = value;
                    base.OnPropertyChanged("userName");
                }
            }

        public String password
        {

            get {
                return _password;
            }
            set {

                _password = value;
                base.OnPropertyChanged("password");
            }
        
        
        }

        public String databaseName
        {

            get { return _databaseName; }
            set {
                _databaseName = value;
                base.OnPropertyChanged("databaseName");
            }
        
        
        }

        public VMDataBase()
        {

            this._SaveCommand = new SaveCommandDatabase(this);
        
        }
    }
}
