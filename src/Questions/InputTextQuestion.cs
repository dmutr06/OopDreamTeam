using System;

namespace OopDreamTeam;
public class InputTextQuestion : BaseQuestion
{
    public string CorrectAnswer { get; set; }

    public InputTextQuestion(string text, double score, string correctAnswer)
        : base(text, score)
    {
        CorrectAnswer = correctAnswer;
    }

    public override double CheckAnswer()
    {
        if (UserAnswer is not string userAnswerText)
            throw new InvalidOperationException("No valid answer has been saved or it is not a string.");

        return string.Equals(userAnswerText, CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? Score : 0;
    }
}
