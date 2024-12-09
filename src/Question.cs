namespace OopDreamTeam;

public abstract class BaseQuestion
{
    public string Text { get; set; }
    public double Score { get; set; }
    public object? UserAnswer { get;  set; }

    protected BaseQuestion(string text, double score)
    {
        Text = text;
        Score = score;
    }

    public abstract double CheckAnswer();
}
