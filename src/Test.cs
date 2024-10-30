namespace OopDreamTeam;

public class Test
{
    public readonly string Name;
    private readonly List<CheckboxQuestion> questions;
    private List<bool>[] answers;

    public Test(string name, List<CheckboxQuestion> questions)
    {
        Name = name;
        this.questions = questions;
        answers = new List<bool>[questions.Count];
    }

    public void AddAnswer(List<bool> answer, int idx)
    {
        if (idx >= questions.Count)
            throw new IndexOutOfRangeException();

        answers[idx] = answer;
    }

    public double CheckAnswers()
    {
        double score = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            if (answers[i] == null) continue;

            score += questions[i].CheckAnswer(answers[i]);
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
