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
        private List<TempQuestion> questions;
        private string[] answers;

        public Test(List<TempQuestion> questions)
        {
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
            Console.WriteLine("Test class should return 3 when all answers are correct\n");
            Test test = new Test(new List<TempQuestion>
            {
                new TempQuestion("What is the capital of France?", "Paris"),
                new TempQuestion("What is the capital of Germany?", "Berlin"),
                new TempQuestion("What is the capital of Italy?", "Rome"),
            });

            test.AddAnswer("Paris", 0);
            test.AddAnswer("Berlin", 1);
            test.AddAnswer("Rome", 2);

            if (test.CheckAnswers() == 3) {
                Console.WriteLine("Test passed");
            } else {
                Console.WriteLine("Test failed");
            }
        } 
    }
}