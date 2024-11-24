using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam
{
    public class CheckboxQuestion : BaseQuestion
    {
        public class Option
        {
            public string Text { get; }
            public bool IsRight { get; }

            public Option(string text, bool isRight)
            {
                Text = text;
                IsRight = isRight;
            }
        }

        public List<Option> Options { get; }
        private readonly bool strictGrading;

        public CheckboxQuestion(string text, double score, List<Option> options, bool strictGrading)
            : base(text, score)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options), "Options cannot be null.");
            this.strictGrading = strictGrading;

            if (!options.Any(option => option.IsRight))
                throw new InvalidOperationException("At least one correct answer is required.");
        }

        public override double CheckAnswer(object userAnswer)
        {
            if (userAnswer is not List<bool> userAnswerList)
                throw new ArgumentException("Answer must be a List<bool> matching the options.");

            if (userAnswerList.Count != Options.Count)
                throw new ArgumentException("The number of answers provided does not match the number of options.");

            double pmax = Score;

            int nmax = Options.Count(option => option.IsRight);
            int kmax = Options.Count - nmax;

            int n = 0;
            int k = 0;

            for (int i = 0; i < Options.Count; i++)
            {
                if (userAnswerList[i])
                {
                    if (Options[i].IsRight)
                        n++;
                    else
                        k++;
                }
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
