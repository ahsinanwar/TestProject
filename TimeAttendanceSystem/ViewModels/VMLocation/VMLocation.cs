﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMLocation.Commands;

namespace TimeAttendanceSystem.ViewModels.VMLocation
{
    class VMLocation : ObservableObject
    {
        #region Intialization
        public Location _selectedLoc;
        public City _selectedCity;
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
        private ObservableCollection<Location> _listOfLocs;
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

                base.OnPropertyChanged("isEnabled");
            }
        }
        private ObservableCollection<City> _listOfCities;
        public Location selectedLoc
        {
            get
            {
                return _selectedLoc;
            }
            set
            {
                this.isEnabled = false;
                _selectedLoc = value;
                _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.LocID == _selectedLoc.LocID));
                base.OnPropertyChanged("ListOfShiftEmps");
                base.OnPropertyChanged("selectedLoc");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<Location> listOfLocs
        {
            get { return _listOfLocs; }

            set
            {
                listOfLocs = value;
                OnPropertyChanged("listOfLocs");
            }
        }
        public ObservableCollection<City> listOfCities
        {
            get { return _listOfCities; }

            set
            {
                listOfCities = value;
                OnPropertyChanged("listOfCities");
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
        public VMLocation()
        {
            entity = new TAS2013Entities();
            _selectedLoc = new Location();
         
            _listOfLocs = new ObservableCollection<Location>(entity.Locations.ToList());
            _selectedLoc = entity.Locations.ToList().FirstOrDefault();
            _listOfCities = new ObservableCollection<City>(entity.Cities.ToList());
            _selectedCity = entity.Cities.ToList().FirstOrDefault();
            _listOfShiftEmps = new ObservableCollection<Emp>(entity.Emps.Where(aa => aa.LocID == _selectedLoc.LocID));
            this._AddCommand = new AddCommandLoc(_selectedLoc);
            this._EditCommand = new EditCommandLoc(this);
            this._DeleteCommand = new DeleteCommandLoc(_selectedLoc);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandLoc(this);
            base.OnPropertyChanged("_listOfLocs");
            base.OnPropertyChanged("_listOfCities");
        }
        #endregion
    }
}
