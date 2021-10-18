using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace textAnalyzer
{

    public class FileAnalyzer
    {
        public string FileName { get; set; }

        public DicoStats Letters { get; set; }
        public DicoStats Trigram { get; set; }
        public DicoStats Bigram { get; set; }
        private string Content { get; set; }

        public FileAnalyzer()
        {
            Letters = new DicoStats();
            Bigram = new DicoStats();
            Trigram = new DicoStats();
            Content = string.Empty;
            FileName = string.Empty;
        }

        public void ReadFile(string path)
        {
            FileName = path;
            using (StreamReader sr = new StreamReader(path))
            {
                Content = sr.ReadToEnd();
            }
        }

        public Task Analyze()
        {
            for (int i = 0; i < Content.Length; i++)
            {
                string l = Content[i].ToString();
                Letters.Add(l);
                if (i < Content.Length - 1)
                {
                    string bi = Content[i].ToString() + Content[i + 1];
                    // we don’t care about space and returns here
                    if (!bi.Contains(" ") && !bi.Contains('\n') && !bi.Contains('\r'))
                    {
                        Bigram.Add(bi);
                    }
                }
                if (i < Content.Length - 2)
                {
                    string tri = Content[i].ToString() + Content[i + 1] + Content[i + 2];
                    if (!tri.Contains(" ") && !tri.Contains('\n') && !tri.Contains('\r'))
                    {
                        Trigram.Add(tri);
                    }
                }
            }

            return null;
        }
    }

}

