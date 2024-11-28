using System;
using System.Collections.Generic;
using System.Linq;

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
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
            }

            if (!userResults.ContainsKey(userName))
            {
                userResults[userName] = new List<Test>();
            }
            userResults[userName].Add(test);
        }

        public double GetAverageScore(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
            }

            if (!userResults.ContainsKey(userName))
            {
                throw new KeyNotFoundException($"User '{userName}' not found.");
            }

            var tests = userResults[userName];
            if (tests.Count == 0)
            {
                throw new InvalidOperationException($"User '{userName}' has no test results.");
            }

            try
            {
                return tests.Average(test => test.CheckAnswers());
            }
            catch (TestCheckAnswersException ex)
            {
                throw new InvalidOperationException($"Failed to calculate average score: {ex.Message}");
            }
        }

        public List<(string TestName, double Score)> GetResults(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
            }

            if (!userResults.ContainsKey(userName))
            {
                throw new KeyNotFoundException($"User '{userName}' not found.");
            }

            try
            {
                return userResults[userName]
                    .Select(test => (test.Name, test.CheckAnswers()))
                    .ToList();
            }
            catch (TestCheckAnswersException ex)
            {
                throw new InvalidOperationException($"Failed to get results: {ex.Message}");
            }
        }
        public void SortTestsByScore(string userName, bool ascending = true)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));
            }

            if (!userResults.ContainsKey(userName))
            {
                throw new KeyNotFoundException($"User '{userName}' not found.");
            }

            try
            {
                userResults[userName] = ascending
                    ? userResults[userName].OrderBy(test => test.CheckAnswers()).ToList()
                    : userResults[userName].OrderByDescending(test => test.CheckAnswers()).ToList();
            }
            catch (TestCheckAnswersException ex)
            {
                throw new InvalidOperationException($"Failed to sort tests: {ex.Message}");
            }
        }
        
        public void SaveResultsToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var user in userResults)
                {
                    writer.WriteLine($"User: {user.Key}");
                    foreach (var test in user.Value)
                    {
                        writer.WriteLine($"  Test: {test.Name}, Score: {test.CheckAnswers():F2}");
                    }
                }
            }
        }
        
        public void LoadResultsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }

            userResults.Clear();

            string currentUser = null;
            foreach (var line in File.ReadLines(filePath))
            {
                if (line.StartsWith("User:"))
                {
                    currentUser = line.Replace("User:", "").Trim();
                    userResults[currentUser] = new List<Test>();
                }
                else if (currentUser != null && line.StartsWith("  Test:"))
                {
                    var parts = line.Replace("  Test:", "").Split(',');
                    var testName = parts[0].Trim();
                    var score = double.Parse(parts[1].Replace("Score:", "").Trim());
                    
                    userResults[currentUser].Add(new Test(testName, Enumerable.Empty<BaseQuestion>(), new DummyTestRunner()));
                }
            }
        }


    }
}
