using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmonogram
{
    class Helpers
    {
        private string dbPath;
        public Helpers(string _dbPath)
        {
            dbPath = Path.GetDirectoryName(_dbPath);
        }

        public void saveJson(string name, object obj)
        {
            if (name == null || name == "") return;
            using (StreamWriter file = File.CreateText(Path.Combine(dbPath, name + ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }

        public T loadJson<T>(string name)
        {
            T obj = default(T);
            string filePath = Path.Combine(dbPath, name + ".json");

            if (File.Exists(filePath) == false) return obj;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                obj = JsonConvert.DeserializeObject<T>(json);
            }

            return obj;
        }

        public bool CantAnybodyElseWork(List<Worker> listWorker, long id, DateTime date)
        {
            bool canWork = true;
            //sprawdza ktokolwiek poza przeslanym ID moze pelnic dyzur
            foreach (Worker wrk in listWorker.Where(x => x.WorkerId != id).ToList())
            {
                if (wrk.CanWork(wrk,date) == true)
                {
                    canWork = false;
                    break;
                }
            }

            return canWork;

        }
      


       
    }
}
