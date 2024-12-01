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

        List<CheckboxQuestion.Option> options = new List<CheckboxQuestion.Option>
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

        List<CheckboxQuestion.Option> options = new List<CheckboxQuestion.Option>
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

    [Test]
    public void SortTests_ShouldSortAlphabetically()
    {
        editor.AddTest("TestC");
        editor.AddTest("TestA");
        editor.AddTest("TestD");
            
        editor.SortTests();

        var sortedTests = editor.SearchTests("");
        Assert.AreEqual(new List<string> {"TestA", "TestC", "TestD"}, sortedTests);
    }

    [Test]
    public void SortTests_ShouldWorkForEmptyList()
    {
        editor.SortTests();
        var sortedTests = editor.SearchTests("");
        Assert.AreEqual(0, sortedTests.Count);
    }

    [Test]
    public void SearchTests_ShouldReturnMatchingTests()
    {
        editor.AddTest("MathTest");
        editor.AddTest("ScienceTest");
        editor.AddTest("HistoryTest");

        var results = editor.SearchTests("Test");
        Assert.AreEqual(3, results.Count);
        
        results = editor.SearchTests("Science");
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("ScienceTest", results[0]);
    }

    [Test]
    public void SearchTests_ShouldHandleMultipleKeywords()
    {
        editor.AddTest("MathTest");
        editor.AddTest("ScienceTest");
        editor.AddTest("HistoryTest");
        
        var results = editor.SearchTests("Math Science");
        Assert.AreEqual(2, results.Count);
        Assert.Contains("MathTest", results);
        Assert.Contains("ScienceTest", results);
    }
    
    [Test]
    public void SaveToFile_ShouldCreateFile()
    {
        string fileName = "test_creation.json";

        editor.AddTest("Test1");
        editor.SaveToFile(fileName);

        Assert.IsTrue(File.Exists(fileName), "File was not created.");

        File.Delete(fileName);
    }
    
    [Test]
    public void SaveToFile_ShouldSaveTestsCorrectly()
    {
        string fileName = "test_save.json";

        editor.AddTest("Test1");
        editor.AddTest("Test2");

        editor.SaveToFile(fileName);

        string fileContent = File.ReadAllText(fileName);
        Assert.IsTrue(fileContent.Contains("Test1"));
        Assert.IsTrue(fileContent.Contains("Test2"));

        File.Delete(fileName);
    }
    
    [Test]
    public void LoadFromFile_ShouldLoadSavedTests()
    {
        string fileName = "test_load.json";

        editor.AddTest("Test1");
        editor.AddTest("Test2");
        editor.SaveToFile(fileName);

        var newEditor = new TestEditor();
        newEditor.LoadFromFile(fileName);

        var loadedTests = newEditor.SearchTests("");
        Assert.Contains("Test1", loadedTests);
        Assert.Contains("Test2", loadedTests);
        Assert.AreEqual(2, loadedTests.Count);

        File.Delete(fileName);
    }
    
    [Test]
    public void LoadFromFile_ShouldThrowExceptionIfFileDoesNotExist()
    {
        string fileName = "non_existent_file.json";

        Assert.Throws<FileNotFoundException>(() => editor.LoadFromFile(fileName));
    }
    
    [Test]
    public void LoadFromFile_ShouldNotOverwriteExistingTests()
    {
        string fileName = "test_data.json";

        editor.AddTest("ExistingTest");
        editor.SaveToFile(fileName);

        var newEditor = new TestEditor();
        newEditor.AddTest("NewTest");
        newEditor.LoadFromFile(fileName);

        var loadedTests = newEditor.SearchTests("");
        Assert.Contains("ExistingTest", loadedTests);
        Assert.Contains("NewTest", loadedTests);
        Assert.AreEqual(2, loadedTests.Count);

        File.Delete(fileName); 
    }
}
