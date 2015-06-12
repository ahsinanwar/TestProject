﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMReader.Commands;

namespace TimeAttendanceSystem.ViewModels.VMReader
{
    class VMReader: ObservableObject
    {
        #region Intialization
        public Reader _selectedRdr;
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
        private ObservableCollection<Reader> _listOfRdrs;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
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
                base.OnPropertyChanged("selectedRdr");
                base.OnPropertyChanged("isEnabled");

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
        public VMReader()
        {
            entity = new TAS2013Entities();
            _selectedRdr = new Reader();
            _listOfRdrs = new ObservableCollection<Reader>(entity.Readers.ToList());
            _selectedRdr = entity.Readers.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandRdr(_selectedRdr);
            this._EditCommand = new EditCommandRdr(this);
            this._DeleteCommand = new DeleteCommandRdr(_selectedRdr);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandRdr(this);
            base.OnPropertyChanged("_listOfRdrs");
        }
        #endregion  
    }
}
