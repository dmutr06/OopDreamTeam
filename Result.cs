using System;
using System.Collections.Generic;

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
            throw new ArgumentException("User not found or no results.");
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

namespace OopDreamTeam.Results
{
    class TestResult
    {
        public static void Run()
        {
            ResultManager resultManager = new ResultManager();

            resultManager.AddResult("Alice", 85);
            resultManager.AddResult("Alice", 90);
            resultManager.AddResult("Alice", 78);

            resultManager.AddResult("Bob", 65);
            resultManager.AddResult("Bob", 70);
            resultManager.AddResult("Charlie", 95);

            try
            {
                Console.WriteLine("Average score for Alice: " + resultManager.GetAverageScore("Alice"));
                Console.WriteLine("Average score for Bob: " + resultManager.GetAverageScore("Bob"));
                Console.WriteLine("Average score for Charlie: " + resultManager.GetAverageScore("Charlie"));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            resultManager.PrintUserResults("Alice");
            resultManager.PrintUserResults("Bob");
            resultManager.PrintUserResults("Charlie");

            resultManager.PrintUserResults("David");
        }
    }
}
