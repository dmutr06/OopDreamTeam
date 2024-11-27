namespace OopDreamTeam;

public class TestCheckedException : Exception {}
public class TestBadQuestionIndexException : Exception {}
public class TestNotCompletedException : Exception {}
public class TestNotCheckedException: Exception {}

public class Test
{
    private enum State
    {
        InProgress,
        Completed,
        Checked
    }

    public readonly string Name;
    private (BaseQuestion Question, double Score)[] results;

    private State state;

    public Test(string name, IEnumerable<BaseQuestion> questions)
    {
        Name = name;
        results = questions.Select(q => (q, 0.0)).ToArray();
        state = State.InProgress;
    }

    public void SetAnswer(object answer, int idx)
    {
        if (state == State.Checked)
            throw new TestCheckedException();
        if (idx >= results.Length)
            throw new TestBadQuestionIndexException();
        
        results[idx].Question.SaveAnswer(answer);

        if (results.All(r => r.Question.UserAnswer != null))
            state = State.Completed;
        else
            state = State.InProgress;
    }

    public double CheckAnswers()
    {
        if (state != State.Completed)
            throw new TestNotCompletedException();

        state = State.Checked;
        return results.Sum(r => r.Score = r.Question.CheckAnswer());
    }

    public (BaseQuestion question, double score)[] GetResults()
    {
        if (state != State.Checked)
            throw new TestNotCheckedException();
        return results; 
    }
}
