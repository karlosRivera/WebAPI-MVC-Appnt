using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserAppointments
    {
        public int RowID { get; set; }
        public int ID { get; set; }
        public string DoctorName { get; set; }
        public string AvailableDate { get; set; }
        public string AvailableTime { get; set; }
        public string Email { get; set; }
    }
}
