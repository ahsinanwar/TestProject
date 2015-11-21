using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCategory.Commands
{
    class DeleteCommandCat : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Category _vm = new Category();
        #endregion
        public DeleteCommandCat(Category vm)
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
            int emptypecount = context.EmpTypes.Where(aa => aa.CatID == selectedCat.CatID).Count();
            if (emptypecount > 0)
            {
                PopUp.popUp("Category", "Delete Employee Types linked with this Category first", Mantin.Controls.Wpf.Notification.NotificationType.Information);
            
            }
            else
            {
                context.Categories.Remove(selectedCat);
                //vmd.isAdding = true;
                //vmd.isEnabled = true;
                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        vmd.listOfCats.Remove(vmd.selectedCat);
                        vmd.selectedCat = vmd.listOfCats[0];
                        PopUp.popUp("Category", "Category Deleted", Mantin.Controls.Wpf.Notification.NotificationType.Information);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception While Deleting...");
                }
            }
            
        }
    }
}
