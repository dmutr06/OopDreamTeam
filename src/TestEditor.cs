using System.Text.Json;
using Newtonsoft.Json;

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

        public void SortTests()
        {
            testNames.Sort();
        }

        public List<string> SearchTests(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return testNames;
            }
            return testNames.Where(name => keyword.Split(' ',
                StringSplitOptions.RemoveEmptyEntries).Any(word => name.Contains(word,
                StringComparison.OrdinalIgnoreCase))).ToList();

        }

        public void SaveToFile(string fileName)
        {
            try
            {
                var serializedTests = JsonConvert.SerializeObject(tests, Formatting.Indented,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                File.WriteAllText(fileName, serializedTests);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error saving to file: '{fileName}'", ex);
            }
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    var serializedTests = File.ReadAllText(fileName);
                    tests = JsonConvert.DeserializeObject<Dictionary<string, List<CheckboxQuestion>>>(serializedTests,
                        new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                    testNames = new List<string>(tests.Keys);
                }
                catch (JsonSerializationException ex)
                {
                    throw new InvalidDataException($"Error deserializing data from file: '{fileName}'", ex);
                }
            }
            else
            {
                throw new FileNotFoundException($"File '{fileName}' was not found");
            }
        }

        public void EditQuestion(string testName, int questionIndex, string newQuestionText)
        {
            if (tests.ContainsKey(testName))
            {
                if (questionIndex >= 0 && questionIndex < tests[testName].Count)
                {
                    tests[testName][questionIndex].Text = newQuestionText;
                }
                else
                {
                    throw new IndexOutOfRangeException("An invalid question index was specified");
                }
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' was not found");
            }
        }

        public void AddAnswerOption(string testName, int questionIndex, string answerText, bool isCorrect)
        {
            if (tests.ContainsKey(testName)) 
            {
                if (questionIndex >= 0 && questionIndex < tests[testName].Count)
                {
                    var question = tests[testName][questionIndex];

                    if (question is CheckboxQuestion checkboxQuestion)
                    {
                        checkboxQuestion.Options.Add(new CheckboxQuestion.Option(answerText, isCorrect));
                    }
                    else
                    {
                        throw new InvalidOperationException("This question is not a CheckboxQuestion");
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("An invalid question index was specified"); 
                }
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' was not found.");
            }
        }

        public void RemoveAnswerOption(string testName, int questionIndex, int optionIndex)
        {
            if (tests.ContainsKey(testName))
            {
                if (questionIndex >= 0 && questionIndex < tests[testName].Count)
                {
                    var question = tests[testName][questionIndex];

                    if (question is CheckboxQuestion checkboxQuestion)
                    {
                        if (optionIndex >= 0 && optionIndex < checkboxQuestion.Options.Count)
                        {
                            checkboxQuestion.Options.RemoveAt(optionIndex);
                        }
                        else
                        {
                            throw new IndexOutOfRangeException("An invalid answer index was specified");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("This question is not a CheckboxQuestion"); 
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("An invalid question index was specified"); 
                }
            }
            else
            {
                throw new KeyNotFoundException($"Test '{testName}' was not found"); 
            }
        }
    }
}
