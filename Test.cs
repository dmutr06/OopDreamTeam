namespace OopDreamTeam
{
    public class Test
    {
        public string Name { get; }
        private readonly List<BaseQuestion> questions;
        private string[] answers;

        public Test(string name, List<BaseQuestion> questions)
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

                if (questions[i].CheckAnswer(answers[i]))
                {
                    score++;
                }
            }

            return score;
        }
    }
}

