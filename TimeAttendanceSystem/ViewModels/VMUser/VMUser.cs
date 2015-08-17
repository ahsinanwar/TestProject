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
        public Emp _dummyEmp;
        public User _selectedUser;
        private String _selectedUserRole;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        public Boolean _isChecked;
        public String selectedUserRole
        { get { return _selectedUserRole; }
            set {
                _selectedUserRole = value;
                OnPropertyChanged("selectedUserRole");
            }
        }
        public Boolean IsChecked
        {
            get { return _isChecked; }
            set
            {

                _isChecked = value;
                OnPropertyChanged("IsChecked");
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
        private ObservableCollection<User> _listOfUsers;
        private ObservableCollection<Emp> _listOfEmps;
       
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
        private ObservableCollection<String> _listOfUserRoles;
        public ObservableCollection<String> listOfUserRoles
        {
            get { return _listOfUserRoles; }
            set
            {
                _listOfUserRoles = value;
                OnPropertyChanged("listOfUsersRoles");
           
            }
        }

        public ObservableCollection<User> listOfUsers
        {
            get { return _listOfUsers; }

            set
            {
                _listOfUsers = value;
                OnPropertyChanged("listOfUsers");
            }
        }

        public ObservableCollection<Emp> listOfEmps
        {
            get
            {
                return _listOfEmps;
            }

            set
            {
                _listOfEmps = value;
                OnPropertyChanged("listOfEmps");
            }
        }
        public Emp dummyEmp
        {
            get
            {
                return _dummyEmp;
            }
            set
            {
                this.isEnabled = false;
                _dummyEmp = value;
                base.OnPropertyChanged("dummyEmp");
                base.OnPropertyChanged("isEnabled");

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
            List<UserRole> forRadComboBox = new List<UserRole>();
            forRadComboBox= entity.UserRoles.ToList();
            _listOfUserRoles = new ObservableCollection<string>();
           foreach(UserRole Rd in forRadComboBox)
           {
               _listOfUserRoles.Add(Rd.RoleName);
           }
            _listOfUsers = new ObservableCollection<User>(entity.Users.ToList());
            _selectedUser = entity.Users.ToList().FirstOrDefault();
            _listOfEmps = new ObservableCollection<Emp>(entity.Emps.ToList());
          
            this._AddCommand = new AddCommandUser(_selectedUser);
            this._EditCommand = new EditCommandUser(this);
            this._DeleteCommand = new DeleteCommandUser(_selectedUser);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandUser();
            base.OnPropertyChanged("_listOfUsers");
            base.OnPropertyChanged("_listOfUserRoles");
            base.OnPropertyChanged("_selectedUser");
        }
        #endregion 
        internal void raiseEmpChange()
        {

            base.OnPropertyChanged("dummyEmp");
        }
    }
}
