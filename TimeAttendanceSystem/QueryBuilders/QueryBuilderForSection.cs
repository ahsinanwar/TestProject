using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.QueryBuilders
{
    class QueryBuilderForSection
    {

        public DataTable GetValuesfromDB(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TimeAttendanceSystem.Properties.Settings.TASDesktopConnectionString"].ConnectionString);

            using (SqlCommand cmdd = Conn.CreateCommand())
            using (SqlDataAdapter sda = new SqlDataAdapter(cmdd))
            {
                cmdd.CommandText = query;
                cmdd.CommandType = CommandType.Text;
                Conn.Open();
                sda.Fill(dt);
                Conn.Close();
            }
            return dt;

        }

        public string MakeCustomizeQuerySection(User _user)
        {
              TAS2013Entities db=  new TAS2013Entities();
            string query = "(";
            List<UserAccess> listOfUserAccess = new List<UserAccess>();
            listOfUserAccess = db.UserAccesses.Where(aa => aa.UserID == _user.UserID).ToList();
            foreach (UserAccess uAccess in listOfUserAccess)
            {

                query = query + "SecID=" + uAccess.CriteriaData + " or ";
            
            }
            if (query.Length > 2)
            {
                query = query.Substring(0, query.Length - 4);
                query = query + ") and Status=true";
            }
            else
                query = query + " Status=true)";
            return query;
        }

        internal List<Section> GetSectionsFromUserAccess(User user)
        {

            List<UserAccess> listOfUserAccess = new List<UserAccess>();
            List<Section> listOfSections = new List<Section>();
            TAS2013Entities ctx = new TAS2013Entities();
            listOfUserAccess = ctx.UserAccesses.Where(aa => aa.UserID == user.UserID).ToList();
            if(listOfUserAccess.Count()>0)
                foreach (UserAccess uAccess in listOfUserAccess)
                {

                    Section section = new Section();
                    section = ctx.Sections.Where(aa => aa.SectionID == uAccess.CriteriaData).FirstOrDefault();
                    listOfSections.Add(section);
                
                }
            return listOfSections;
        
        }

        internal string MakeCustomizeQuerySec(User _user)
        {
            TAS2013Entities db = new TAS2013Entities();
            string query = "(";
            List<UserAccess> listOfUserAccess = new List<UserAccess>();
            listOfUserAccess = db.UserAccesses.Where(aa => aa.UserID == _user.UserID).ToList();
            foreach (UserAccess uAccess in listOfUserAccess)
            {

                query = query + "SectionID=" + uAccess.CriteriaData + " or ";

            }
            if (query.Length > 2)
            {
                query = query.Substring(0, query.Length - 4);
                query = query + ")";
            }
            else
                query = query + ")";
            return query;
        }
    }
}
