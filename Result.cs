using System;
using System.Collections.Generic;

namespace OopDreamTeam
{
    public class ResultManager
    {
        private Dictionary<string, List<Test>> userResults;

        public ResultManager()
        {
            userResults = new Dictionary<string, List<Test>>();
        }

        public void AddTest(string userName, Test test)
        {
            if (!userResults.ContainsKey(userName))
            {
                userResults[userName] = new List<Test>();
            }
            userResults[userName].Add(test);
        }

        public double GetAverageScore(string userName)
        {
            if (userResults.ContainsKey(userName) && userResults[userName].Count > 0)
            {
                double totalScore = 0;
                int numberOfTests = userResults[userName].Count;

                foreach (var test in userResults[userName])
                {
                    totalScore += test.CheckAnswers();
                }

                return totalScore / numberOfTests;
            }
            else
            {
                throw new Exception("User not found or no results available.");
            }
        }

        public void PrintUserResults(string userName)
        {
            if (userResults.ContainsKey(userName))
            {
                Console.WriteLine($"Results for {userName}:");
                foreach (var test in userResults[userName])
                {
                    Console.WriteLine($" - Test: {test.Name}, Score: {test.CheckAnswers()}");
                }
            }
            else
            {
                Console.WriteLine($"No results found for user: {userName}");
            }
        }
    }
}


