using System.Collections.Generic;

namespace textAnalyzer
{
    public class Stats
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public int CountUpperCase { get; set; }

        public double Percentage { get; set; }
        public double PercentageUpperCase { get; set; }

        public Stats(string value)
        {
            Value = value.ToLower();
            Count = 1;
            CountUpperCase = value == value.ToUpper() ? 1 : 0;
        }

        public void Add(string l)
        {
            if (l == l.ToUpper())
            {
                CountUpperCase++;
            }
            Count++;
        }

        public void Concat(Stats s)
        {
            this.Count += s.Count;
            this.CountUpperCase += s.CountUpperCase;
        }
    }

    public class DicoStats
    {
        public Dictionary<string, Stats> dictionary { get; set; }

        public DicoStats()
        {
            dictionary = new Dictionary<string, Stats>();
        }

        public void Add(string value)
        {
            string lower = value.ToLower();
            if (!dictionary.ContainsKey(lower))
            {
                dictionary.Add(lower, new Stats(value));
            }
            else
            {
                dictionary[lower].Add(value);
            }
        }
    }
}