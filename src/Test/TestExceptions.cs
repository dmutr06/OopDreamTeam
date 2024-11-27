namespace OopDreamTeam;

public class TestBadQuestionIndexException : Exception 
{
    public TestBadQuestionIndexException(uint idx) : base($"Bad index - {idx}") {}
}

public class TestStateException : Exception
{
    public TestStateException(string message) : base(message) {}
}

public class TestSetAnswerException : TestStateException
{
    public TestSetAnswerException(string stateName) : base($"Can't set answer in state {stateName}") {}
}

public class TestCheckAnswersException : TestStateException
{
    public TestCheckAnswersException(string stateName) : base($"Can't check answers in state {stateName}") {}
}

public class TestStartException : TestStateException
{
    public TestStartException(string stateName) : base($"Can't start in state {stateName}") {}
}

public class TestShuffleException : TestStateException
{
    public TestShuffleException(string stateName) : base($"Can't shuffle in state {stateName}") {}
}
public class TestGetResultsException : TestStateException
{
    public TestGetResultsException(string stateName) : base($"Can't get results in state {stateName}") {}
}
