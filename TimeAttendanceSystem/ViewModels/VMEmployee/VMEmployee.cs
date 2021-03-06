﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.QueryBuilders;
using TimeAttendanceSystem.ViewModels.VMEmployee.Commands;

namespace TimeAttendanceSystem.ViewModels.VMEmployee
{
    class VMEmployee: ObservableObject
    {
        #region Intialization
        public Emp _selectedEmp;
        public Emp _dummyEmp;
        private Boolean _isChecked;
        public Category _selectedCat;
        public Section _selectedSec;
        public Department _selectedDept;
        public Grade _selectedGrade;
        public Boolean _isEnabled = false;
        public Boolean _isAdding = false;
        public Boolean IsChecked
        {
            get { return _isChecked; }
            set {

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
        private ObservableCollection<Category> _listOfCats;
        private ObservableCollection<JobTitle> _listOfJobs;
        private ObservableCollection<EmpType> _listOfEmpTypes;
        private ObservableCollection<Designation> _listOfDesgs;
        private ObservableCollection<Grade> _listOfGrades;
        private ObservableCollection<string> _listOfGenders;
        private ObservableCollection<Shift> _listOfShifts;
        private ObservableCollection<Department> _listOfDepts;
        private ObservableCollection<Location> _listOfLocs;
        private ObservableCollection<string> _listOfMarried;
        private ObservableCollection<Section> _listOfSecs;
        private ObservableCollection<Crew> _listOfCrews;
        private ObservableCollection<Emp> _listOfEmps;
        public ICommand _AddCommand { get; set; }
        public ICommand _EditCommand { get; set; }
        public ICommand _SaveCommand { get; set; }
        public ICommand _DeleteCommand { get; set; }
        public ICommand _DeactiveCommand { get; set; }
        TAS2013Entities entity;
        public ObservableCollection<JobTitle> listOfJobs
        {

            get
            {
                return _listOfJobs;
            }

            set
            {
                listOfJobs = value;
                OnPropertyChanged("listOfJobs");
            }


        
        }
        public ObservableCollection<string> listOfMarried
        {
            get
            {
                return _listOfMarried;
            }

            set
            {
                listOfMarried = value;
                OnPropertyChanged("listOfMarried");
            }


        }
        public ObservableCollection<string> listOfGenders
        {
            get
            {
                return _listOfGenders;
            }

            set
            {
                listOfGenders = value;
                OnPropertyChanged("listOfGenders");
            }
        
        
        }
        public Category selectedCat
        {
            get
            {
                return _selectedCat;
            }
            set
            {
                this.isEnabled = false;
                _selectedCat = value;
                listOfEmpTypes = new ObservableCollection<EmpType>(entity.EmpTypes.Where(aa => aa.CatID == selectedCat.CatID));
                //add more code
            }
    }
        public Department selectedDept
        {
            get
            {
                return _selectedDept;
            }
            set
            {
                _selectedDept = value;
               listOfSecs= new ObservableCollection<Section>(entity.Sections.Where(aa => aa.DeptID == _selectedDept.DeptID));
                base.OnPropertyChanged("selectedDept");
            }
        }
        public Section selectedSec
        {
            get
            {
                return _selectedSec;
            }
            set
            {
                _selectedSec = value;
                _selectedEmp.Section = _selectedSec;
                 //listOfSecs = new ObservableCollection<Section>(entity.Sections.Where(aa => aa.DeptID == _selectedDept.DeptID));
                base.OnPropertyChanged("selectedEmp");
                base.OnPropertyChanged("selectedSec");
            }
        }
        public Grade selectedGrade
        {
            get
            {
                return _selectedGrade;
            }
            set
            {
                _selectedGrade = value;
                listOfGrades = new ObservableCollection<Grade>(entity.Grades.Where(aa => aa.GradeID == _selectedGrade.GradeID));
                base.OnPropertyChanged("selectedGrade");
            }
        }
        public Emp selectedEmp
        {
            get
            {
                if(_selectedEmp!= null)
                if (_selectedEmp.Section == null)
                {
                    _selectedEmp.Section = entity.Sections.FirstOrDefault();
                    if (_selectedEmp.Section.Department != null)
                    {
                        _listOfSecs = new ObservableCollection<Section>(entity.Sections.Where(aa => aa.DeptID == _selectedEmp.Section.Department.DeptID));
                        _selectedSec = _selectedEmp.Section;
                        base.OnPropertyChanged("selectedSec");
                        base.OnPropertyChanged("listOfSecs");
                    }
                }
               
           
                return _selectedEmp;
            }
            set
            {
                this.isEnabled = false;
                _selectedEmp = value;
                
                if (_selectedEmp != null && _selectedSec != null && _selectedDept != null)
                {
                    _listOfSecs = new ObservableCollection<Section>(entity.Sections.Where(aa => aa.DeptID == _selectedEmp.Section.DeptID).ToList());
                _selectedEmp.SecID = _selectedSec.SectionID;
                _selectedEmp.Section.DeptID = _selectedDept.DeptID;
                }
                _dummyEmp = value;
                base.OnPropertyChanged("dummyEmp");
                base.OnPropertyChanged("selectedEmp");
                base.OnPropertyChanged("isEnabled");

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

        public ObservableCollection<Emp> listOfEmps
        {
            get { 
                return _listOfEmps; }

            set
            {
                 
                listOfEmps = value;
                OnPropertyChanged("listOfEmps");
            }
        }
        public ObservableCollection<Category> listOfCats
        {
            get { return _listOfCats; }

            set
            {
                listOfCats = value;
                OnPropertyChanged("listOfCats");
            }
        }
        public ObservableCollection<EmpType> listOfEmpTypes
        {
            get { return _listOfEmpTypes; }

            set
            {
                _listOfEmpTypes = value;
                OnPropertyChanged("listOfEmpTypes");
            }
        }
        public ObservableCollection<Designation> listOfDesgs
        {
            get { return _listOfDesgs; }

            set
            {
                _listOfDesgs = value;
                OnPropertyChanged("listOfDesgs");
            }
        }
        public ObservableCollection<Grade> listOfGrades
        {
            get { return _listOfGrades; }

            set
            {
                _listOfGrades = value;
                OnPropertyChanged("listOfGrades");
            }
        }
        public ObservableCollection<Shift> listOfShifts
        {
            get { return _listOfShifts; }

            set
            {
                _listOfShifts = value;
                OnPropertyChanged("listOfShifts");
            }
        }
        public ObservableCollection<Department> listOfDepts
        {
            get { return _listOfDepts; }

            set
            {
                _listOfDepts = value;
                OnPropertyChanged("listOfDepts");
            }
        }
        public ObservableCollection<Location> listOfLocs
        {
            get { return _listOfLocs; }

            set
            {
                _listOfLocs = value;
                OnPropertyChanged("listOfLocs");
            }
        }
        public ObservableCollection<Section> listOfSecs
        {
            get { return _listOfSecs; }

            set
            {
                _listOfSecs = value;
                OnPropertyChanged("listOfSecs");
            }
        }
        public ObservableCollection<Crew> listOfCrews
        {
            get { return _listOfCrews; }

            set
            {
                _listOfCrews = value;
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
        public ICommand DeactiveCommand
        {
            get
            {
                return _DeactiveCommand;
            }

        }


        #endregion

        #region constructor
        public VMEmployee()
        {
            IsChecked = false;
            entity = new TAS2013Entities();

            _listOfJobs = new ObservableCollection<JobTitle>(entity.JobTitles.ToList());
            _selectedDept = new Department();

            _listOfEmps = new ObservableCollection<Emp>(entity.Emps.Where(emp => emp.Status == true).ToList().OrderByDescending(x => x.EmpNo));
             _selectedEmp = entity.Emps.ToList().FirstOrDefault();

             User _user = GlobalClasses.Global.user;
             QueryBuilderForSection queryForSection = new QueryBuilderForSection();
             string query = queryForSection.MakeCustomizeQuerySection(_user);

             _listOfEmps = new ObservableCollection<Emp>(entity.Emps.Where(query).AsQueryable());
             _selectedEmp = _listOfEmps.FirstOrDefault();
             Designation des = _selectedEmp.Designation;

             _dummyEmp = selectedEmp;
             _listOfMarried = new ObservableCollection<string>();
             _listOfMarried.Add("Single");
             _listOfMarried.Add("Married");
             _listOfMarried.Add("Engaged");
             _listOfCats = new ObservableCollection<Category>(entity.Categories.ToList().OrderByDescending(x => x.CatName));
             _selectedCat = entity.Categories.ToList().FirstOrDefault();

             _listOfEmpTypes = new ObservableCollection<EmpType>(entity.EmpTypes.ToList().OrderByDescending(x => x.TypeName));
             _listOfDesgs = new ObservableCollection<Designation>(entity.Designations.ToList().OrderByDescending(x => x.DesignationName));
             _listOfGrades = new ObservableCollection<Grade>(entity.Grades.ToList().OrderByDescending(x => x.GradeName));

            _listOfEmpTypes = new ObservableCollection<EmpType>(entity.EmpTypes.ToList());
            _listOfDesgs = new ObservableCollection<Designation>(entity.Designations.ToList().OrderBy(s=>s.DesignationName));
            _listOfGrades = new ObservableCollection<Grade>(entity.Grades.ToList());

            _listOfGenders = new ObservableCollection<string>();
            _listOfGenders.Add( "Male");
            _listOfGenders.Add("Female");
            _selectedGrade = entity.Grades.ToList().FirstOrDefault();
            _listOfShifts = new ObservableCollection<Shift>(entity.Shifts.ToList().OrderByDescending(x => x.ShiftName));
            _listOfDepts = new ObservableCollection<Department>(entity.Departments.ToList());
            _selectedDept = entity.Departments.ToList().FirstOrDefault();
            _listOfLocs = new ObservableCollection<Location>(entity.Locations.ToList());
            // We did not get the section list from the  dep id in AHC this will change for different clients.

            _listOfSecs = new ObservableCollection<Section>(entity.Sections.ToList().OrderByDescending(x => x.SectionName));

            query = queryForSection.MakeCustomizeQuerySec(_user);
            _listOfSecs = new ObservableCollection<Section>(entity.Sections.Where(query));

            _selectedSec = _listOfSecs.FirstOrDefault();
            _listOfCrews = new ObservableCollection<Crew>(entity.Crews.ToList().OrderByDescending(x => x.CrewName));
         
            this._AddCommand = new AddCommandEmp(_selectedEmp);
            this._EditCommand = new EditCommandEmp(this);
            this._DeleteCommand = new DeleteCommandEmp(_selectedEmp);
            this._DeactiveCommand = new DeactiveCommandEmp(_selectedEmp);
            this._isAdding = false;
            this._isEnabled = false;
            this._SaveCommand = new SaveCommandEmp(this);
            
            IsChecked = true;
        }
        #endregion

        internal void raiseEmpChange()
        {
          
            base.OnPropertyChanged("dummyEmp");
        }
    }
}
