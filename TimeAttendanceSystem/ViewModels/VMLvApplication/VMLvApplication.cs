﻿using System;
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
        public LvApplication _selectedLvApp;
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
        private ObservableCollection<LvApplication> _listOfLvApps;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public LvApplication selectedLvApp
        {
            get
            {
                return _selectedLvApp;
            }
            set
            {
                this.isEnabled = false;
                _selectedLvApp = value;
                base.OnPropertyChanged("selectedLvApp");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<LvApplication> listOfLvApps
        {
            get { return _listOfLvApps; }

            set
            {
                listOfLvApps = value;
                OnPropertyChanged("_listOfLvApps");
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
        public VMLvApplication()
        {
            entity = new TAS2013Entities();
            _selectedLvApp = new LvApplication();
            _listOfLvApps = new ObservableCollection<LvApplication>(entity.LvApplications.ToList());
            _selectedLvApp = entity.LvApplications.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandLvApp(_selectedLvApp);
            this._EditCommand = new EditCommandLvApp(this);
            this._DeleteCommand = new DeleteCommandLvApp(_selectedLvApp);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLvApp(this);
            base.OnPropertyChanged("_listOfLvApps");
        }
        #endregion  
    }
}
