using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SignUpMeeting.Models;

namespace SignUpMeeting.Controllers
{
    public class AttendeesController : ApiController
    {
        private AttendeesRepository respository;

        public AttendeesController()
        {
            respository = new AttendeesRepository();
        }

        [HttpGet]
        public IEnumerable<Attendees> GetSpeakers()
        {
            return respository.GetAttendees().Where(x => x.IsSpeaker == true);
        }

        [HttpPost]
        public void PostResponse(Attendees attendee)
        {
            if (this.ModelState.IsValid)
            {
                respository.AddAttendee(attendee);
            }
        }
    }
}
