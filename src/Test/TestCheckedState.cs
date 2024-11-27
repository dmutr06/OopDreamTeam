namespace OopDreamTeam;

public partial class Test
{
    private class TestChecked: TestState
    {
        public TestChecked(Test test) : base(test) {}
        
        public override (BaseQuestion, double)[] GetResults()
        {
            return test.results;
        }
    }
}
