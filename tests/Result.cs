namespace OopDreamTeam.Tests;

using NUnit.Framework;
using System;
using System.Collections.Generic;

[TestFixture]
public class ResultManagerTests
{
    private ResultManager resultManager;

    [SetUp]
    public void Setup()
    {
        resultManager = new ResultManager();
    }

    [Test]
    public void AddTest_ShouldAddNewTest()
    {
        string userName = "User1";
        var questions = new List<CheckboxQuestion>();
        var test = new Test("SampleTest", questions);

        resultManager.AddTest(userName, test);

        Assert.DoesNotThrow(() => resultManager.PrintUserResults(userName));
    }

    [Test]
    public void GetAverageScore_ShouldReturnCorrectAverage()
    {
        string userName = "User2";
        var questions1 = new List<CheckboxQuestion>();
        var test1 = new Test("Test1", questions1);

        var questions2 = new List<CheckboxQuestion>();
        var test2 = new Test("Test2", questions2);

        resultManager.AddTest(userName, test1);
        resultManager.AddTest(userName, test2);

        double averageScore = resultManager.GetAverageScore(userName);

        Assert.AreEqual(0, averageScore);
    }

    [Test]
    public void GetAverageScore_ShouldThrowErrorForUnknownUser()
    {
        string unknownUser = "UnknownUser";

        Assert.Throws<Exception>(() => resultManager.GetAverageScore(unknownUser));
    }

    [Test]
    public void PrintUserResults_ShouldPrintResultsForExistingUser()
    {
        string userName = "User3";
        var questions = new List<CheckboxQuestion>();
        var test = new Test("Test3", questions);

        resultManager.AddTest(userName, test);
        Assert.DoesNotThrow(() => resultManager.PrintUserResults(userName));
    }

    [Test]
    public void PrintUserResults_ShouldShowNoResultsForUnknownUser()
    {
        string unknownUser = "UnknownUser";
        Assert.DoesNotThrow(() => resultManager.PrintUserResults(unknownUser));
    }
}
