using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUMP_EventCalendar
{
    public class Event
    {
        public Guid Id { get; }
        public string Name;
        public string Location;
        public DateTime EventStartDate;
        public DateTime EventEndDate;
        public string Emails { get; private set; }

        public Event()
        {
            Id=Guid.NewGuid();
        }


        public bool ActiveEvent()
        {
            var dateNow=DateTime.Now;
            if (EventStartDate <= dateNow & EventEndDate >= dateNow)
                return true;
            return false;
        }

        public bool UpcomingEvent()
        {
            var dateNow = DateTime.Now;
            if (EventStartDate > dateNow)
                return true;
            return false;
        }
        public void AddEmailToEvent(string emails) 
        {
            Emails = emails;
        }

        public string GetParticipants()
        {
            string[] listOfEmails = Emails.Split(' ');
            
            var listOfEmailsLength=listOfEmails.Length;
            string printLista = listOfEmails[0];
            for (int i = 1; i < listOfEmailsLength; i++)
            {
                printLista+=", " + listOfEmails[i];
            }
            return printLista;
        }

        public void DeleteEmail(string[] mails)
        {

            string[] listOfMails = Emails.Split(' ');
            Emails = "";
            foreach (var item in mails)
            {
                for (int i = 0; i < listOfMails.Length; i++)
                {
                    if (listOfMails[i]!=item & !Emails.Contains(item))
                    {
                        Emails= Emails + " " + item;
                    }
                }
            }
        }

        public bool CheckIfEventIsFinished()
        {
            if (EventEndDate < DateTime.Now)
                return true;
            return false;
        }
    }
}
