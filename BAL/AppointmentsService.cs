using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class AppointmentsService
    {
        public IEnumerable<Appointments> GetUserWiseAppointments(string email)
        {
            AppointmentsRepository _appointmentservice = new AppointmentsRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            IEnumerable<Appointments> _appointments = _appointmentservice.GetAppointmentByUser(email);
            return _appointments;
        }
    }
}
