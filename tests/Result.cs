namespace OopDreamTeam.Tests;

using NUnit.Framework;
using System;
using System.Collections.Generic;

public class TestResult 
{
    [Test]
    public void AddResult_ShouldStoreResultsCorrectly()
    {
        ResultManager resultManager = new ResultManager();

        resultManager.AddResult("Alice", 85);
        resultManager.AddResult("Alice", 90);

        double average = resultManager.GetAverageScore("Alice");
        Assert.That(average, Is.EqualTo(87.5));
    }

    [Test]
    public void GetAverageScore_ShouldThrowExceptionForUnknownUser()
    {
        ResultManager resultManager = new ResultManager();

        Assert.Throws<Exception>(() => resultManager.GetAverageScore("UnknownUser"));
    }

    [Test]
    public void GetAverageScore_ShouldReturnCorrectAverage()
    {
        ResultManager resultManager = new ResultManager();

        resultManager.AddResult("Bob", 60);
        resultManager.AddResult("Bob", 80);
        resultManager.AddResult("Bob", 70);

        double average = resultManager.GetAverageScore("Bob");
        Assert.That(average, Is.EqualTo(70.0));
    }

    [Test]
    public void GetUserResults_ShouldReturnNoResultsMessageForUnknownUser()
    {
        ResultManager resultManager = new ResultManager();

       
        Assert.Throws<Exception>(() => resultManager.GetAverageScore("David"));
    }

    [Test]
    public void AddResult_ShouldHandleMultipleUsers()
    {
        ResultManager resultManager = new ResultManager();

        resultManager.AddResult("Alice", 100);
        resultManager.AddResult("Bob", 50);

        double aliceAverage = resultManager.GetAverageScore("Alice");
        double bobAverage = resultManager.GetAverageScore("Bob");

        Assert.That(aliceAverage, Is.EqualTo(100.0));
        Assert.That(bobAverage, Is.EqualTo(50.0));
    }
}

