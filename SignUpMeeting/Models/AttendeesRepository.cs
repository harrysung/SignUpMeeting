using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SignUpMeeting.Models
{
    public class AttendeesRepository : IAttendeesRepository
    {
        private string xmlFilePath;
        private XDocument xmlDocument;
        public AttendeesRepository()
        {
            try
            {
                xmlFilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\Attendees.xml");
                xmlDocument = XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XElement[] FormatAttendeeData(Attendees attendee)
        {
            XElement[] attendeeInfor = {
                                    new XElement("id", attendee.Id),
                                    new XElement("name", attendee.Name),
                                    new XElement("email", attendee.Email),
                                    new XElement("phone", attendee.Phone),
                                    new XElement("isSpeaker", attendee.IsSpeaker.ToString())
                                   };
            return attendeeInfor;
        }

        public Attendees GetAttendee(string id)
        {
            try
            {
                return (from attendee in xmlDocument.Elements("attendees").Elements("attendee")
                        where attendee.Attribute("id").Value.Equals(id)
                        select new Attendees
                        {
                            Id = attendee.Attribute("id").Value,
                            Name = attendee.Element("name").Value,
                            Email = attendee.Element("email").Value,
                            Phone = attendee.Element("phone").Value,
                            IsSpeaker = Convert.ToBoolean(attendee.Element("isSpeaker").Value),
                        }).Single();
            }
            catch
            {
                return null;
            }
        }


        public void AddAttendee(Attendees attendee)
        {
            try
            {
                var topAttendee = (from attendeeNode in xmlDocument.Elements("attendees").Elements("attendee")
                                   orderby attendeeNode.Attribute("id").Value descending
                                   select attendeeNode).Take(1);
                string newAttendeeId = "at" + (Convert.ToInt32(topAttendee.Attributes("id").First().Value.Substring(2)) + 1).ToString();

                if (this.GetAttendee(newAttendeeId) == null)
                {
                    XElement attendeesRoot = xmlDocument.Elements("attendees").Single();
                    XElement newattendee = new XElement("attendee", new XAttribute("id", newAttendeeId));
                    XElement[] attendeeInfor = FormatAttendeeData(attendee);
                    newattendee.ReplaceNodes(attendeeInfor);
                    attendeesRoot.Add(newattendee);
                    xmlDocument.Save(xmlFilePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Attendees> GetAttendees()
        {
            try
            {
                return (
                    from attendee in xmlDocument.Elements("attendees").Elements("attendee")
                    orderby attendee.Attribute("id").Value ascending
                    select new Attendees
                    {
                        Id = attendee.Attribute("id").Value,
                        Name = attendee.Element("name").Value,
                        Email = attendee.Element("email").Value,
                        Phone = attendee.Element("phone").Value,
                        IsSpeaker = Convert.ToBoolean(attendee.Element("isSpeaker").Value)
                    }
                    ).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}