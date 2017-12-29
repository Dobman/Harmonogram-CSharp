using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonogram
{
    class History
    {
        public DateTime  date ;
        public long id;

        public History(DateTime date, long id)
        {
            this.date = date;
            this.id = id;
        }
    }
}
