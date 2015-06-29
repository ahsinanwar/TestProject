using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class SaveCommandEmp : ICommand
    {
        #region Fields
        VMEmployee _vmemp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandEmp(VMEmployee vm)
        { _vmemp = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmemp.selectedEmp != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmployee vmd = (VMEmployee)parameter;
            if (vmd.isAdding)
            {
                context.Emps.Add(vmd.selectedEmp);
                context.SaveChanges();
                vmd.listOfEmps.Add(vmd.selectedEmp);

            }
            else
            {
                Emp emp = context.Emps.First(aa => aa.EmpID == vmd.dummyEmp.EmpID);
                emp.EmpName = vmd.dummyEmp.EmpName;


                emp.EmpPhoto = context.EmpPhotoes.FirstOrDefault(aa => aa.EmpID == emp.EmpID);

                if (emp.EmpPhoto == null)
                {
                    EmpPhoto ep = new EmpPhoto()
                    {
                        EmpID = emp.EmpID,
                        EmpPic = vmd.dummyEmp.EmpPhoto.EmpPic
                    };
                    context.EmpPhotoes.Add(ep);
                    emp.EmpImageID = ep.PhotoID;
                }
                else
                {
                    
                }


                

                
                context.SaveChanges();

                
                emp.EmpImageID = emp.EmpPhoto.PhotoID;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }

        
        #endregion
    }
}
