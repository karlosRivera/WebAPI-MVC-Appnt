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
    public class DoctorRepository : BaseRepository<Doctor>
    {
        public DoctorRepository(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<Doctor> GetAll()
        {
            using (var command = new SqlCommand("SELECT * FROM Doctor"))
            {
                return GetRecords(command);
            }
        }

        public Doctor GetById(string id)
        {
            // PARAMETERIZED QUERIES!
            using (var command = new SqlCommand("SELECT * FROM Doctor WHERE ID = @ID"))
            {
                command.Parameters.Add(new SqlParameter ("ID", id));
                return GetRecord(command);
            }
        }

        public override Doctor PopulateRecord(SqlDataReader reader)
        {
            return new Doctor
            {
                ID = Convert.ToInt32(reader["ID"].ToString()),
                Name = reader["Name"].ToString()
            };
        }
    }
}
