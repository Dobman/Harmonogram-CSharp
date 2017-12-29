using System;
using System.Collections.Generic;
using System.Linq;

namespace Harmonogram
{
    class Worker
    {
        public long WorkerId { get; }
        public string name;
        public  List<DateTime> Holiday { get; }
       // public int period { get; set; }
        public int overwork { get; set; }
       
        public Worker(long workerId, string name, List<DateTime> holiday, int overwork)
        {
            this.WorkerId = workerId;
            this.name = name;
            this.Holiday = holiday;
           // this.period = numberOffHoliday;
            this.overwork = overwork;
        }

        public override string ToString()
        {
            return "ID: " + WorkerId.ToString() + " imie: " + name.ToString();
        }
        // metoda sprawdzająca czy dany  pracownik ma w tym czasie urlop
        public bool CanWork(Worker worker, DateTime today)
        {
            return !worker.Holiday.Contains(today);
        }
     
    }
    
   

}
