namespace OopDreamTeam;

public partial class Test
{
    private class TestInProgress : TestState
    {
        public TestInProgress(Test test) : base(test) {}

        public override void SetAnswer(object answer, uint idx)
        {
            if (idx >= test.results.Length)
                throw new TestBadQuestionIndexException(idx);
            
            test.results[idx].Question.UserAnswer = answer;

            if (test.results.All(r => r.Question.UserAnswer != null))
                test.SetState(new TestCompleted(test));
        }
    }
}
