using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Appointments.Models;
using Entities;
using BAL;
using System.Web.Security;

namespace Appointments.Controllers
{

    public class AppointmentsController : Controller
    {
        public async Task<ActionResult> List()
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToAction("Login", "Account", new { returnUrl = Url.Action("List", "Appointments") });

            var fullAddress = ConfigurationManager.AppSettings["baseAddress"] + "api/Appointments/UserAppointments/" + User.Identity.Name + "/";

            IEnumerable<UserAppointments> app = null;

            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = client.GetAsync(fullAddress).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var customerJsonString = await response.Content.ReadAsStringAsync();
                            app = JsonConvert.DeserializeObject<IEnumerable<Entities.UserAppointments>>(customerJsonString);
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // catch any exception here
            }

            return View(app);
        }

        public  ActionResult AddAppointments()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "Account", new { returnUrl = Url.Action("AddAppointments", "Appointments") });
            }
            else
            {
                DoctorService _ds = new DoctorService();
                IEnumerable<Doctor> _doctor = _ds.GetAllDoctors();
                DoctorViewModel dvm = new DoctorViewModel();
                dvm.DocID = 0;
                dvm.Doctors = new List<SelectListItem>();

                foreach (var item in _doctor)
                {
                    dvm.Doctors.Add(new SelectListItem() { Text = item.Name, Value = item.ID.ToString() });
                }
                return View(dvm);
            }
            return View();
        }

     }
}