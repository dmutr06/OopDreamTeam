namespace OopDreamTeam;

public partial class Test
{
    private class TestCreated : TestState
    {
        public TestCreated(Test test) : base(test) {}
        public override void Start()
        {
            test.SetState(new TestInProgress(test));
            test.startTime = DateTime.Now;
            test.runner.Run(test);
        }

        public override void Shuffle()
        {
            Random rand = new Random();
            for (int i = test.results.Length - 1; i > 0; --i)
            {
                int j = rand.Next(i + 1);
                (test.results[i], test.results[j]) = (test.results[j], test.results[i]);
            }
        }
    }
}
