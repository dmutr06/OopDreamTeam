namespace OopDreamTeam;

public partial class Test
{
    public readonly string Name;
    public readonly TestRunner runner;
    private (BaseQuestion Question, double Score)[] results;
    private DateTime startTime;
    private DateTime endTime;

    private TestState state;

    public Test(string name, IEnumerable<BaseQuestion> questions, TestRunner testRunner)
    {
        Name = name;
        results = questions.Select(q => (q, 0.0)).ToArray();
        state = new TestCreated(this);
        runner = testRunner;
    }

    public Test(string name, IEnumerable<BaseQuestion> questions)
        : this(name, questions, new DummyTestRunner()) {}

    private void SetState(TestState newState)
    {
        state = newState;
    }

    public void Start()
    {
        state.Start();
    }

    public void SetAnswer(object answer, uint idx)
    {
        state.SetAnswer(answer, idx);
    }

    public double CheckAnswers()
    {
        return state.CheckAnswers();
    }

    public void Shuffle()
    {
        state.Shuffle();
    }

    public BaseQuestion GetQuestion(uint idx)
    {
        if (idx >= results.Length)
            throw new TestBadQuestionIndexException(idx);

        return results[idx].Question;
    }

    public (BaseQuestion question, double score)[] GetResults()
    {
        return state.GetResults(); 
    }

    public BaseQuestion[] GetQuestions()
    {
        return results.Select(r => r.Question).ToArray();
    }
}



