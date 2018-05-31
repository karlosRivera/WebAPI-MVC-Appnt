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
    public class UserAppointmentRepository : BaseRepository<UserAppointments>
    {
        public UserAppointmentRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<UserAppointments> GetByEmail(string email)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM vwAppointments WHERE UserEmail = @email"))
            {
                command.Parameters.Add(new SqlParameter("email", email));
                return GetRecords(command);
            }
        }

        public IEnumerable<UserAppointments> GetByDocID(int id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM vwAppointments WHERE ID = @DocID"))
            {
                command.Parameters.Add(new SqlParameter("DocID", id));
                return GetRecords(command);
            }
        }

        public bool BookAppointment(string email, int rowid)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("UPDATE Appointments SET UserEmail=@Email WHERE RowID = @RowID"))
            {
                command.Parameters.Add(new SqlParameter("RowID", rowid));
                command.Parameters.Add(new SqlParameter("Email", email));
                return UpdateData(command);
            }
        }

         public override UserAppointments PopulateRecord(SqlDataReader reader)
        {
            return new UserAppointments
            {
                RowID = Convert.ToInt32(reader["RowID"].ToString()),
                ID = Convert.ToInt32(reader["ID"].ToString()),
                DoctorName = reader["Name"].ToString(),
                Email = reader["UserEmail"].ToString(),
                AvailableDate = reader["AvailableDate"].ToString(),
                AvailableTime = reader["AvailableTime"].ToString()
            };
        }
    }
}
