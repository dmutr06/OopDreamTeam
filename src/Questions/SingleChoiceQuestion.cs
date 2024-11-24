using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam
{
    public class SingleChoiceQuestion : BaseQuestion
    {
        public class Option
        {
            public string Text { get; }
            public bool IsCorrect { get; }
            
            public Option(string text, bool isCorrect)
            {
                Text = text;
                IsCorrect = isCorrect;
            }
        }

        public List<Option> Options { get; }
        private const int MaxOptions = 26;

        public SingleChoiceQuestion(string text, double score, List<Option> options)
            : base(text, score)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options), "Options cannot be null.");

            if (Options.Count > MaxOptions)
                throw new InvalidOperationException($"Number of options cannot exceed {MaxOptions}.");

            if (Options.Count(option => option.IsCorrect) != 1)
                throw new InvalidOperationException("There must be exactly one correct answer.");
        }

        public override double CheckAnswer(object userAnswer)
        {
            if (userAnswer is not int userAnswerIndex)
                throw new ArgumentException("Answer must be an integer representing the option index.");

            if (userAnswerIndex < 0 || userAnswerIndex >= Options.Count)
                throw new ArgumentOutOfRangeException(nameof(userAnswer), "Selected option is out of range.");

            return Options[userAnswerIndex].IsCorrect ? Score : 0;
        }
    }
}
