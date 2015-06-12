using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCategory.Commands
{
    class SaveCommandCat : ICommand
    {
        #region Fields
        VMCategory _vmcategory;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandCat(VMCategory vm)
        { _vmcategory = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcategory.selectedCat != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCategory vmd = (VMCategory)parameter;
            if (vmd.isAdding)
            {
                context.Categories.Add(vmd.selectedCat);
                context.SaveChanges();
                vmd.listOfCats.Add(vmd.selectedCat);

            }
            else
            {
                Category cat = context.Categories.First(aa => aa.CatID == vmd.selectedCat.CatID);
                cat.CatName = vmd.selectedCat.CatName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
