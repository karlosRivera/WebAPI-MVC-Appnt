using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;


namespace BAL
{
    public class UserAppointmentService
    {
        public IEnumerable<UserAppointments> GetAppointmentsByEmail(string email)
        {
            UserAppointmentRepository _appservice = new UserAppointmentRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            IEnumerable<UserAppointments> _appointments = _appservice.GetByEmail(email);
            return _appointments;
        }

        public IEnumerable<UserAppointments> GetAppointmentsByDocID(int docid)
        {
            UserAppointmentRepository _appservice = new UserAppointmentRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            IEnumerable<UserAppointments> _appointments = _appservice.GetByDocID(docid);
            return _appointments;
        }

        public bool BookAppointment(string email,int rowid)
        {
            UserAppointmentRepository _appservice = new UserAppointmentRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            bool status = _appservice.BookAppointment(email, rowid);
            return status;
        }


    }
}
