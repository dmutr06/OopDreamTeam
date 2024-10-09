class Program 
{
    static void Main(string[] args)
    {
        OopDreamTeam.Tests.TestTest.Run();
        Console.WriteLine("----------");
        OopDreamTeam.Tests.TestEditorTest.Run();
        Console.WriteLine("----------");
        OopDreamTeam.Tests.TestQuestion.Run();
        Console.WriteLine("----------");
        OopDreamTeam.Results.TestResult.Run();
    }
}
