using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication2.NewDataBase;
using OldDatabase.OldModel;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {

        TAS2007Entities oldDataBase = new TAS2007Entities();
        TAS2013Entities newDataBase = new TAS2013Entities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ConvertFromAccessToSQDutyCode();
            // ConvertFromAccessToSQHoliday();
            // ConvertFromAccessToSQDutyTime();
            // ConvertFromAccessToSQDownloadTime();
            // ConvertFromAccessToSQLocation();
            // ConvertFromAccessToSQGrade();
            // ConvertFromAccessToSQJobTitle();
            //ConvertFromAccessToSQEmpType();
            // ConvertFromAccessToSQRoster();
            // ConvertFromAccessToSQDept();
            // ConvertFromAccessToSQLvData();
            // ConvertFromAccessToSQRdrType();
            // ConvertFromAccessToSQLvAppl();
            // ConvertFromMedixiToTrackUser();
            // ConvertFromMedixiToTrackUser();
            // ConvertFromAccessToSQUserRole();
            // ConvertFromAccessToSQShift();
           // ConvertFromAccessToSQEmp();
            ConvertFromAccessToSQPollData();
           // ConvertFromAccessToSQReader();






        }

        private void ConvertFromAccessToSQReader()
        {
            List<OldDatabase.OldModel.Reader> listofoldReaders = new List<OldDatabase.OldModel.Reader>();
            listofoldReaders = oldDataBase.Readers.ToList<OldDatabase.OldModel.Reader>();
            foreach (var old in listofoldReaders)
            {
                WindowsFormsApplication2.NewDataBase.Reader reader = new NewDataBase.Reader();
                reader.RdrID = old.ReaderID;
                reader.RdrName = old.ReaderName;
                reader.RdrDutyID =Convert.ToByte (old.DutyID);
                reader.IpAdd = old.IPAddress;
                reader.IpPort =(short) old.Port;
                reader.RdrTypeID =Convert.ToByte (old.ReaderType);
                 if (old.Status == "A")
                     reader.Status = true;
                else
                     reader.Status = false;





                newDataBase.Readers.Add(reader);

                newDataBase.SaveChanges();



            }
        }
       

        private void ConvertFromAccessToSQPollData()
        {
            List<OldDatabase.OldModel.PollData> listofoldPollDatas = new List<OldDatabase.OldModel.PollData>();
            listofoldPollDatas = oldDataBase.PollDatas.ToList<OldDatabase.OldModel.PollData>();
            foreach (var old in listofoldPollDatas)
            {
                WindowsFormsApplication2.NewDataBase.PollData polldata = new NewDataBase.PollData();
                polldata.PollID = old.TranNo;
                if(old.EmpNo!="<ERR>")
                polldata.EmpID = Convert.ToInt16 (old.EmpNo);
                polldata.EmpDate = old.EmpDate;
                polldata.CardNo = old.CardNo;
                polldata.EntDate =Convert.ToDateTime (old.EntryDate);
               // polldata.EntTime =Convert.ToDateTime (old.EntryTime);
                string enttime = old.EntryTime;
                string hh = enttime.Substring(0, 2);
                string mm = enttime.Substring(2);
                int hour = int.Parse(hh);
                int min = int.Parse(mm);
                polldata.EntTime = polldata.EntDate+new TimeSpan(hour, min, 0);
                polldata.RdrID =(short) old.ReaderID;

                if (old.Status == "N")
                    polldata.Split = true;
                else
                    polldata.Split = false;












                newDataBase.PollDatas.Add(polldata);

                newDataBase.SaveChanges();



            }
        }
        
        

        private void ConvertFromAccessToSQEmp()
        {
            List<OldDatabase.OldModel.Emp> listofoldEmps = new List<OldDatabase.OldModel.Emp>();
            listofoldEmps = oldDataBase.Emps.ToList<OldDatabase.OldModel.Emp>();
            foreach (var old in listofoldEmps)
            {
                WindowsFormsApplication2.NewDataBase.Emp emp = new NewDataBase.Emp();
                emp.EmpID = Convert.ToInt32(old.EmpID);
                emp.EmpNo = old.EmpNo;
                emp.EmpName = old.EmpName;
                emp.JobID = old.JobID;
                emp.Gender = Convert.ToByte(old.Gender);
              //  emp.ShiftID = Convert.ToByte(old.ShiftID);
                emp.LocID = old.LocID;
                emp.TypeID = Convert.ToByte(old.TypeID);
                if (old.GradeID == null)
                    emp.GradeID = 1;
                    if(old.GradeID == 0)
                        emp.GradeID = 1;
                if(old.GradeID==1)
                emp.GradeID = Convert.ToByte(old.GradeID);
                emp.CardNo = old.CardNo;
                emp.FpID = old.FpID;
                emp.PinCode = old.PinCode;
                emp.NicNo = old.NicNo;
                emp.FatherName = old.FatherName;
                emp.BloodGroup = old.BloodGroup;
                emp.BirthDate = old.BirthDate;
                emp.MarStatus = old.MarStatus + "";
                emp.JoinDate = old.JoinDate;
                emp.ValidDate = old.ValidDate;
                emp.ProcessAtt = Convert.ToBoolean(old.ProcessAtt);
                Designation design = new Designation();
                List<Designation> list = newDataBase.Designations.ToList();
                string name = old.Designation.Replace(System.Environment.NewLine, string.Empty);
                foreach (var desgn in list)
                {
                    string newname = desgn.DesignationName.Replace(System.Environment.NewLine, string.Empty);
                    if (name == newname)
                    {
                        design = newDataBase.Designations.Where(aa => aa.DesignationName == desgn.DesignationName).FirstOrDefault();
                        emp.DesigID = design.DesignationID;

                    }

                }

                if (old.Status == "A")
                    emp.Status = true;
                else
                    emp.Status = false;
                emp.HomeAdd = old.PstAddress;
                emp.ProcessIn = Convert.ToBoolean(old.ProcessTimeIn);
                emp.CellNo = old.Reference;
                emp.Remarks = old.Remarks;
                if (old.FlgFPData1 != null)
                {
                    if (old.FlgFPData1 == "Y")
                        emp.FlagFP = true;
                    else
                        emp.FlagFP = false;
                }

                if (old.FlgFPData2 != null)
                {
                    if (old.FlgFPData2 == "Y")
                        emp.FlagFace = true;
                    else
                        emp.FlagFace = false;

                }

                if (old.FlgFPData3 != null)
                {
                    if (old.FlgFPData3 == "Y")
                        emp.FlagCard = true;
                    else
                        emp.FlagCard = false;

                }

                newDataBase.Emps.Add(emp);

                newDataBase.SaveChanges();



            }
        }
       

        private void ConvertFromAccessToSQShift()
        {
            List<OldDatabase.OldModel.Shift> listofoldShifts = new List<OldDatabase.OldModel.Shift>();
            listofoldShifts = oldDataBase.Shifts.ToList<OldDatabase.OldModel.Shift>();
            foreach (var old in listofoldShifts)
          {
              WindowsFormsApplication2.NewDataBase.Shift shift = new NewDataBase.Shift();
              shift.ShiftID =Convert.ToByte (old.ShiftID);
            

              string adnan = old.ShiftTime;
              string hh = adnan.Substring(0, 2);
              string mm = adnan.Substring(2);
              int hour = int.Parse(hh);
              int min = int.Parse(mm);
              shift.StartTime = new TimeSpan(hour, min, 0);
              shift.DayOff1 =Convert.ToByte (old.Do1);
              shift.DayOff2 =Convert.ToByte (old.Do2);
              shift.Holiday =Convert.ToBoolean (old.GH);






              newDataBase.Shifts.Add(shift);

              newDataBase.SaveChanges();



          }
        }
        

       private void ConvertFromAccessToSQUserRole()
        {
            List<OldDatabase.OldModel.UserRole> listofoldUserRoles = new List<OldDatabase.OldModel.UserRole>();
            listofoldUserRoles = oldDataBase.UserRoles.ToList<OldDatabase.OldModel.UserRole>();
             foreach (var old in listofoldUserRoles)
          {
              WindowsFormsApplication2.NewDataBase.UserRole userrole = new NewDataBase.UserRole();
              userrole.RoleID =Convert.ToByte (old.RoleID);
              userrole.RoleName = old.RoleName;



              newDataBase.UserRoles.Add(userrole);

              newDataBase.SaveChanges();



          }
        }
       
        

        private void ConvertFromMedixiToTrackUser()
        {
            
             List<OldDatabase.OldModel.User> listofoldUsers = new List<OldDatabase.OldModel.User>();
             listofoldUsers = oldDataBase.Users.ToList<OldDatabase.OldModel.User>();
             foreach (var old in listofoldUsers)
           {
               WindowsFormsApplication2.NewDataBase.User user = new NewDataBase.User();
              user.Password= old.Password;
              if (old.Status == "A")
                  user.Status = true;
              else
                  user.Status = false;
              user.Name = old.UserName;
              



           }
                 }

        private void ConvertFromAccessToSQLvAppl()
        {
            List<OldDatabase.OldModel.LvAppl> listofoldLvAppls = new List<OldDatabase.OldModel.LvAppl>();
           listofoldLvAppls = oldDataBase.LvAppls.ToList<OldDatabase.OldModel.LvAppl>();
            foreach (var old in listofoldLvAppls)
           {
               WindowsFormsApplication2.NewDataBase.LvApplication lvapplication = new NewDataBase.LvApplication();
               lvapplication.LvID = old.LvID;
               lvapplication.LvDate =Convert.ToDateTime (old.LvDate);
               lvapplication.TypeID = old.LvType;
               lvapplication.EmpID =Convert.ToInt16 (old.EmpNo);
               lvapplication.FromDate =Convert.ToDateTime (old.FromDate);
               lvapplication.ToDate =Convert.ToDateTime (old.ToDate);
               lvapplication.NoOfDays =(float) old.NoDays;
               if (old.HalfLeave == 1)
                   lvapplication.IsHalf = true;
               else
                   lvapplication.IsHalf = false;
               if (old.HalfAbsent == 1)
                   lvapplication.HalfAbsent = true;
               else 
                   lvapplication.HalfAbsent = false;
               lvapplication.LvReason = old.LvReason;
               lvapplication.LvAddress = old.LvAddress;
               lvapplication.CreatedBy = Convert.ToInt32(old.CreateBy);
               lvapplication.ApprovedBy =Convert.ToInt16 (old.ApproveBy);
               lvapplication.LvStatus = old.Status;


               newDataBase.LvApplications.Add(lvapplication);

               newDataBase.SaveChanges();



           }
        }
        

        private void ConvertFromAccessToSQRdrType()
        {
             List<OldDatabase.OldModel.RdrType> listofoldRdrTypes = new List<OldDatabase.OldModel.RdrType>();
           listofoldRdrTypes = oldDataBase.RdrTypes.ToList<OldDatabase.OldModel.RdrType>();
            foreach (var old in listofoldRdrTypes)
           {
               WindowsFormsApplication2.NewDataBase.ReaderType readertype = new NewDataBase.ReaderType();
               readertype.RdrTypeID =Convert.ToByte (old.RTypeID);
               readertype.RdrTypeName = old.RTypeName;


               newDataBase.ReaderTypes.Add(readertype);

               newDataBase.SaveChanges();



           }
        }
       

        //private void ConvertFromAccessToSQLvData()
        //{
        //    List<OldDatabase.OldModel.LvData> listofoldLvDatas = new List<OldDatabase.OldModel.LvData>();
        //    listofoldLvDatas = oldDataBase.LvDatas.ToList<OldDatabase.OldModel.LvData>();
        //   foreach (var old in listofoldLvDatas)
        //    {
        //        WindowsFormsApplication2.NewDataBase.LvData lvdata = new NewDataBase.LvData();
        //        lvdata.EmpDate = old.EmpDate;
        //        lvdata.LvID = old.LvID;
        //        lvdata.EmpID =Convert.ToInt16 (old.EmpNo);
        //        lvdata.LvCode = old.DutyCode;
        //       lvdata.LvStatus =Convert.ToByte (old.LvStatus);
        //        lvdata.Remarks = old.Remarks;
        //        lvdata.AttDate = Convert.ToDateTime (old.DutyDate);
        //        newDataBase.LvDatas.Add(lvdata);

        //        newDataBase.SaveChanges();



        //    }
        //}
       

        private void ConvertFromAccessToSQDept()
        {
             List<OldDatabase.OldModel.Dept> listofoldDepts = new List<OldDatabase.OldModel.Dept>();
            listofoldDepts = oldDataBase.Depts.ToList<OldDatabase.OldModel.Dept>();
            foreach (var old in listofoldDepts)
            {
                WindowsFormsApplication2.NewDataBase.Department department = new NewDataBase.Department();
                department.DeptID = old.DeptID;
                department.DeptName = old.DeptName;
                newDataBase.Departments.Add(department);

                newDataBase.SaveChanges();



            }
        }
       

        private void ConvertFromAccessToSQRoster()
        {
            List<OldDatabase.OldModel.Roster> listofoldRosters = new List<OldDatabase.OldModel.Roster>();
            listofoldRosters = oldDataBase.Rosters.ToList<OldDatabase.OldModel.Roster>();
            foreach (var old in listofoldRosters)
            {
                WindowsFormsApplication2.NewDataBase.Roster roster = new NewDataBase.Roster();
                roster.EmpDate = old.EmpDate;
                roster.EmpID =Convert.ToInt16 (old.EmpNo);
                roster.RosterDate =(DateTime) old.DutyDate;
                roster.DutyCode = old.DutyCode;
               
                string dutytime = old.DutyTime;

                if (dutytime == "R")
                    roster.DutyTime = new TimeSpan(0, 0, 0);
                else
                {
                    string h = dutytime.Substring(0, 2);
                    string m = dutytime.Substring(2);
                    int hour = int.Parse(h);
                    int min = int.Parse(m);
                    roster.DutyTime = new TimeSpan(hour, min, 0);
                }
                
                roster.WorkMin = (short)old.WrkMin;
                roster.Remarks = old.Remarks;
                roster.RosterSubAppID = old.RosterID;

                newDataBase.Rosters.Add(roster);

                newDataBase.SaveChanges();
            }
        }
        private void ConvertFromAccessToSQEmpType()
        {
            List<OldDatabase.OldModel.EmpType> listofoldEmpTypes = new List<OldDatabase.OldModel.EmpType>();
            listofoldEmpTypes = oldDataBase.EmpTypes.ToList<OldDatabase.OldModel.EmpType>();
            foreach (var old in listofoldEmpTypes)
            {
                WindowsFormsApplication2.NewDataBase.EmpType emptype = new NewDataBase.EmpType();
                emptype.TypeID =Convert.ToByte (old.TypeID);
                emptype.TypeName = old.TypeName;
                newDataBase.EmpTypes.Add(emptype);

                newDataBase.SaveChanges();

            }
        }
        private void ConvertFromAccessToSQJobTitle()
        {
            
            
                 List<OldDatabase.OldModel.JobTitle> listofoldJobTitles = new List<OldDatabase.OldModel.JobTitle>();
            listofoldJobTitles = oldDataBase.JobTitles.ToList<OldDatabase.OldModel.JobTitle>();
            foreach (var old in listofoldJobTitles)
            { 
                WindowsFormsApplication2.NewDataBase.JobTitle jobtitle = new NewDataBase.JobTitle();
                jobtitle.JobID = old.JobID;
                jobtitle.JobTitle1 = old.JobName;
                newDataBase.JobTitles.Add(jobtitle);

                newDataBase.SaveChanges();
                

               
            }
        }

               

            

        
        private void ConvertFromAccessToSQGrade()
        {
            List<OldDatabase.OldModel.Grade> listofoldGrades = new List<OldDatabase.OldModel.Grade>();
            listofoldGrades = oldDataBase.Grades.ToList<OldDatabase.OldModel.Grade>();
            foreach (var old in listofoldGrades)
            {
                WindowsFormsApplication2.NewDataBase.Grade grade = new NewDataBase.Grade();
                grade.GradeID =Convert.ToByte (old.GradeID);
                grade.GradeName = old.GradeName;
                newDataBase.Grades.Add(grade);

                newDataBase.SaveChanges();
                

               
            }
        }
      

        private void ConvertFromAccessToSQLocation()
        {
            List<OldDatabase.OldModel.Location> listofoldLocations = new List<OldDatabase.OldModel.Location>();
            listofoldLocations = oldDataBase.Locations.ToList<OldDatabase.OldModel.Location>();
            foreach (var old in listofoldLocations)
            {
                WindowsFormsApplication2.NewDataBase.Location location = new NewDataBase.Location();
                location.LocID = (short)old.LocID;
                location.LocName = old.LocName;
                newDataBase.Locations.Add(location);

                newDataBase.SaveChanges();
            }
        }
        private void ConvertFromAccessToSQDownloadTime()
        {
            List<OldDatabase.OldModel.DownloadTime> listofoldDownloadTimes = new List<OldDatabase.OldModel.DownloadTime>();
            listofoldDownloadTimes = oldDataBase.DownloadTimes.ToList<OldDatabase.OldModel.DownloadTime>();
            foreach (var old in listofoldDownloadTimes)
            {
                WindowsFormsApplication2.NewDataBase.DownloadTime downloadtime = new NewDataBase.DownloadTime();
                string loadtime = old.LoadTime;
                string hh = loadtime.Substring(0, 2);
                string mm = loadtime.Substring(2);
                int hour = int.Parse(hh);
                int min = int.Parse(mm);
                downloadtime.DownTime = new TimeSpan(hour, min, 0);
                newDataBase.DownloadTimes.Add(downloadtime);
                newDataBase.SaveChanges();
            }
        }

        private void ConvertFromAccessToSQDutyTime()
        {
            List<OldDatabase.OldModel.DutyTime> listofoldDutyTimes = new List<OldDatabase.OldModel.DutyTime>();
            listofoldDutyTimes = oldDataBase.DutyTimes.ToList<OldDatabase.OldModel.DutyTime>();
            foreach (var old in listofoldDutyTimes)
            {
                WindowsFormsApplication2.NewDataBase.DutyTime dutytime = new NewDataBase.DutyTime();
                dutytime.TimeSample = old.DutyTime1;
                newDataBase.DutyTimes.Add(dutytime);
                newDataBase.SaveChanges();
            }
                newDataBase.SaveChanges();
            
                
            }
            
        private void ConvertFromAccessToSQHoliday()
        {
            List<OldDatabase.OldModel.Holiday> listofoldHolidays = new List<OldDatabase.OldModel.Holiday>();
            listofoldHolidays = oldDataBase.Holidays.ToList<OldDatabase.OldModel.Holiday>();
            foreach (var old in listofoldHolidays) // Loop over the rows.
            {

                WindowsFormsApplication2.NewDataBase.Holiday holiday = new NewDataBase.Holiday();
              //  holiday.HolDate = Convert.ToDateTime(old.HolDate);
                string sub = old.HolDate.Substring(0, 2);
                string Mint = old.HolDate.Substring(2);
                //01/08/2008 14:50'"
                old.HolDate = ( "2015/" +sub + "/" + Mint + " " +"00:00");
              //  String EmpDate = WindowsFormsApplication2.NewDataBase(row, "Holiday");
                //string EmpNo = EmpDate.Substring(0, 6);
                //string Date = EmpDate.Substring(6);
                holiday.HolDesc = old.Description;
               // dutycode.CodeID = old.DutyID + "";
                newDataBase.Holidays.Add(holiday);
                newDataBase.SaveChanges();
            }
            newDataBase.SaveChanges();
        }

        private void ConvertFromAccessToSQDutyCode()
        {
           
            List<OldDatabase.OldModel.DutyCode> listofolddutycodes = new List<OldDatabase.OldModel.DutyCode>();
            listofolddutycodes = oldDataBase.DutyCodes.ToList<OldDatabase.OldModel.DutyCode>();
            foreach (var old in listofolddutycodes) // Loop over the rows.
            {

                WindowsFormsApplication2.NewDataBase.DutyCode dutycode = new NewDataBase.DutyCode();
                dutycode.DutyName = old.DutyName;
                dutycode.CodeID = old.DutyID + "";
                newDataBase.DutyCodes.Add(dutycode);
                newDataBase.SaveChanges();
            }
            newDataBase.SaveChanges();
        }
    }
}
