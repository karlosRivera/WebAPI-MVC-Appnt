using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BAL;
using Entities;
using System.Net.Http;

namespace Appointments.Controllers
{

    [System.Web.Http.RoutePrefix("api/Appointments")]
    public class AppointmentsServiceController : ApiController
    {

        [System.Web.Http.HttpPost, System.Web.Http.Route("login")]
        public IHttpActionResult login(Patient patient)
        {
            PatientService _studentservice = new PatientService();
            bool found = _studentservice.IsValidLogin(patient.Email, patient.Password);
            if (found)
            {
                return Ok("SUCCESS");
            }
            else
            {
                return NotFound();
            }
        }


        [System.Web.Http.HttpGet, System.Web.Http.Route("UserAppointments/{email}")]
        public System.Net.Http.HttpResponseMessage UserAppointments(string email = null)
        {
            System.Net.Http.HttpResponseMessage retObject = null;

            if (!string.IsNullOrEmpty(email))
            {
                UserAppointmentService _appservice = new UserAppointmentService();
                IEnumerable<Entities.UserAppointments> app = _appservice.GetAppointmentsByEmail(email);

                if (app.Count() <= 0)
                {
                    var message = string.Format("No appointment found for the user [{0}]", email);
                    HttpError err = new HttpError(message);
                    retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                    retObject.ReasonPhrase = message;
                }
                else
                {
                    retObject = Request.CreateResponse(System.Net.HttpStatusCode.OK, app);
                }
            }
            else
            {
                var message = string.Format("No email provided");
                HttpError err = new HttpError(message);
                retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                retObject.ReasonPhrase = message;

            }
            return retObject;
        }

        [System.Web.Http.HttpGet, System.Web.Http.Route("DocAppointmentDetails/{id}")]
        public System.Net.Http.HttpResponseMessage DocAppointmentDetails(int id = 0)
        {
            System.Net.Http.HttpResponseMessage retObject = null;

            if (id>0)
            {
                UserAppointmentService _appservice = new UserAppointmentService();
                IEnumerable<Entities.UserAppointments> app = _appservice.GetAppointmentsByDocID(id);

                if (app.Count() <= 0)
                {
                    var message = string.Format("No appointment found for doctor [{0}]", id);
                    HttpError err = new HttpError(message);
                    retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                    retObject.ReasonPhrase = message;
                }
                else
                {
                    retObject = Request.CreateResponse(System.Net.HttpStatusCode.OK, app);
                }
            }
            else
            {
                var message = string.Format("doc id can not be zero");
                HttpError err = new HttpError(message);
                retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                retObject.ReasonPhrase = message;

            }
            return retObject;
        }


        [System.Web.Http.HttpPost,WebAPICORSMULTIPOSTPARAMS.MultiPostParamsBinding.MultiPostParams, 
        System.Web.Http.Route("BookAppointment")]
        public System.Net.Http.HttpResponseMessage BookAppointment(string email, int rowid = 0)
        {
            System.Net.Http.HttpResponseMessage retObject = null;

            if (rowid > 0 && email != "")
            {
                UserAppointmentService _appservice = new UserAppointmentService();
                bool success = _appservice.BookAppointment(Appointments.Utility.Utilities.RemoveJunkChar(email), rowid);

                if (!success)
                {
                    var message = string.Format("error occur for updating data", rowid);
                    HttpError err = new HttpError(message);
                    retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                    retObject.ReasonPhrase = message;
                }
                else
                {
                    retObject = Request.CreateResponse(System.Net.HttpStatusCode.OK, "SUCCESS");
                }
            }
            else
            {
                var message = string.Format("doc id and emial can not be zero or blank");
                HttpError err = new HttpError(message);
                retObject = Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, err);
                retObject.ReasonPhrase = message;

            }
            return retObject;
        }
    }
}
