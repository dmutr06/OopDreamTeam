using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam
{
    public class CheckboxQuestion
    {
        public class Option
        {
            public readonly string Text;
            public readonly bool IsRight;

            public Option(string text, bool isRight)
            {
                Text = text;
                IsRight = isRight;
            }
        }

        public readonly string Text;
        public readonly double Score;
        public readonly List<Option> Options;
        private readonly bool strictGrading;

        public CheckboxQuestion(string text, double score, List<Option> options, bool strictGrading)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text), "Question text cannot be null.");
            Score = score;
            Options = options ?? throw new ArgumentNullException(nameof(options), "Options cannot be null.");
            this.strictGrading = strictGrading;

            if (!options.Any(option => option.IsRight))
                throw new InvalidOperationException("At least one correct answer is required.");
        }

        public double CheckAnswer(List<bool> userAnswer)
        {
            if (userAnswer.Count != Options.Count)
                throw new ArgumentException("The number of answers provided does not match the number of options.");

            double pmax = Score;

            int nmax = 0;
            int kmax = 0;
            int n = 0;
            int k = 0;

            for (int i = 0; i < Options.Count; i++)
            {
                if (Options[i].IsRight) nmax++;
                else kmax++;

                if (userAnswer[i] && Options[i].IsRight) n++;
                else if (userAnswer[i] && !Options[i].IsRight) k++;
            }

            if (strictGrading)
            {
                return (n == nmax && k == 0) ? pmax : 0;
            }
            else
            {
                double p = n * (pmax / nmax);
                if (kmax > 0)
                {
                    p -= k * (pmax / kmax);
                }

                return Math.Max(0, Math.Round(p, 2));
            }
        }
    }
}
