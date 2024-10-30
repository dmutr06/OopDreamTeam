namespace OopDreamTeam;

public class Test
{
    public readonly string Name;
    private readonly List<CheckboxQuestion> questions;
    public List<bool>[] Answers { get; }

    public Test(string name, List<CheckboxQuestion> questions)
    {
        Name = name;
        this.questions = questions;
        Answers = new List<bool>[questions.Count];
    }

    public void SetAnswer(List<bool> answer, int idx)
    {
        if (idx >= questions.Count)
            throw new IndexOutOfRangeException();

        Answers[idx] = answer;
    }

    public double CheckAnswers()
    {
        double score = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            if (Answers[i] == null) continue;

            score += questions[i].CheckAnswer(Answers[i]);
        }

        return score;
    }

    public CheckboxQuestion GetQuestion(int idx)
    {
        if (idx >= questions.Count)
            throw new IndexOutOfRangeException();

        return questions[idx];
    }
}
