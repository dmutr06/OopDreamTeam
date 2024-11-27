namespace OopDreamTeam;

public partial class Test
{
    private abstract class TestState
    {
        protected Test test;

        public TestState(Test test) 
        {
            this.test = test;
        }

        public virtual void Start()
        {
            throw new TestStartException(this.GetType().Name);
        }
        public virtual void SetAnswer(object answer, uint idx)
        {
            throw new TestSetAnswerException(this.GetType().Name);
        }
        public virtual double CheckAnswers()
        {
            throw new TestCheckAnswersException(this.GetType().Name);
        }
        public virtual void Shuffle()
        {
            throw new TestShuffleException(this.GetType().Name);
        }
        public virtual (BaseQuestion Question, double Score)[] GetResults()
        {
            throw new TestGetResultsException(this.GetType().Name);
        }
    }
}
