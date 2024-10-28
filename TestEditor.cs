namespace OopDreamTeam
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string text, List<string> options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }

    public class TestEditor
    {
        private List<string> testNames = new List<string>();
        private Dictionary<string, List<Question>> tests = new Dictionary<string, List<Question>>();

        public void AddTest(string testName)
        {
            if (!testNames.Contains(testName))
            {
                testNames.Add(testName);
                tests[testName] = new List<Question>();
            }
            else
            {
                throw new InvalidOperationException($"Test '{testName}' already exists.");
            }
        }

        public void AddQuestionToTest(string testName, Question question)
        {
            if (tests.ContainsKey(testName))
            {
                tests[testName].Add(question);
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' wasn't found.");
            }
        }

        public void DisplayTestQuestions(string testName)
        {
            if (tests.ContainsKey(testName))
            {
                Console.WriteLine($"Test questions to '{testName}'");
                foreach (var question in tests[testName])
                {
                    Console.WriteLine($"- {question.Text}");
                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {question.Options[i]}");
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' wasn't found.");
            }
        }

        public void RemoveTest(string testName)
        {
            if (tests.ContainsKey(testName))
            {
                tests.Remove(testName);
                testNames.Remove(testName);
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' was not found.");
            }
        }
    }
}


