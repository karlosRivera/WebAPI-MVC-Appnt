using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BAL
{
    public class PatientService
    {
        public IEnumerable<Patient> GetAllPatients()
        {
            PatientRepository _patientservice = new PatientRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            IEnumerable<Patient> _patient = _patientservice.GetAll();
            return _patient;
        }

        public bool IsValidLogin(string email,string password)
        {
            PatientRepository _patientservice = new PatientRepository(System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            return _patientservice.IsValidPatient(email,password);
        }
    }
}
