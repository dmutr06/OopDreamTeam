namespace OopDreamTeam.Tests;

using NUnit.Framework;

public class TestTestEditor
{
    private TestEditor editor;

    [SetUp]
    public void Setup()
    {
        editor = new TestEditor();
    }

    [Test]
    public void AddTest_ShouldAddNewTest()
    {
        string testName = "SampleTest";
        editor.AddTest(testName);
        
        Assert.That(() => editor.DisplayTestQuestions(testName), Throws.Nothing);
    }

    [Test]
    public void AddTest_ShouldNotAddDuplicateTest()
    {
        string testName = "DuplicateTest";
        editor.AddTest(testName);
        
        Assert.Throws<InvalidOperationException>(() => editor.AddTest(testName));
    }

    [Test]
    public void AddQuestionToTest_ShouldAddQuestionWhenTestExists()
    { 
        string testName = "TestWithQuestions";
        editor.AddTest(testName);

        Question question = new Question(
            "Sample Question?", 
            new List<string>{"Option1", "Option2", "Option3"}, 
            1
        );
        
        editor.AddQuestionToTest(testName, question);
        
        Assert.DoesNotThrow(() => editor.DisplayTestQuestions(testName));
    }

    [Test]
    public void AddQuestionToTest_ShouldThrowErrorWhenTestDoesNotExists()
    {
        string nonExistenTest = "nonExistenTest";
        
        Question question = new Question(
            "Sample Question?", 
            new List<string>{"Option1", "Option2", "Option3"}, 
            1
        );

        Assert.Throws<KeyNotFoundException>(() => editor.AddQuestionToTest(nonExistenTest, question));
    }

    [Test]
    public void RemoveTest_ShouldRemoveExistingTest()
    {
        string testName = "RemovableTest";
        editor.AddTest(testName);
        
        editor.RemoveTest(testName);

        Assert.Throws<KeyNotFoundException>(() => editor.DisplayTestQuestions(testName));
    }

    [Test]
    public void RemoveTest_ShouldThrowErrorWhenTestDoesNotExist()
    {
        string testName = "NonRemovableTest";

        Assert.Throws<KeyNotFoundException>(() => editor.RemoveTest(testName));
    }
} 