using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheRace
{
    public class Race
    {
        private List<Racer> data;

        public Race(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            data = new List<Racer>();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Count => data.Count;

        public void Add(Racer racer)
        {
            if (data.Count < Capacity)
            {
                data.Add(racer);
            }
        }
        
        public bool Remove(string name)
        {
            Racer racer = data.FirstOrDefault(r => r.Name == name);

            return data.Remove(racer);
        }

        public Racer GetOldestRacer()
        {
            return data.OrderByDescending(racer => racer.Age).FirstOrDefault();
        }
        
        public Racer GetRacer(string name)
        {
            return data.FirstOrDefault(racer => racer.Name == name);
        }

        public Racer GetFastestRacer()
        {
            return data.OrderByDescending(racer => racer.Car.Speed).FirstOrDefault();
        }
        
        public string Report()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Racers participating at {Name}:");
            foreach (var racer in data)
            {
                result.AppendLine(racer.ToString());
            }

            return result.ToString().Trim();
        }
    }
}