﻿using System;

namespace OopDreamTeam
{
    public class InputTextQuestion : BaseQuestion
    {
        public string CorrectAnswer { get; }

        public InputTextQuestion(string text, double score, string correctAnswer)
            : base(text, score)
        {
            CorrectAnswer = correctAnswer ?? throw new ArgumentNullException(nameof(correctAnswer), "Correct answer cannot be null.");
        }

        public override double CheckAnswer(object userAnswer)
        {
            if (userAnswer is not string userAnswerText)
                throw new ArgumentException("Answer must be a string.");

            return string.Equals(userAnswerText, CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? Score : 0;
        }
    }
}