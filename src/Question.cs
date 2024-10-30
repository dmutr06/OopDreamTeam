using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam
{
    public abstract class BaseQuestion
    {
        public string Text { get; set; }
        public double Score { get; set; }

        protected BaseQuestion(string text, double score)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text), "Question text cannot be null.");
            Score = score;
        }

        public abstract double CheckAnswer(List<int> userAnswerIndices);
    }
}