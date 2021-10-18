using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace textAnalyzer
{
    class Program
    {
        const string dataPath = "..\\data";
        const string resultPath = "..\\data\\result\\result";
        static void Main(string[] args)
        {
            DataAnalyzer da = new DataAnalyzer();
            foreach (string fileName in Directory.GetFiles(dataPath))
            {
                FileAnalyzer fa = new FileAnalyzer();
                fa.ReadFile(fileName);
                fa.Analyze();
                da.AddResult(fa);
            }
            da.ComputeGlobalStats();
            StatsPresenter statsPresenter = new StatsPresenter();
            statsPresenter.chars = da.chars.Values.OrderByDescending(x => x.Count);
            statsPresenter.bigrams = da.bigrams.Values.OrderByDescending(x => x.Count);
            statsPresenter.trigrams = da.trigrams.Values.OrderByDescending(x => x.Count);


            using (StreamWriter w = new StreamWriter(resultPath + ".json"))
            {
                w.Write(JsonConvert.SerializeObject(statsPresenter));
            }

            ToCsv(statsPresenter.chars,"chars");
            ToCsv(statsPresenter.bigrams,"bigrams");
            ToCsv(statsPresenter.trigrams,"trigrams");


        }

        static void ToCsv(IEnumerable<Stats> list, string name)
        {
            using (StreamWriter w = new StreamWriter($"{resultPath}_{name}.csv"))
            {
                foreach (Stats s in list)
                {
                    w.WriteLine($"\"{s.Value}\";{s.Percentage}");
                }

            }

        }
    }
}
