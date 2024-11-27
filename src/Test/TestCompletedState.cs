namespace OopDreamTeam;

public partial class Test
{
    private class TestCompleted : TestState
    {
        public TestCompleted(Test test) : base(test) {}

        public override void SetAnswer(object answer, uint idx)
        {
            if (idx >= test.results.Length)
                throw new TestBadQuestionIndexException(idx);
            
            test.results[idx].Question.UserAnswer = answer;

            if (!test.results.All(r => r.Question.UserAnswer != null))
                test.SetState(new TestInProgress(test));
        }

        public override double CheckAnswers()
        {
            test.endTime = DateTime.Now;
            test.SetState(new TestChecked(test));
            return test.results.Sum(r => r.Score = r.Question.CheckAnswer());
        }
    }
}
