using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

namespace Appointments.Models
{
    public class DoctorViewModel
    {
        public int DocID { get; set; }
        public List<SelectListItem> Doctors { get; set; }
    }
}