namespace OopDreamTeam
{
    public class TestEditor
    {
        private List<string> testNames = new List<string>();
        private Dictionary<string, List<CheckboxQuestion>> tests = new Dictionary<string, List<CheckboxQuestion>>();

        public void AddTest(string testName)
        {
            if (!testNames.Contains(testName))
            {
                testNames.Add(testName);
                tests[testName] = new List<CheckboxQuestion>();
            }
            else
            {
                throw new InvalidOperationException($"Test '{testName}' already exists.");
            }
        }

        public void AddQuestionToTest(string testName, CheckboxQuestion question)
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

        public Test GetTest(string testName)
        {
            if (tests.ContainsKey(testName))
            {
                List<CheckboxQuestion> questions = tests[testName];
                return new Test(testName, questions);
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' was not found");
            }
        }
    }
}
