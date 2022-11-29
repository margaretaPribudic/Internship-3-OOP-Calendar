using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUMP_EventCalendar
{
    public class Person
    {
        public string FirstName;
        public string LastName { get; }

        public string Email { get; }

        public Dictionary<Guid, bool> Attendance { get; private set; }

        public Person(string lastName, string email)
        {
            LastName = lastName;
            Email = email;
        }

        public void ChangeToNotAttended(Guid someEventId)
        {
            if (Attendance.ContainsKey(someEventId))
            {
                Attendance[someEventId] = false;
            }
            else
            {
                Console.WriteLine("Nije ni trebao biti prisutan");
            }
        }

        public void NoteDownAttendance(Event someEvent)
        {
            if (someEvent.Emails.Contains(Email))
            {
                if (Attendance.ContainsKey(someEvent.Id))
                    Attendance[someEvent.Id] = true;
                else
                    Attendance.Add(someEvent.Id, true);
            }
            else
            {
                if (Attendance.ContainsKey(someEvent.Id))
                    Attendance[someEvent.Id] = false;
            }
        }
    }
}
