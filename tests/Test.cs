namespace OopDreamTeam.Tests;

using NUnit.Framework;

public class TestTest
{
    private Test test = new Test(string.Empty, new List<CheckboxQuestion>(), new MockTestRunner());
    private BaseQuestion[] questions = 
    {
        new CheckboxQuestion(
            "question", 2,
            new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("1 option", true),
                new CheckboxQuestion.Option("2 option", true),
            }, false),
        new InputTextQuestion("text question", 2, "answer"),
        new SingleChoiceQuestion("single choice question", 3, new List<SingleChoiceQuestion.Option> 
                {
                    new SingleChoiceQuestion.Option("option 1", true),
                    new SingleChoiceQuestion.Option("option 2", false),
                    new SingleChoiceQuestion.Option("option 3", false),
                }), 
    };

    [SetUp]
    public void Setup()
    {
        test = new Test("test", questions, new MockTestRunner());
        test.Start();
    }

    [Test]
    public void SetAnswer_ShouldThrowError()
    {
        Assert.Throws<TestBadQuestionIndexException>(
            () => test.SetAnswer(0, 3)
        );
    }

    [Test]
    public void CheckAnswers_ShouldReturn7()
    {
        Assert.That(test.CheckAnswers(), Is.EqualTo(7));
    }

    [Test]
    public void GetQuestion_ShouldReturnFirstQuestion()
    {
        Assert.That(test.GetQuestion(0), Is.EqualTo(questions[0]));
    }
}
