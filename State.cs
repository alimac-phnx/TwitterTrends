using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace TwitterTrends
{
    public class State
    {
        public string Name { get; set; }
        public List<object>[,] Coordinates { get; set; }
        public double Mood { get { return CalculateTheMood(); } set { } }

        private double MinValue(double first, int position)
        {
            double min = first;

            for (int i = 0; i < Coordinates.Length; i++)
            {
                //minLng = Coordinates[i, 0].Cast<List<double>>().ToList().Min(x => (double)x[0]);
                foreach (List<double> item in Coordinates[i, 0])
                {
                    if (min > item[position])
                    {
                        min = item[position];
                    }
                }
            }

            return min;
        }
        private double MinSingleValue(double first, int position)
        {
            double min = first;

            for (int i = 0; i < Coordinates.Length; i++)
            {
                for (int j = 0; j < Coordinates[0, i].Count; j++)
                {
                    foreach (var item in Coordinates)
                    {
                        var h = item[position];
                        if (min > (double)item[position])
                        {
                            min = (double)item[position];
                        }
                    }
                }
            }

            return min;
        }

        private double MaxValue(double first, int position)
        {
            double max = first;

            for (int i = 0; i < Coordinates.Length; i++)
            {
                //minLng = Coordinates[i, 0].Cast<List<double>>().ToList().Min(x => (double)x[0]);
                foreach (List<double> item in Coordinates[i, 0])
                {
                    if (max < item[position])
                    {
                        max = item[position];
                    }
                }
            }

            return max;
        }
        private double MaxSingleValue(double first, int position)
        {
            double max = first;

            for (int i = 0; i < Coordinates.Length; i++)
            {
                for (int j = 0; j < Coordinates[0, i].Count; j++)
                {
                    foreach (var item in Coordinates)
                    {
                        if (max < (double)item[position])
                        {
                            max = (double)item[position];
                        }
                    }
                }
            }

            return max;
        }

        private double GetMinLng()
        {
            double minLng;
            switch (Name)
            {
                case "WA": case "HI": case "VA": case "AK": case "MD": case "MI": case "RI":
                    minLng = MinValue(((List<double>)Coordinates[0, 0][0])[0], 0);
                    break;
                default:
                    minLng = MinSingleValue((double)Coordinates[0, 0][0], 0);
                    break;
            }

            return minLng;
        }
        private double GetMinLat()
        {
            double minLat;
            switch (Name)
            {
                case "WA": case "HI": case "VA": case "AK": case "MD": case "MI": case "RI":
                    minLat = MinValue(((List<double>)(Coordinates[0, 0][0]))[1], 1);
                    break;
                default:
                    minLat = MinSingleValue((double)(Coordinates[0, 0])[1], 1);
                    break;
            }

            return minLat;
        }

        private double GetMaxLng()
        {
            double maxLng;
            switch (Name)
            {
                case "WA": case "HI": case "VA": case "AK": case "MD": case "MI": case "RI":
                    maxLng = MaxValue(((List<double>)(Coordinates[0, 0][0]))[0], 0);
                    break;
                default:
                    maxLng = MaxSingleValue((double)(Coordinates[0, 0])[0], 0);
                    break;
            }

            return maxLng;
        }
        private double GetMaxLat()
        {
            double maxLat;
            switch (Name)
            {
                case "WA": case "HI": case "VA": case "AK": case "MD": case "MI": case "RI":
                    maxLat = MaxValue(((List<double>)(Coordinates[0, 0][0]))[1], 1);
                    break;
                default:
                    maxLat = MaxSingleValue((double)(Coordinates[0, 0])[1], 1);
                    break;
            }

            return maxLat;
        }

        public List<double> GetStateCenter()
        {
            return new List<double>() { (GetMinLat() + GetMaxLat()) / 2, (GetMinLng() + GetMaxLng()) / 2 };
        }

        public double CalculateTheMood()
        {
            double value = 0;
            bool flag = false;
            int counter = 0;
            List<Tweet> tweets = new List<Tweet>();//new TwitterFile(@"C:\Users\alimac\Downloads\cali_tweets2014.txt").GetTweets();
            foreach (var tweet in new TwitterFile(@"C:\Users\alimac\Downloads\cali_tweets2014.txt").GetTweets())
            {
                if (tweet.Region == Name)
                {
                    tweets.Add(tweet);
                    tweet.Mood = tweet.CalculateTheMood();
                    
                }
            }
            if (tweets.All(x => x.Mood == 10_000))
            {
                flag = true;
            }
            else { flag = false; }
            foreach (var tweet in new TwitterFile(@"C:\Users\alimac\Downloads\cali_tweets2014.txt").GetTweets())
            {
                if (tweet.Region == Name)
                {
                    counter++;
                    if (flag == false)
                    {
                        tweet.Mood = tweet.CalculateTheMood();
                        if (tweet.Mood == 10_000) { tweet.Mood = 0; }
                        value += tweet.Mood;
                    }
                    else { return 10_000; }
                }
            }
            if (counter == 0) { return 10_000; }
            else { return value; }
        }
    }
}