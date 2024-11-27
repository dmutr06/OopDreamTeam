namespace OopDreamTeam;

public interface TestRunner
{
    void Run(Test test);
}

public class MockTestRunner : TestRunner
{
    public void Run(Test test)
    {
        BaseQuestion[] questions = test.GetQuestions();

        for (uint i = 0; i < questions.Length; ++i)
        {
            switch (questions[i])
            {
                case CheckboxQuestion q:
                    test.SetAnswer(q.Options.Select(option => option.IsRight).ToList(), i);
                    break;
                case InputTextQuestion q:
                    test.SetAnswer(q.CorrectAnswer, i);
                    break;
                case MatchingQuestion q:
                    test.SetAnswer(Enumerable.Range(0, q.Pairs.Count).ToList(), i);
                    break;
                case OrderingQuestion q:
                    test.SetAnswer(q.CorrectOrder, i);
                    break;
                case SingleChoiceQuestion q:
                    test.SetAnswer(q.Options.FindIndex(opt => opt.IsCorrect), i);
                    break;

            }
        }
    }
}

public class DummyTestRunner : TestRunner
{
    public void Run(Test _) {}
}
