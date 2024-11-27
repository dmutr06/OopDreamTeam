namespace OopDreamTeam;

public class TestBadQuestionIndexException : Exception {}

public class TestStateException : Exception
{
    public TestStateException(string message) : base(message) {}
}

public class TestNotCompletedException : TestStateException {
    public TestNotCompletedException() : base("Test is not completed") {}
}

public class TestNotCheckedException : TestStateException {
    public TestNotCheckedException() : base("Test is not checked") {}
}

public class Test
{
    private enum State
    {
        Created,
        InProgress,
        Completed,
        Checked
    }

    public readonly string Name;
    private (BaseQuestion Question, double Score)[] results;
    private DateTime startTime;
    private DateTime endTime;

    public TimeSpan TestDuration 
    { 
        get
        {
            if (state != State.Checked)
                throw new TestNotCheckedException();
            return endTime - startTime;
        }
    }

    private State state;

    public Test(string name, IEnumerable<BaseQuestion> questions)
    {
        Name = name;
        results = questions.Select(q => (q, 0.0)).ToArray();
        state = State.Created;
    }

    public void StartTest()
    {
        if (state != State.Created)
            throw new TestStateException("Test is already started");
        state = State.InProgress;
        startTime = DateTime.Now;
    }

    public void SetAnswer(object answer, int idx)
    {
        if (state == State.Checked || state == State.Created)
            throw new TestStateException("Can't set answer");
        if (idx >= results.Length)
            throw new TestBadQuestionIndexException();
        
        results[idx].Question.UserAnswer = answer;

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
        endTime = DateTime.Now;
        return results.Sum(r => r.Score = r.Question.CheckAnswer());
    }

    public (BaseQuestion question, double score)[] GetResults()
    {
        if (state != State.Checked)
            throw new TestNotCheckedException();
        return results; 
    }

    public void Shuffle()
    {
        if (state != State.Created)
            throw new TestStateException("Can't shuffle questions");

        Random rand = new Random();
        for (int i = results.Length - 1; i > 0; --i)
        {
            int j = rand.Next(i + 1);
            (results[i], results[j]) = (results[j], results[i]);
        }
    }
}
