using System;
using System.Collections.Generic;

namespace OopDreamTeam
{
    public class ResultManager
    {

        private Dictionary<string, List<int>> userResults;

        public ResultManager()
        {
            userResults = new Dictionary<string, List<int>>();
        }


        public void AddResult(string userName, int score)
        {
            if (!userResults.ContainsKey(userName))
            {
                userResults[userName] = new List<int>();
            }

            userResults[userName].Add(score);
        }


        public double GetAverageScore(string userName)
        {
            if (userResults.ContainsKey(userName) && userResults[userName].Count > 0)
            {
                double sum = 0;
                foreach (int score in userResults[userName])
                {
                    sum += score;
                }

                return sum / userResults[userName].Count;
            }
            else
            {
                throw new Exception("User not found or no results.");
            }
        }


        public void PrintUserResults(string userName)
        {
            if (userResults.ContainsKey(userName))
            {
                Console.WriteLine($"Results for {userName}:");
                foreach (int score in userResults[userName])
                {
                    Console.WriteLine($" - Score: {score}");
                }
            }
            else
            {
                Console.WriteLine($"No results found for user: {userName}");
            }
        }
    }
}

