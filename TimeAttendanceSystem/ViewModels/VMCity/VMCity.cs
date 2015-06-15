using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMCity.Commands;

namespace TimeAttendanceSystem.ViewModels.VMCity
{
    public class VMCity: ObservableObject
    {
        #region Intialization
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
        private ObservableCollection<City> _listOfCities;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public City selectedCity
        {
            get
            {
                return _selectedCity;
            }
            set
            {
                this.isEnabled = false;
                _selectedCity = value;
                base.OnPropertyChanged("selectedCity");
                base.OnPropertyChanged("isEnabled");

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
        public VMCity()
        {
            entity = new TAS2013Entities();
            _selectedCity = new City();
            _listOfCities = new ObservableCollection<City>(entity.Cities.ToList());
            _selectedCity = entity.Cities.ToList().FirstOrDefault();
            this._AddCommand = new AddCommandCity(_selectedCity);
            this._EditCommand = new EditCommandCity(this);
            this._DeleteCommand = new DeleteCommandCity(_selectedCity);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandCity(this);
            base.OnPropertyChanged("listOfCities");
        }
        #endregion
    }
}
