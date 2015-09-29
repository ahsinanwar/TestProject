using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDLesson
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
            RefreshList();
        }

        /// <summary>
        ///     Method to show how mutiple entries in database can be updated using entity framework
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Update_Click(object sender, EventArgs e)
        {
            CRUDLessonEntities context = new CRUDLessonEntities();
            List<Person> list_person = new List<Person>();
            list_person = context.People.ToList();

            foreach (Person person in list_person) {

                person.Height = 8.9;
                person.Age = 23;
            }
            context.SaveChanges();
            RefreshList();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            using (CRUDLessonEntities context = new CRUDLessonEntities())
            {
                Person person = new Person();
                person.Age = 27;
                person.Name = "Hardcoded Person";
                person.Height = 5.8;
                person.City = "Hardcoded City";

                context.People.Add(person);
                context.SaveChanges();
            }
            RefreshList();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            using (CRUDLessonEntities context = new CRUDLessonEntities())
            {
                Person personToBeRemoved = new Person();
                personToBeRemoved = context.People.First(aa => aa.Name == "Hardcoded Person");
                if(personToBeRemoved != null)
                    context.People.Remove(personToBeRemoved);
                context.SaveChanges();
            }
            RefreshList();
        }

        private void btn_DeleteMultiple_Click(object sender, EventArgs e)
        {
            using (CRUDLessonEntities context = new CRUDLessonEntities())
            {
                List<Person> list_peopleToBeRemoved = new List<Person>();
                list_peopleToBeRemoved = context.People.Where(aa => aa.Name == "Hardcoded Person").ToList();
                foreach (Person person in list_peopleToBeRemoved)
                {
                    context.People.Remove(person);
                }
                context.SaveChanges();
            }
            RefreshList();
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            lst_persons.Items.Clear();
            using (CRUDLessonEntities context = new CRUDLessonEntities())
            {
                List<Person> listPeople = new List<Person>();
                listPeople = context.People.ToList();
                foreach (Person person in listPeople)
                {
                    ListViewItem lvi = new ListViewItem(person.Name);
                    lvi.SubItems.Add(person.Age.ToString());
                    lvi.SubItems.Add(person.City);
                    lvi.SubItems.Add(person.Height.ToString());
                    lst_persons.Items.Add(lvi);
                }
            }
        }

        private void btn_updateSingle_Click(object sender, EventArgs e)
        {
            using (CRUDLessonEntities context = new CRUDLessonEntities())
            {
                string name = lst_persons.SelectedItems[0].Text;
                Person p = context.People.First(person => person.Name == name);
                p.Age = 90;
                context.SaveChanges();
                RefreshList();
            }
        }
    }
}
