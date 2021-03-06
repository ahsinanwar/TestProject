﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDivision.Commands;

namespace TimeAttendanceSystem.ViewModels.VMDivision
{
    class VMDivision : ObservableObject
    {
        
        #region Intialization
        public Division _selectedDiv;
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
        private ObservableCollection<Division> _listOfDivs;
        private ObservableCollection<Emp> _listOfDivEmps;
        public ObservableCollection<Emp> ListOfDivEmps
        {

            get { return _listOfDivEmps; }
            set
            {

                _listOfDivEmps = value;

                base.OnPropertyChanged("ListOfDivEmps");
                base.OnPropertyChanged("isEnabled");
            }
        }
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public Division selectedDiv
        {
            get
            {
                return _selectedDiv;
            }
            set
            {
                this.isEnabled = false;
                _selectedDiv = value;
                _listOfDivEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.Section.Department.DivID == _selectedDiv.DivisionID));
                base.OnPropertyChanged("selectedDiv");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Division> listOfDivs
        {
            get { return _listOfDivs; }

            set
            {
                listOfDivs = value;
                OnPropertyChanged("listOfDivs");
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
        public VMDivision()
        {
            entity = new TAS2013Entities();
            _selectedDiv = new Division();
            
            _listOfDivs = new ObservableCollection<Division>(entity.Divisions.ToList());
            _selectedDiv = entity.Divisions.ToList().FirstOrDefault();
            _listOfDivEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.Section.Department.DivID == _selectedDiv.DivisionID));
            this._AddCommand = new AddCommandDiv(_selectedDiv);
            this._EditCommand = new EditCommandDiv(this);
            this._DeleteCommand = new DeleteCommandDiv(_selectedDiv);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandDiv(this);
            base.OnPropertyChanged("_listOfDivs");
        }
        #endregion

    }
}
