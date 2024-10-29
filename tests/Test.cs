namespace OopDreamTeam.Tests;

using NUnit.Framework;

public class TestTest
{
    private Test test = new Test("", new List<CheckboxQuestion>());

    [SetUp]
    public void Setup()
    {
        test = new Test("test", new List<CheckboxQuestion> 
            {
                new CheckboxQuestion(
                        "question", 2, 
                        new List<CheckboxQuestion.Option>
                        {
                            new CheckboxQuestion.Option("1 option", true),
                            new CheckboxQuestion.Option("2 option", true),
                        }, false),
            }
        );

    }

    [Test]
    public void CheckAnswers_ShouldReturn1()
    {
        test.AddAnswer(new List<bool> { true, false }, 0);
        Assert.That(test.CheckAnswers(), Is.EqualTo(1.0));
    }

    [Test]
    public void AddAnswer_ShouldThrowError() {
      Assert.Throws<IndexOutOfRangeException>(
          () => test.AddAnswer(new List<bool>(), 1)
      );
    }
}
