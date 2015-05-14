using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpMeeting.Models
{
    interface IAttendeesRepository
    {
        void AddAttendee(Attendees attendee);
        IEnumerable<Attendees> GetAttendees();
    }
}
