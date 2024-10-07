namespace OopDreamTeam
{
    public class TempQuestion
    {
        public string Text { get; set; }
        public string Answer { get; set; }

        public TempQuestion(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }

        public bool Validate(string answer)
        {
            return answer == Answer;
        }
    }

    public class Test
    {
        public string Name { get; }
        private List<TempQuestion> questions;
        private string[] answers;

        public Test(string name, List<TempQuestion> questions)
        {
            Name = name;
            this.questions = questions;
            answers = new string[questions.Count];
        }

        public void AddAnswer(string answer, int index)
        {
            if (index >= questions.Count)
            {
                throw new IndexOutOfRangeException();
            }

            answers[index] = answer;
        }

        public int CheckAnswers()
        {
            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (answers[i] == null) continue;

                if (questions[i].Validate(answers[i]))
                {
                    score++;
                }
            }

            return score;
        }
    }
}

namespace OopDreamTeam.Tests {
    class TestTest {
        public static void Run() {
            Console.WriteLine("Testing Test class:");
            ResultShouldBe3();
            TestShouldThrowException();
        } 

        private static void ResultShouldBe3() 
        {
            Console.Write("Test class should return 3 when all answers are correct: ");
            Test test = new Test("Capitals", new List<TempQuestion>
            {
                new TempQuestion("What is the capital of France?", "Paris"),
                new TempQuestion("What is the capital of Germany?", "Berlin"),
                new TempQuestion("What is the capital of Italy?", "Rome"),
            });

            test.AddAnswer("Paris", 0);
            test.AddAnswer("Berlin", 1);
            test.AddAnswer("Rome", 2);

            if (test.CheckAnswers() == 3) 
            {
                Console.WriteLine("Test passed");
            } 
            else 
            {
                Console.WriteLine("Test failed");
            }
        }

        private static void TestShouldThrowException()
        {
            Console.Write("Test class should throw an exception: ");
            Test test = new Test("Test", new List<TempQuestion>());

            try
            {
                test.AddAnswer("A", 1);
            } catch
            {
                Console.WriteLine("Test passed");
                return;
            }

            Console.WriteLine("Test failed");
        }
    }
}
