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
        
        Assert.That(() => editor.GetTest(testName), Throws.Nothing);
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
        
        List<CheckboxQuestion.Option> options = new  List<CheckboxQuestion.Option>
        {
            new CheckboxQuestion.Option("Option1", false),
            new CheckboxQuestion.Option("Option2", true),
            new CheckboxQuestion.Option("Option3", false)
        };

        CheckboxQuestion question = new CheckboxQuestion(
            "Sample Question?", 
            1, 
            options, 
            strictGrading: false 
        );
        
        editor.AddQuestionToTest(testName, question);
        
        Assert.DoesNotThrow(() => editor.GetTest(testName));
    }

    [Test]
    public void AddQuestionToTest_ShouldThrowErrorWhenTestDoesNotExists()
    {
        string nonExistenTest = "nonExistenTest";
        
        List<CheckboxQuestion.Option> options = new  List<CheckboxQuestion.Option>
        {
            new CheckboxQuestion.Option("Option1", false),
            new CheckboxQuestion.Option("Option2", true),
            new CheckboxQuestion.Option("Option3", false)
        };
        
        CheckboxQuestion question = new CheckboxQuestion(
            "Sample Question?",
            1, 
            options, 
            strictGrading: false 
        );

        Assert.Throws<KeyNotFoundException>(() => editor.AddQuestionToTest(nonExistenTest, question));
    }

    [Test]
    public void RemoveTest_ShouldRemoveExistingTest()
    {
        string testName = "RemovableTest";
        editor.AddTest(testName);
        
        editor.RemoveTest(testName);

        Assert.Throws<KeyNotFoundException>(() => editor.GetTest(testName));
    }

    [Test]
    public void RemoveTest_ShouldThrowErrorWhenTestDoesNotExist()
    {
        string testName = "NonRemovableTest";

        Assert.Throws<KeyNotFoundException>(() => editor.RemoveTest(testName));
    }
} 