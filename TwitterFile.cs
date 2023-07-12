using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TwitterTrends
{
    public class TwitterFile
    {
        private string Path { get; set; }
        public TwitterFile(string filePath)
        {
            Path = filePath;
        }

        public List<Tweet> GetTweets()
        {
            List<Tweet> tweets = (File.ReadLines(Path)).Select(x => new Tweet
            {
                Text = ReadData(x, '\t'),
                Coordinates = ReadData(x, 1, '\t')
            }).ToList();
            
            return tweets;
        } 

        private string ReadData(string line, char parameter)
        {
            string data = line[(line.LastIndexOf(parameter) + 1)..];

            return data;
        }

        private List<double> ReadData(string line, int parameter1, char parameter2)
        {
            var data1 = line.Substring(parameter1, (line.IndexOf(parameter2) - 2));
            var data = line[parameter1..(line.IndexOf(parameter2) - 1)].Replace(" ", "").Split(',').Select(double.Parse).ToList();

            return data;
        }
    }
}