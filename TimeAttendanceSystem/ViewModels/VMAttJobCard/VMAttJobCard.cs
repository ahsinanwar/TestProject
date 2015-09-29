using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMAttJobCard
{
    public class VMAttJobCard : ObservableObject
    {
        #region Intialization
        public AttData _selectedAttData;
        public JobCardApp _selectedJobCardApp;
        public AttData _attDataShow;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        public AttData AttDataShow
        {
            get { return _attDataShow; }
            set
            {

                _attDataShow = value;
                OnPropertyChanged("AttDataShow");
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
        private ObservableCollection<AttData> _listOfAttData;
        private ObservableCollection<JobCard> _listOfJobCards;
        private ObservableCollection<JobCardApp> _listofJobCardApps;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        TAS2013Entities entity;

        public AttData selectedAttData
        {
            get
            {
                return _selectedAttData;
            }
            set
            {
                this.isEnabled = false;
                _selectedAttData = value;
                AttDataShow = new AttData();
                AttDataShow = value;
                // base.OnPropertyChanged("selectedAttData");
                base.OnPropertyChanged("AttDataShow");
                base.OnPropertyChanged("isEnabled");

            }
        }

        public ObservableCollection<AttData> listOfAttData
        {
            get { return _listOfAttData; }

            set
            {
                listOfAttData = value;
                OnPropertyChanged("listOfAttData");
            }
        }
        public ObservableCollection<JobCard> listOfJobCards
        {
            get { return _listOfJobCards; }

            set
            {
                _listOfJobCards = value;
                OnPropertyChanged("_listOfJobCards");
            }
        }
        public ObservableCollection<JobCardApp> listofJobCardApps
        {
            get { return _listofJobCardApps; }

            set
            {
                _listofJobCardApps = value;
                OnPropertyChanged("listofJobCardApps");
            }
        }
        #endregion

        #region ICommands

        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand;
            }

        }
        #endregion

        #region constructor
        public VMAttJobCard()
        {
            entity = new TAS2013Entities();
            Emp sd = new Emp();
            _attDataShow = new AttData();
            _selectedAttData = new AttData();
            DateTime date = new DateTime(2015, 03, 15);
            _listofJobCardApps = new ObservableCollection<JobCardApp>(entity.JobCardApps.ToList());
           
            _listOfAttData = new ObservableCollection<AttData>(entity.AttDatas.Where(aa => aa.AttDate == date).ToList());
            _selectedAttData = entity.AttDatas.ToList().FirstOrDefault();
            _attDataShow = entity.AttDatas.ToList().FirstOrDefault();
            _listOfJobCards = new ObservableCollection<JobCard>(entity.JobCards.ToList());
            this._isAdding = false;
            this._isEnabled = false;
            //this._SaveCommand = new SaveCommandAttEdit(this);
            base.OnPropertyChanged("_listOfJobCards");
        }
        #endregion
    }
}
