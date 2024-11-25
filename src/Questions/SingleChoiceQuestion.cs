using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam;

public class SingleChoiceQuestion : BaseQuestion
{
    public class Option
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Option(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
    }

    public List<Option> Options { get; set; }
    private const int maxOptions = 26;

    public SingleChoiceQuestion(string text, double score, List<Option> options)
        : base(text, score)
    {
        Options = options ?? throw new ArgumentNullException(nameof(options), "Options cannot be null.");

        if (Options.Count > maxOptions)
            throw new InvalidOperationException($"Number of options cannot exceed {maxOptions}.");

        if (Options.Count(option => option.IsCorrect) != 1)
            throw new InvalidOperationException("There must be exactly one correct answer.");
    }

    public override double CheckAnswer()
    {
        if (UserAnswer is not int userAnswerIndex)
            throw new InvalidOperationException("No valid answer has been saved or it is not an integer.");

        if (userAnswerIndex < 0 || userAnswerIndex >= Options.Count)
            throw new ArgumentOutOfRangeException(nameof(UserAnswer), "Selected option is out of range.");

        return Options[userAnswerIndex].IsCorrect ? Score : 0;
    }
}
