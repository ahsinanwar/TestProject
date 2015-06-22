using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMUser.Commands;

namespace TimeAttendanceSystem.ViewModels.VMUser
{
    class VMUser:ObservableObject
    {
        #region Intialization
        public User _selectedUser;
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
        private ObservableCollection<User> _listOfUsers;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public User selectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                this.isEnabled = false;
                _selectedUser = value;
                base.OnPropertyChanged("selectedUser");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<User> listOfUsers
        {
            get { return _listOfUsers; }

            set
            {
                listOfUsers = value;
                OnPropertyChanged("listOfUsers");
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
        public VMUser()
        {
            entity = new TAS2013Entities();
            _selectedUser = new User();
            _listOfUsers = new ObservableCollection<User>(entity.Users.ToList());
            _selectedUser = entity.Users.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandUser(_selectedUser);
            this._EditCommand = new EditCommandUser(this);
            this._DeleteCommand = new DeleteCommandUser(_selectedUser);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandUser(this);
            base.OnPropertyChanged("_listOfUsers");
        }
        #endregion  
    }
}
