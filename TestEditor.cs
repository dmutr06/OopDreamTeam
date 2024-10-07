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
                Console.WriteLine($"Test '{testName}' added");
            }
            else
            {
                Console.WriteLine("Test already exists");
            }
        }

        public void AddQuestionToTest(string testName, Question question)
        {
            if (tests.ContainsKey(testName))
            {
                tests[testName].Add(question);
                Console.WriteLine($"Question added in test '{testName}'");
            }
            else
            {
                Console.WriteLine($"Test '{testName}' wasn`t found");
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
                Console.WriteLine($"Test '{testName}' wasn`t found");
            }
        }
    }
}

namespace OopDreamTeam.Tests
{
    public class TestEditorTest
    {
        public static void Run()
        {
            TestEditor editor = new TestEditor();
            
            Console.Write("Write test name: ");
            string testName = Console.ReadLine();
            editor.AddTest(testName);
        
            while (true)
            {
                Console.WriteLine("Додати запитання? (так/ні)");
                string addQuestion = Console.ReadLine();
                if (addQuestion.ToLower() != "так")
                    break;

                Console.Write("Введіть текст запитання: ");
                string questionText = Console.ReadLine();

                Console.Write("Введіть варіанти відповідей через кому: ");
                List<string> options = Console.ReadLine().Split(',').ToList();

                Console.Write("Введіть номер правильної відповіді: ");
                int correctAnswerIndex = int.Parse(Console.ReadLine());

                Question question = new Question(questionText, options, correctAnswerIndex);
                editor.AddQuestionToTest(testName, question);
            }
            editor.DisplayTestQuestions(testName);
        }
    }
}


