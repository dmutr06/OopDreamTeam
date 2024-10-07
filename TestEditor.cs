namespace OopDreamTeam
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public int SelectedAnswerIndex { get; set; }

        public Question(string text, List<string> options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public bool IsCorrect()
        {
            return SelectedAnswerIndex == CorrectAnswerIndex;
        }
    }
    
    public class CheckTest
    {
        public string TestName { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();

        public CheckTest(string testName)
        {
            TestName = testName;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }

    public class TestEditor
    {
        private List<CheckTest> tests = new List<CheckTest>();

        public void AddTest(CheckTest test)
        {
            tests.Add(test);
        }
    }

    public class TestSession
    {
        private CheckTest test;
        private int currentQuestionIndex;

        public TestSession(CheckTest test)
        {
            this.test = test;
            currentQuestionIndex = 0;
        }

        public Question GetNextQuestion()
        {
            if (currentQuestionIndex < test.Questions.Count)
            {
                return test.Questions[currentQuestionIndex++];
            }

            return null;
        }
    }
}

namespace OopDreamTeam.Tests
{
    public class TestEditorTest
    {
        public static void Run()
        {
            CheckTest test = new CheckTest("Sample Test");
        
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

                test.AddQuestion(new Question(questionText, options, correctAnswerIndex));
            }
        
            TestEditor manager = new TestEditor();
            manager.AddTest(test);
        
            TestSession session = new TestSession(test);
        
            Question currentQuestion;
            while ((currentQuestion = session.GetNextQuestion()) != null)
            {
                Console.WriteLine(currentQuestion.Text);
                for (int i = 0; i < currentQuestion.Options.Count; i++)
                {
                    Console.WriteLine($"{i}. {currentQuestion.Options[i]}");
                }

                int selectedAnswer = int.Parse(Console.ReadLine());
                currentQuestion.SelectedAnswerIndex = selectedAnswer; 
            }
        }
    }
}


