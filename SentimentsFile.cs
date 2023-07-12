using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TwitterTrends
{
    public class SentimentsFile
    {
        private string Path { get; set; }
        public SentimentsFile(string fileName)
        {
            Path = fileName;
        }

        public List<Sentiment> GetSentiments()
        {
            List<Sentiment> sentiments = (File.ReadLines(Path)).Select(x => new Sentiment
            {
                Word = x[0..x.IndexOf(',')],
                Value = Convert.ToDouble(x[(x.IndexOf(',') + 1)..])
            }).ToList();

            return sentiments;
        }
    }
}