using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Harmonogram
{
    class Program
    {
        // ścieżka do programu 
        private static string dbPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // Tworzenie listy uzytkownikow 
        public static List<Worker> CreateWorkers()
        {
            List<DateTime> offDays = new List<DateTime>();
            offDays.Add(new DateTime(2018, 1, 1));
            offDays.Add(new DateTime(2018, 1, 2));
            offDays.Add(new DateTime(2018, 1, 3));



            List<DateTime> offDays2 = new List<DateTime>();
            offDays2.Add(new DateTime(2018, 1, 4));
            offDays2.Add(new DateTime(2018, 1, 5));
            offDays2.Add(new DateTime(2018, 1, 6));
            offDays2.Add(new DateTime(2018, 1, 7));
            offDays2.Add(new DateTime(2018, 1, 8));
            offDays2.Add(new DateTime(2018, 1, 9));
            offDays2.Add(new DateTime(2018, 1, 10));
            offDays2.Add(new DateTime(2018, 1, 11));

            List<DateTime> offDays3 = new List<DateTime>();
            offDays3.Add(new DateTime(2018, 1, 4));
            offDays3.Add(new DateTime(2018, 1, 5));
            offDays3.Add(new DateTime(2018, 1, 6));
            offDays3.Add(new DateTime(2018, 1, 7));
            offDays3.Add(new DateTime(2018, 1, 8));
            offDays3.Add(new DateTime(2018, 1, 9));
            offDays3.Add(new DateTime(2018, 1, 10));
            offDays3.Add(new DateTime(2018, 1, 11));

            List<DateTime> offDays4 = new List<DateTime>();
            offDays4.Add(new DateTime(2018, 1, 4));
            offDays4.Add(new DateTime(2018, 1, 5));
            offDays4.Add(new DateTime(2018, 1, 6));
            offDays4.Add(new DateTime(2018, 1, 7));
            offDays4.Add(new DateTime(2018, 1, 8));
            offDays4.Add(new DateTime(2018, 1, 9));
            offDays4.Add(new DateTime(2018, 1, 10));
            offDays4.Add(new DateTime(2018, 1, 11));

            List<DateTime> offDays5 = new List<DateTime>();
            offDays5.Add(new DateTime(2018, 1, 4));
            offDays5.Add(new DateTime(2018, 1, 5));
            offDays5.Add(new DateTime(2018, 1, 6));
            offDays5.Add(new DateTime(2018, 1, 7));
            offDays5.Add(new DateTime(2018, 1, 8));
            offDays5.Add(new DateTime(2018, 1, 9));
            offDays5.Add(new DateTime(2018, 1, 10));
            offDays5.Add(new DateTime(2018, 1, 11));

            List<Worker> workers = new List<Worker>
            {
                new Worker(0001, "staszek",offDays, 0),
                new Worker(0002, "heniek",offDays2, 0),
                new Worker(0003, "mirek",offDays3, 0),
                new Worker(0004, "zbyszek",offDays4, 0),
                new Worker(0005, "wojtek", offDays5, 0)
            };
            workers.OrderBy(x => x.overwork).ToList();

            return workers;
        }


        public int HowManyDays(DateTime first, DateTime last)
        {
            int count = (last - first).Days;

            return count;
        }
        // funkcja pobierajaca date first
        public static DateTime getStartDate()
        {
            DateTime date = default(DateTime);

            while (date == DateTime.MinValue)
            {
                try
                {
                    date = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Podaj odpowiedni format  daty: YYYY-MM-DD");
                }
            }

            return date;

        }

        // funkcja pobierajaca date end
        public static DateTime getEndDate()
        {
            DateTime date = default(DateTime);
            while (date == DateTime.MinValue)
            {
                try
                {
                    date = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Podaj datę zakończenia harmonogramu");
                }
            }


            return date;

        }
        // tworzenie listy z pracujacymi dniami
        public static List<DateTime> GetWorkedDays(DateTime start, DateTime end)
        {
            int count = (end - start).Days;
            List<DateTime> days = new List<DateTime>();
            for (var i = 0; i < count; i++)
            {
                if (!DayOffWork.IsDayOff(start.AddDays(i)))
                {
                    days.Add(start.AddDays(i));
                }


            }
            return days;
        }
        // funkcja zliczajaca ilosc przepracowanych godzin przez pracownika 
        public static List<Worker> CalculateOverwork(List<Worker> listWorker, List<History> listHistory)
        {
            if (!(listHistory == null))
            {
                foreach (Worker worker in listWorker)
                {
                    worker.overwork = listHistory.Where(x => x.id == worker.WorkerId && x.date <= DateTime.Now).Count();
                }

                return listWorker;
            }
            else
            {
                return listWorker;
            }

        }

        static void Main(string[] args)
        {
            Helpers dbHelper = new Helpers(dbPath);
            DateTime now = DateTime.Now;
            List<Worker> workList;
            // wczytanie jsona z  pracownikami 
            workList = dbHelper.loadJson<List<Worker>>("workList");

            if (workList == null || workList.Count() == 0)
            {
                workList = CreateWorkers();
                dbHelper.saveJson("workList", workList);
            }

            // wczytanie jsona z historia
            List<History> listHistory = dbHelper.loadJson<List<History>>("listHistory");

            if (listHistory == null || listHistory.Count() == 0)
            {
                listHistory = new List<History>();
            }
            // zliczenie ilosci przepracowanych godzin przez pracownikow i posortowanie ich 
            workList = CalculateOverwork(workList, listHistory);
            workList = workList.OrderBy(x => x.overwork).ToList();
            int howManyWorkers = workList.Count;

            ////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Witaj w programie! Wpisz 'quit' by wyjść.");
            Console.WriteLine("Wpisz H by wygererować harmonogram ");
            string str = Console.ReadLine();

            while (!(str.Equals("quit")))
            {

                while (!str.ToUpper().Equals("H"))
                {
                    try
                    {
                        str = Console.ReadLine();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Tymczasowo inne opcje niedostepne wcisnij H");
                        Console.WriteLine("Wpisz H by wygererować harmonogram ");
                    }
                }

                try
                {
                    // pobranie poczatku harmonogramu 
                    Console.WriteLine("Podaj datę rozpoczęcia  harmonogramu");
                    DateTime start = getStartDate();
                    while (start < now)
                    {
                        Console.WriteLine(" Data utworzenia harmonogramu nie może być wcześniejsza niż {0} ", now);
                        start = getStartDate();
                    }
                    Console.WriteLine(string.Format("Wybrana data rozpoczęcia to: {0}", start.ToShortDateString()));
                    // pobranie  zakonczenia  harmonogramu 
                    Console.WriteLine("Podaj datę zakończenia harmonogramu");
                    DateTime end = getEndDate();
                    while (end <= start)
                    {
                        Console.WriteLine(" Data zakończenia harmonogramu nie może być wcześniejsza niż data utworzenia harmonogramu");
                        end = getEndDate();
                    }
                    Console.WriteLine(string.Format("Wybrana data zakończenia to: {0}", end.ToShortDateString()));


                    List<DateTime> days = GetWorkedDays(start, end);

                    // Sprawdzenie ostatnich 3  dni historii 
                    listHistory = listHistory.Where(x => x.date <= start).ToList();
                    List<History> lastThree = listHistory.OrderByDescending(x => x.date).Take(3).ToList();

                    long lastWorkerID = -69;
                    int lastWorkerOccurency = 0;
                    History lastWorker;
                    if (lastThree.Count > 0)
                    {
                        lastWorkerID = lastThree[0].id;
                        lastWorkerOccurency = lastThree.Where(x => x.id == lastWorkerID).Count();
                        lastWorker = listHistory.OrderByDescending(x => x.date).FirstOrDefault();
                    }


                    // Generowanie mapy data - id_pracownika
                    Console.WriteLine("Generuje harmonogram");
                    Dictionary<DateTime, long> harmonogram = new Dictionary<DateTime, long>();
                    int harmonogramLenght = days.Count;
                    int a = 1;
                    int c = 0;
                    int j = 0;
                    Worker worker = workList.Where(x => x.WorkerId == lastWorkerID).FirstOrDefault();

                    if (worker == null || listHistory.Last().date < start)
                    {
                        worker = workList[0];
                    }

                    while (j != harmonogramLenght)
                    {
                        // sprawdzenie ostatniego pracownika 
                        if (worker.WorkerId == lastWorkerID)
                        {
                            switch (lastWorkerOccurency)
                            {
                                case 1:
                                    c = lastWorkerOccurency;
                                    break;

                                case 2:
                                    c = lastWorkerOccurency;
                                    break;
                                case 3:
                                    c = lastWorkerOccurency;
                                    break;
                                default:
                                    a = a % (howManyWorkers) + 1;
                                    c = 0;
                                    break;
                            }

                        }
                        lastWorkerID = -69;
                        // sprawdzenie czy aktualny  pracownik jest jedyny 
                        if (dbHelper.CantAnybodyElseWork(workList, worker.WorkerId, days[j]))
                        {
                            listHistory.Add(new History(days[j], worker.WorkerId));
                            harmonogram.Add(days[j], worker.WorkerId);
                            j++;
                            a = a % (howManyWorkers) + 1;
                            c = 0;
                        }
                        else
                        {
                            // przypisanie 
                            if (worker.CanWork(worker, days[j]) && c != 3)
                            {
                                listHistory.Add(new History(days[j], worker.WorkerId));
                                harmonogram.Add(days[j], worker.WorkerId);
                                c++;
                                j++;
                            }
                            else
                            {
                                a = a % (howManyWorkers) + 1;
                                c = 0;
                            }
                        }
                        worker = workList[a - 1];


                    }
                    foreach (KeyValuePair<DateTime, long> kvp in harmonogram)
                    {
                        Console.WriteLine("Data: {0} . Id pracownika: {1}", kvp.Key.Date.ToString("yyyy-MM-dd"), kvp.Value);
                    }
                    dbHelper.saveJson("listHistory", listHistory);
                    

                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Podaj odpowiedni format  daty: YYYY-MM-DD");

                };

                   str = Console.ReadLine();
            }
        }

    }


}

