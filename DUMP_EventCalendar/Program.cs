using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUMP_EventCalendar
{
    public class Program
    {
        


        static void Main(string[] args)
        {
            var event1 = new Event()
            {
                Name = "DUMP Days 2022",
                Location = "Split",
                EventStartDate = new DateTime(2022, 5, 14,10,0,0),
                EventEndDate = new DateTime(2022, 5, 15,20,0,0)
            };
            var event2 = new Event()
            {
                Name = "Advent u Splitu",
                Location = "Split",
                EventStartDate = new DateTime(2022, 11, 22,8,0,0),
                EventEndDate = new DateTime(2023, 1, 6,23,0,0)
            };
            var event3 = new Event()
            {
                Name = "Noć knjige",
                Location = "Zagreb",
                EventStartDate = new DateTime(2022, 4, 22,16,0,0),
                EventEndDate = new DateTime(2023, 4, 25,23,0,0)
            };
            var event4 = new Event()
            {
                Name = "Event u 2023",
                Location = "Split",
                EventStartDate = new DateTime(2023, 3, 22,8,0,0),
                EventEndDate = new DateTime(2023, 3, 26,20,0,0)
            };
            var event5 = new Event()
            {
                Name = "Buduci event",
                Location = "Zagreb",
                EventStartDate = new DateTime(2023, 3, 22),
                EventEndDate = new DateTime(2023, 3, 26)
            };

            var listOfEvents = new List<Event>()
            {
                event1,event2,event3,event4,event5
            };

            var person1 = new Person("Matic","matematic") { FirstName="Mate"};
            var person2 = new Person("Ivic","ivoivic") { FirstName = "Ivo" };
            var person3 = new Person("Antic","anteantic") { FirstName = "Ante" };
            var person4 = new Person("Horvat","hrvojehorvat") { FirstName = "Hrvoje" };
            var person5 = new Person("Peric", "peroperic") { FirstName = "Pero" };
            var person6 = new Person("Matic", "mateamatic") { FirstName = "Matea" };
            var person7 = new Person("Ivic", "ivaivic") { FirstName = "Iva" };
            var person8 = new Person("Antic", "anteaantic") { FirstName = "Antea" };
            var person9 = new Person("Horvat", "helenahorvat") { FirstName = "Helena" };
            var person10 = new Person("Peric", "petrapetric") { FirstName = "Petra" };

            var listOfPeople=new List<Person>() { person1,person2,person3,person4,person5,person6,person7,person8,person9,person10 };

            string stringOfEmails = $"{person1.Email} {person2.Email} {person3.Email} {person4.Email} {person5.Email} " +
                $"{person6.Email} {person7.Email} {person8.Email} {person9.Email} {person10.Email}";

            foreach (var ev in listOfEvents)
            {
                ev.AddEmailToEvent(stringOfEmails);
            }

            Methods m=new Methods();

            m.MainMenu(listOfEvents,listOfPeople);

            



            //TREĆA TOČKA
            void EventiKojiSuZavrsiliMenu()
            {

            }


            //ČETVRTA TOČKA
            void KreirajEventMenu()
            {

            }
        }


    }

    public class Methods
    {
        public void MainMenu(List<Event> listOfEvents,List<Person> listOfPeople)
        {
            var actionChoice = "";
            do
            {
                Console.WriteLine("1 - Aktivni eventi\n2 - Nadolazeći eventi\n3 - Eventi koji su završili\n4 - Kreiraj event\n0 - Izađi iz programa");
                actionChoice = Console.ReadLine();
            } while (actionChoice != "0" & actionChoice != "1" & actionChoice != "2" & actionChoice != "3" & actionChoice != "4");

            switch (actionChoice)
            {
                case "1":
                    AktivniEventiMenu(listOfEvents,listOfPeople);
                    break;

                case "2":
                    NadolazeciEventiMenu(listOfEvents,listOfPeople);
                    break;

                case "3":
                    EventiKojiSuZavršili(listOfEvents,listOfPeople);
                    MainMenu(listOfEvents,listOfPeople);
                    break;

                case "4":
                    KreirajEvent(listOfEvents,listOfPeople);
                    MainMenu(listOfEvents,listOfPeople);
                    break;

                case "0":
                    Console.Clear();
                    Console.WriteLine("Izašli ste iz aplikacije");
                    break;

                default:
                    Console.WriteLine("Morate unijeti 0, 1, 2, 3 ili 4");
                    break;
            }
        }



        //PRVA TOCKA
        public void AktivniEventiMenu(List<Event> someListOfEvents, List<Person> someListOfPeople)
        {
            Console.WriteLine("Aktivni eventi:");
            foreach (var item in someListOfEvents)
            {
                if (item.ActiveEvent())
                {
                    var endsInHours =Math.Round( (item.EventEndDate - DateTime.Now).TotalHours,1);
                    Console.WriteLine($"Id: {item.Id}\n" +
                        $"{item.Name} - {item.Location} - Zavrsava za: {endsInHours}sati\n" +
                        $"Popis sudionika: {item.GetParticipants()}");
                }
            }
            var listOfActiveEventIds = new List<string>();
            foreach (var item in someListOfEvents)
            {
                if (item.ActiveEvent())
                {
                    listOfActiveEventIds.Add(item.Id.ToString());
                }
            }

            var inputSubmenu = "";
            do
            {
                Console.WriteLine("1 - Zabilježi neprisutnost\n" +
                    "0 - Povratak na glavni menu");
                inputSubmenu = Console.ReadLine();
            } while (inputSubmenu != "0" & inputSubmenu != "1");


            switch (inputSubmenu)
            {
                case "1":
                    ZabiljeziNeprisutnostMenu(listOfActiveEventIds ,someListOfEvents, someListOfPeople);
                    AktivniEventiMenu(someListOfEvents,someListOfPeople);
                    break;

                case "0":
                    Console.Clear();
                    MainMenu(someListOfEvents,someListOfPeople);
                    break;
                default:
                    Console.WriteLine("Morate unijeti 1 ili 0");
                    break;
            }
        }

        Guid someEventId;
        public void ZabiljeziNeprisutnostMenu(List<string> someList,List<Event> listOfActiveEvents,List<Person> someListPeople)
        {
            var inputedString = "";
            do
            {
                Console.WriteLine("Unesite Id eventa");
                inputedString = Console.ReadLine();
            } while (!someList.Contains(inputedString));

            foreach (var item in listOfActiveEvents)
            {
                if (item.Id.ToString() == inputedString& item.ActiveEvent())
                {
                    someEventId = item.Id;
                    break;
                }
            }
            ZabiljeziNeprisutnost(someEventId,someListPeople);
        }

        public void ZabiljeziNeprisutnost(Guid chosenEventId,List<Person> someListOdPeople)
        {
            Console.WriteLine("Unesite emailove osoba koje nisu bile prisutne: ");
            var inputedEmails = Console.ReadLine();
            string[] peopleNotAttending = inputedEmails.Split(' ');

            foreach (var person in someListOdPeople)
            {
                if (peopleNotAttending.Contains(person.Email))
                {
                    person.ChangeToNotAttended(chosenEventId);
                }
            }
        }



        //DRUGA TOCKA
        public void NadolazeciEventiMenu(List<Event> listOfEvents,List<Person> listOfPeople)
        {
            Console.WriteLine("Aktivni eventi:");
            foreach (var item in listOfEvents)
            {
                if (item.UpcomingEvent())
                {
                    var startInDays = (item.EventStartDate - DateTime.Now).TotalDays;
                    var durationInHours = Math.Round((item.EventEndDate-item.EventStartDate).TotalHours,1);
                    Console.WriteLine($"Id: {item.Id}\n" +
                        $"{item.Name} - {item.Location} - Počinje za {startInDays} dana- Trajanje: {durationInHours}sati\n" +
                        $"Popis sudionika: {item.GetParticipants()}");
                }
            }

            var listOfUpcomingEventIds = new List<string>();
            foreach (var item in listOfEvents)
            {
                if (item.ActiveEvent())
                {
                    listOfUpcomingEventIds.Add(item.Id.ToString());
                }
            }

            var inputSubmenu = "";
            do
            {
                Console.WriteLine("1 - Izbriši event\n" +
                    "2 - Ukloni osobe s eventa" +
                    "0 - Povratak na glavni menu");
                inputSubmenu = Console.ReadLine();
            } while (inputSubmenu != "0" & inputSubmenu != "1" & inputSubmenu!="2");


            switch (inputSubmenu)
            {
                case "1":
                    DeleteEvent(listOfEvents, listOfPeople,listOfUpcomingEventIds);
                    break;

                case "2":
                    DeletePeople(listOfEvents, listOfPeople,listOfUpcomingEventIds);
                    break;

                case "0":
                    Console.Clear();
                    MainMenu(listOfEvents, listOfPeople);
                    break;
                default:
                    Console.WriteLine("Morate unijeti 1 ili 0");
                    break;
            }
        }

        public void DeleteEvent(List<Event> listOfevents,List<Person> listOfPeople,List<string> upcomingEventsIdlist)
        {
            var inputedString = "";
            do
            {
                Console.WriteLine("Unesite Id eventa");
                inputedString = Console.ReadLine();
            } while (!upcomingEventsIdlist.Contains(inputedString));

            foreach (var item in listOfevents)
            {
                if (item.Id.ToString() == inputedString & item.UpcomingEvent())
                {
                    listOfevents.Remove(item);
                    break;
                }
            }

            NadolazeciEventiMenu(listOfevents, listOfPeople);
        }

        public void DeletePeople( List<Event> listOfEvents, List<Person> listOfPeople,List<string>upcomingEventsIdlist)
        {
            var inputedString = "";
            do
            {
                Console.WriteLine("Unesite Id eventa");
                inputedString = Console.ReadLine();
            } while (!upcomingEventsIdlist.Contains(inputedString));

            foreach (var item in listOfEvents)
            {
                if (item.Id.ToString() == inputedString & item.UpcomingEvent())
                {
                    Console.WriteLine("Unesite email osobe koju zelite ukloniti s eventa: ");
                    string[] chosenPerson = Console.ReadLine().Split(' ');
                    item.DeleteEmail(chosenPerson);
                }
            }
            NadolazeciEventiMenu(listOfEvents, listOfPeople);
        }



        //TRECA TOCKA
        public void EventiKojiSuZavršili(List<Event> listofEvents,List<Person>listOfPeople)
        {
            foreach (var item in listofEvents)
            {
                if (item.CheckIfEventIsFinished())
                {
                    var notPart = "";
                    foreach (var p in listOfPeople)
                    {
                        if (!item.Emails.Contains(p.Email))
                        {
                            notPart = notPart + p.Email + ", ";
                        }
                    }
                    var daysSinceEnded = (DateTime.Now-item.EventEndDate).TotalDays;
                    var durationInHours = Math.Round((item.EventEndDate - item.EventStartDate).TotalHours, 1);
                    Console.WriteLine($"Id: {item.Id}\n" +
                        $"{item.Name} - {item.Location} - Zavrsilo prije {daysSinceEnded} dana- Trajanje: {durationInHours} sati\n" +
                        $"Popis sudionika: {item.GetParticipants()}" +
                        $"Nisu prisutni: {notPart}");


                }
            }
            Console.WriteLine("Pritisnite bilo koju tipkuza povratak na glavni menu");
            Console.ReadKey();
        }


        //CETVRTA TOCKA
        public void KreirajEvent(List<Event>listOfEvent,List<Person>listOfPeople)
        {
            Console.WriteLine("Unesite Naziv eventa: ");
            var nameOfEvent = Console.ReadLine();

            Console.WriteLine("Uneesite lokaciju eventa: ");
            var locationOfEvent=Console.ReadLine();

            DateTime startDate=new DateTime();
            DateTime endDate=new DateTime();
            do
            {
                Console.WriteLine("Unesite datum pocetka ");
                startDate=DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Unesite datum kraja ");
                endDate=DateTime.Parse(Console.ReadLine());

            } while (startDate<DateTime.Now || endDate<=DateTime.Now || startDate.GetType()!=typeof(DateTime) || endDate.GetType()!=typeof(DateTime));

            Console.WriteLine("Unesite emailove sudionika eventa ");
            var mails=Console.ReadLine();

            var noviEvent = new Event()
            {
                Name = nameOfEvent,
                Location = locationOfEvent,
                EventStartDate = startDate,
                EventEndDate = endDate
            };
            noviEvent.AddEmailToEvent(mails);

            listOfEvent.Add(noviEvent);
            foreach (var item in listOfPeople)
            {
                if (mails.Contains(item.Email))
                {
                    item.NoteDownAttendance(noviEvent);
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za povratak na glavni menu");
            Console.ReadKey();
        }

    }
    

}
