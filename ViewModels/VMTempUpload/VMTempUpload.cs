using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMTempUpload
{
    public class VMTempUpload: ObservableObject
    {
        #region Intialization
        
        private ObservableCollection<Reader> _listOfRdrs;
        
        TAS2013Entities entity;
        public List<Reader> _selectedRdr;
        public List<Reader> selectedRdr
        {
            get
            {
                return _selectedRdr;
            }
            set
            {
                _selectedRdr = value;
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

       

        #region constructor
        public VMTempUpload()
        {
            entity = new TAS2013Entities();
            selectedRdr = new List<Reader>();
            _listOfRdrs = new ObservableCollection<Reader>(entity.Readers.ToList());
            base.OnPropertyChanged("_listOfRdrs");
        }
        #endregion  
    }
}
