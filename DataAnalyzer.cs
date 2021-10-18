using System;
using System.Collections.Generic;

namespace textAnalyzer
{
    public class DataAnalyzer
    {
        const double PCTMIN = 0.01;
        public Dictionary<string, Stats> chars { get; set; }
        public Dictionary<string, Stats> bigrams { get; set; }
        public Dictionary<string, Stats> trigrams { get; set; }

        private int countChars;
        private int countBigrams;
        private int countTrigrams;

        public DataAnalyzer()
        {
            chars = new Dictionary<string, Stats>();
            bigrams = new Dictionary<string, Stats>();
            trigrams = new Dictionary<string, Stats>();
            countChars = countBigrams = countTrigrams = 0;
        }

        public void AddResult(FileAnalyzer fa)
        {
            countChars += ConcatDico(chars, fa.Letters.dictionary);
            countBigrams += ConcatDico(bigrams, fa.Bigram.dictionary);
            countTrigrams += ConcatDico(trigrams, fa.Trigram.dictionary);
        }

        public void ComputeGlobalStats()
        {
            ComputeDico(chars, countChars);
            ComputeDico(bigrams, countBigrams);
            ComputeDico(trigrams, countTrigrams);
        }

        private static void ComputeDico(Dictionary<string, Stats> dico, int max)
        {
            foreach (KeyValuePair<string, Stats> s in dico)
            {
                s.Value.Percentage = Math.Round((double)s.Value.Count / max * 100.0, 3);
                s.Value.PercentageUpperCase = (double)s.Value.CountUpperCase / s.Value.Count * 100.0;
                if (s.Value.Percentage <= PCTMIN) {
                    dico.Remove(s.Key);
                }
            }
        }

        private static int ConcatDico(Dictionary<string, Stats> a, Dictionary<string, Stats> b)
        {
            int count = 0;
            foreach (KeyValuePair<string, Stats> s in b)
            {
                if (!a.ContainsKey(s.Key))
                {
                    a.Add(s.Key, s.Value);
                }
                else
                {
                    a[s.Key].Concat(s.Value);
                }
                count += s.Value.Count;
            }
            return count;
        }
    }
}