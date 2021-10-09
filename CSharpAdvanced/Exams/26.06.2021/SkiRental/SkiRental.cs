using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiRental
{
    public class SkiRental
    {
        public SkiRental(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Data = new List<Ski>(Capacity);
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Ski> Data { get; set; }
        public int Count => Data.Count;

        public void Add(Ski ski)
        {
            if (Data.Count < Capacity)
            {
                Data.Add(ski);
            }
        }

        public Boolean Remove(string manufacturer, string model)
        {
            int index = Data.FindIndex(ski => ski.Manufacturer == manufacturer && ski.Model == model);

            if (index != -1)
            {
                Data.RemoveAt(index);
                return true;
            }

            return false;
        }

        public Ski GetNewestSki()
        {
            return Data.OrderByDescending(ski => ski.Year).FirstOrDefault();
        }

        public Ski GetSki(string manufacturer, string model)
        {
            return Data.FirstOrDefault(ski => ski.Manufacturer == manufacturer && ski.Model == model);
        }

        public string GetStatistics()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"The skis stored in {Name}:");
            foreach (var ski in Data)
            {
                result.AppendLine(ski.ToString());
            }

            return result.ToString();
        }
    }
}