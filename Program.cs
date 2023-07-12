using System;
using System.Collections.Generic;
using System.IO;

namespace TwitterTrends
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            TwitterFile twitterFile = new TwitterFile(@"C:\Users\alimac\Downloads\cali_tweets2014.txt");
            JsonFile jsonFile = new JsonFile(@"C:\Users\alimac\Downloads\states.json");
            List<State> states = jsonFile.GetStates();
            using StreamWriter fileOut = File.CreateText(@"C:\Users\alimac\Desktop\statesMood.txt");


            foreach (var state in states)
            {
                fileOut.WriteLine($"{state.Name}\t{state.Mood}");
            }
        }
    }
}
