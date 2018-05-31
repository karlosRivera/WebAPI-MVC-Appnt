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

    public class PatientRepository : BaseRepository<Patient>
    {
        public PatientRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Patient> GetAll()
        {
            using (var command = new SqlCommand("SELECT * FROM Patient"))
            {
                return GetRecords(command);
            }
        }

        public bool IsValidPatient(string email, string password)
        {
            using (var command = new SqlCommand("SELECT count(*) FROM Patient WHERE email='" + email + "' and password='" + password + "'"))
            {
                return HasData(command);
            }
        }

        public Patient GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM Patient WHERE ID = @ID"))
            {
                command.Parameters.Add(new SqlParameter("ID", id));
                return GetRecord(command);
            }
        }

        public override Patient PopulateRecord(SqlDataReader reader)
        {
            return new Patient
            {
                ID = Convert.ToInt32(reader["ID"].ToString()),
                Name = reader["Name"].ToString(),
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString()
            };
        }
    }
}
