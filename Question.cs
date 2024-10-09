using System;
using System.Collections.Generic;

namespace OopDreamTeam {

    public class BaseQuestion {
        public string Text { get; set; }
        public string Answer { get; set; }

        public BaseQuestion(string text, string answer) {
            Text = text;
            Answer = answer;
        }

        public bool CheckAnswer(string userAnswer) {
            return Answer.Equals(userAnswer, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class QuestionManager {
        private static QuestionManager _instance;
        private List<BaseQuestion> questions;

        private QuestionManager() {
            questions = new List<BaseQuestion>();
        }

        public static QuestionManager Instance {
            get {
                if (_instance == null)
                {
                    _instance = new QuestionManager();
                }
                return _instance;
            }
        }

        public void AddQuestion(BaseQuestion question) {
            questions.Add(question);
        }

        public List<BaseQuestion> GetAllQuestions() {
            return questions;
        }
    }
}

namespace OopDreamTeam.Tests {
    public class TestQuestion {
        public static void Run() {

            QuestionManager manager = QuestionManager.Instance;

            BaseQuestion question1 = new BaseQuestion("What is the largest planet in our solar system?", "Jupiter");
            BaseQuestion question2 = new BaseQuestion("In which year did the Titanic sink?", "1912");

            manager.AddQuestion(question1);
            manager.AddQuestion(question2);

            Console.WriteLine("Testing Question class:\n");

            Console.WriteLine($"Added questions:\n1. {question1.Text}\n2. {question2.Text}\n");

            List<BaseQuestion> questions = manager.GetAllQuestions();
            Console.WriteLine($"Number of questions in the manager: {questions.Count} (Expected: 2)\n");

            Console.WriteLine($"Testing question: \"{question1.Text}\"");
            Console.WriteLine($"User answer: \"Jupiter\", Expected: True, Actual: {question1.CheckAnswer("Jupiter")}");
            Console.WriteLine($"User answer: \"jUpiTer\", Expected: True, Actual: {question1.CheckAnswer("jUpiTer")}");
            Console.WriteLine($"User answer: \"Saturn\", Expected: False, Actual: {question1.CheckAnswer("Saturn")}\n");

            Console.WriteLine($"Testing question: \"{question2.Text}\"");
            Console.WriteLine($"User answer: \"1912\", Expected: True, Actual: {question2.CheckAnswer("1912")}");
            Console.WriteLine($"User answer: \"1911\", Expected: False, Actual: {question2.CheckAnswer("1911")}\n");

            Console.WriteLine("All tests completed.");
        }
    }
}
