﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMCrew.Commands;

namespace TimeAttendanceSystem.ViewModels.VMCrew
{
    class VMCrew :ObservableObject
    {
        #region Intialization
        public Crew _selectedCrew;
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
        private ObservableCollection<Crew> _listOfcrews;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;
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

        public Crew selectedCrew
        {
            get
            {
                return _selectedCrew;
            }
            set
            {
                this.isEnabled = false;
                _selectedCrew = value;
                _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.CrewID == _selectedCrew.CrewID));
                base.OnPropertyChanged("ListOfShiftEmps");
                base.OnPropertyChanged("selectedCrew");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Crew> listOfCrews
        {
            get { return _listOfcrews; }

            set
            {
                listOfCrews = value;
                OnPropertyChanged("listOfCrews");
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
        public VMCrew()
        {
            entity = new TAS2013Entities();
            _selectedCrew = new Crew();
            _listOfcrews = new ObservableCollection<Crew>(entity.Crews.ToList());
            _selectedCrew = entity.Crews.ToList().FirstOrDefault();
            _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.CrewID == _selectedCrew.CrewID));
            this._AddCommand = new AddCommandCrew(_selectedCrew);
            this._EditCommand = new EditCommandCrew(this);
            this._DeleteCommand = new DeleteCommandCrew(_selectedCrew);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandCrew(this);
            base.OnPropertyChanged("_listOfCrew");
        }
        #endregion
    }
}
