using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCategory.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Category _vm = new Category();
        #endregion
        public DeleteCommand(Category vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCategory vmd = (VMCategory)parameter;
            Category selectedCat = context.Categories.FirstOrDefault(aa => aa.CatID == vmd.selectedCat.CatID);
            context.Categories.Remove(selectedCat);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfCats.Remove(vmd.selectedCat);
                    vmd.selectedCat = vmd.listOfCats[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
