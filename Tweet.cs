using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace TwitterTrends
{
    public class Tweet
    {
        public List<double> Coordinates { get; set; }
        public string Text { get; set; }
        public string Region { get { return GetTweetBelonging(); } set { } }
        public double mood;
        public double Mood { get { return mood; } set { mood = value; } }


        private string GetTweetBelonging()
        {
            JsonFile jsonFile = new JsonFile(@"C:\Users\alimac\Downloads\states.json");
            List<State> states = jsonFile.GetStates();

            DistFormula formula = new DistFormula(Coordinates[0] * Math.PI / 180, Coordinates[1] * Math.PI / 180, states[0].GetStateCenter()[0] * Math.PI / 180, states[0].GetStateCenter()[1] * Math.PI / 180);

            double min = formula.Calculate();

            string region = null;

            for (int i = 1; i < states.Count; i++)
            {
                formula = new DistFormula(Coordinates[0] * Math.PI / 180, Coordinates[1] * Math.PI / 180, states[i].GetStateCenter()[0] * Math.PI / 180, states[i].GetStateCenter()[1] * Math.PI / 180);

                double dist = formula.Calculate();

                if (min > dist)
                {
                    min = dist;
                    region = states[i].Name;
                }
            }

            return region;
        }
        public double CalculateTheMood()
        {
            double value = 0;

            List<string> words = Text.Trim().Split().ToList();
            SentimentsFile sentimentsFile = new SentimentsFile(@"C:\Users\alimac\Downloads\sentiments.txt");
            int counter = 0;

            foreach (var sentiment in sentimentsFile.GetSentiments())
            {
                foreach (var word in words)
                {
                    if (word == sentiment.Word)
                    {
                        value += sentiment.Value;
                        counter++;
                    }
                    //List<string> sentWord = sentiment.Word.Split().ToList();
                    //foreach (var w in sentWord)
                    //{
                    //    if (word == w)
                    //    {
                    //        value += sentiment.Value;
                    //        counter++;
                    //    }
                    //}
                }
            }
            if (counter == 0) { return mood = 10_000; }
            else { return mood = value; }
        }
    }
}