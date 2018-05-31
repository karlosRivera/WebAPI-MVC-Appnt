using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BAL
{
    public class DoctorService
    {
        public IEnumerable<Doctor> GetAllDoctors()
        {
            DoctorRepository _doctorservice = new DoctorRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            IEnumerable<Doctor> _doctor = _doctorservice.GetAll();
            return _doctor;
        }
    }
}
