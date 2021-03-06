﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMSection.Commands;

namespace TimeAttendanceSystem.ViewModels.VMSection
{
    class VMSection : ObservableObject
    {
        #region Intialization
        public Section _selectedSec;
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
        private ObservableCollection<Section> _listOfSecs;
        private ObservableCollection<Emp> _listOfShiftEmps;
        public ObservableCollection<Emp> ListOfShiftEmps
        {

            get { return _listOfShiftEmps; }
            set
            {

                _listOfShiftEmps = value;

                base.OnPropertyChanged("ListOfShiftEmps");
                base.OnPropertyChanged("isEnabled");
            }
        }
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;
     
        public Section selectedSec
        {
            get
            {
                return _selectedSec;
            }
            set
            {
                this.isEnabled = false;
                _selectedSec = value;
                _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.SecID == _selectedSec.SectionID));
                base.OnPropertyChanged("ListOfShiftEmps");
                base.OnPropertyChanged("selectedSec");
                base.OnPropertyChanged("isEnabled");

            }
        }
        private ObservableCollection<Department> _listOfDepts;
        public ObservableCollection<Department> listOfDepts
        {
            get { return _listOfDepts; }

            set
            {
                listOfDepts = value;
                OnPropertyChanged("listOfDepts");
            }
        }
        public ObservableCollection<Section> listOfSecs
        {
            get { return _listOfSecs; }

            set
            {
                listOfSecs = value;
                OnPropertyChanged("listOfSecs");
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
        public VMSection()
        {
            entity = new TAS2013Entities();
            _selectedSec = new Section();
            _listOfSecs = new ObservableCollection<Section>(entity.Sections.ToList());
            _selectedSec = entity.Sections.ToList().FirstOrDefault();
            _listOfDepts = new ObservableCollection<Department>(entity.Departments.ToList());
            _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.SecID == _selectedSec.SectionID));
            this._AddCommand = new AddCommandSec(_selectedSec);
            this._EditCommand = new EditCommandSec(this);
            this._DeleteCommand = new DeleteCommandSec(_selectedSec);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandSec(this);
            base.OnPropertyChanged("_listOfSecs");
            base.OnPropertyChanged("_listOfDepts");
            base.OnPropertyChanged("_listOfShiftEmps");
        }
        #endregion
    }
}
