using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AppointmentsRepository : BaseRepository<Appointments>
    {
        public AppointmentsRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Appointments> GetAll()
        {
            using (var command = new SqlCommand("SELECT * FROM Appointments"))
            {
                return GetRecords(command);
            }
        }

        public IEnumerable<Appointments> GetAppointmentByUser(string email)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM vwAppointments WHERE useremail = @email"))
            {
                command.Parameters.Add(new SqlParameter("email", email));
                return GetRecords(command);
            }
        }

        public override Appointments PopulateRecord(SqlDataReader reader)
        {
            return new Appointments
            {

                DocID = Convert.ToInt32(reader["DocID"].ToString()),
                AvailableDate = reader["AvailableDate"].ToString(),
                AvailableTime = reader["AvailableTime"].ToString()
            };
        }
    }
}
