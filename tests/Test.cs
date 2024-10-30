namespace OopDreamTeam.Tests;

using NUnit.Framework;

public class TestTest
{
    private Test test = new Test(string.Empty, new List<CheckboxQuestion>());
    private List<CheckboxQuestion> questions = new List<CheckboxQuestion>
    {
        new CheckboxQuestion(
            "question", 2,
            new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("1 option", true),
                new CheckboxQuestion.Option("2 option", true),
            }, false),
    };

    [SetUp]
    public void Setup() => test = new Test("test", questions);

    [Test]
    public void CheckAnswers_ShouldReturn1()
    {
        test.SetAnswer(new List<bool> { true, false }, 0);
        Assert.That(test.CheckAnswers(), Is.EqualTo(1.0));
    }

    [Test]
    public void SetAnswer_ShouldThrowError()
    {
        Assert.Throws<IndexOutOfRangeException>(
            () => test.SetAnswer(new List<bool>(), 1)
        );
    }

    [Test]
    public void GetQuestion_ShouldReturnFirstQuestion()
    {
        Assert.That(test.GetQuestion(0), Is.EqualTo(questions[0]));
    }
}
