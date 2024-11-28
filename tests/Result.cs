namespace OopDreamTeam.Tests;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

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
        var test = new Test("SampleTest", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());

        resultManager.AddTest(userName, test);

        var results = resultManager.GetResults(userName);
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("SampleTest", results[0].TestName);
    }

    [Test]
    public void GetAverageScore_ShouldReturnCorrectAverage()
    {
        string userName = "User2";
        var test1 = new Test("Test1", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());
        var test2 = new Test("Test2", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());

        resultManager.AddTest(userName, test1);
        resultManager.AddTest(userName, test2);

        double averageScore = resultManager.GetAverageScore(userName);

        Assert.AreEqual(0, averageScore); // За замовчуванням CheckAnswers повертає 0
    }

    [Test]
    public void GetAverageScore_ShouldThrowErrorForUnknownUser()
    {
        string unknownUser = "UnknownUser";

        var ex = Assert.Throws<KeyNotFoundException>(() => resultManager.GetAverageScore(unknownUser));
        Assert.AreEqual($"User '{unknownUser}' not found.", ex.Message);
    }

    [Test]
    public void GetResults_ShouldReturnCorrectResults()
    {
        string userName = "User3";
        var test1 = new Test("Test1", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());
        var test2 = new Test("Test2", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());

        resultManager.AddTest(userName, test1);
        resultManager.AddTest(userName, test2);

        var results = resultManager.GetResults(userName);

        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("Test1", results[0].TestName);
        Assert.AreEqual("Test2", results[1].TestName);
    }

    [Test]
    public void SortTestsByScore_ShouldSortAscending()
    {
        string userName = "User4";
        var test1 = new Test("LowScoreTest", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());
        var test2 = new Test("HighScoreTest", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());

        resultManager.AddTest(userName, test1);
        resultManager.AddTest(userName, test2);

        resultManager.SortTestsByScore(userName, ascending: true);

        var results = resultManager.GetResults(userName);

        Assert.AreEqual("LowScoreTest", results[0].TestName);
        Assert.AreEqual("HighScoreTest", results[1].TestName);
    }

    [Test]
    public void SortTestsByScore_ShouldThrowForUnknownUser()
    {
        string unknownUser = "UnknownUser";

        var ex = Assert.Throws<KeyNotFoundException>(() => resultManager.SortTestsByScore(unknownUser));
        Assert.AreEqual($"User '{unknownUser}' not found.", ex.Message);
    }

    [Test]
    public void SaveResultsToFile_ShouldSaveCorrectly()
    {
        string userName = "User5";
        var test1 = new Test("Test1", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());
        var test2 = new Test("Test2", Enumerable.Empty<BaseQuestion>(), new DummyTestRunner());

        resultManager.AddTest(userName, test1);
        resultManager.AddTest(userName, test2);

        string filePath = Path.GetTempFileName();
        resultManager.SaveResultsToFile(filePath);

        string[] lines = File.ReadAllLines(filePath);

        Assert.AreEqual(3, lines.Length); // User + 2 Tests
        Assert.AreEqual($"User: {userName}", lines[0]);
        Assert.IsTrue(lines[1].Contains("Test1"));
        Assert.IsTrue(lines[2].Contains("Test2"));

        File.Delete(filePath);
    }

    [Test]
    public void LoadResultsFromFile_ShouldLoadCorrectly()
    {
        string filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, new[]
        {
            "User: User6",
            "  Test: Test1, Score: 80.0",
            "  Test: Test2, Score: 90.0"
        });

        resultManager.LoadResultsFromFile(filePath);

        var results = resultManager.GetResults("User6");

        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("Test1", results[0].TestName);
        Assert.AreEqual("Test2", results[1].TestName);

        File.Delete(filePath);
    }

    [Test]
    public void LoadResultsFromFile_ShouldThrowForMissingFile()
    {
        string missingFile = "NonExistentFile.txt";

        var ex = Assert.Throws<FileNotFoundException>(() => resultManager.LoadResultsFromFile(missingFile));
        Assert.AreEqual($"File '{missingFile}' not found.", ex.Message);
    }
}

