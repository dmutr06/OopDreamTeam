namespace OopDreamTeam.Tests;

using NUnit.Framework;

public class TestTest
{
    [Test]
    public void CheckAnswers_ShouldReturn1()
    {
        Test test = new Test("test", new List<TempQuestion>([new TempQuestion("question", "answer")]));
        test.AddAnswer("answer", 0);
        Assert.That(test.CheckAnswers(), Is.EqualTo(1));
    }

    [Test]
    public void AddAnswer_ShouldThrowError() {
      Test test = new Test("test", new List<TempQuestion>([new TempQuestion("question", "answer")]));

      Assert.Throws<IndexOutOfRangeException>(
          () => test.AddAnswer("answer", 1)
      );
    }
}
