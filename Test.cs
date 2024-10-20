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

