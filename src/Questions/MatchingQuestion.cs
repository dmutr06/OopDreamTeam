using System;
using System.Collections.Generic;

namespace OopDreamTeam
{
    public class MatchingQuestion : BaseQuestion
    {
        public class Pair
        {
            public string Left { get; }
            public string Right { get; }

            public Pair(string left, string right)
            {
                Left = left ?? throw new ArgumentNullException(nameof(left), "Left item cannot be null.");
                Right = right ?? throw new ArgumentNullException(nameof(right), "Right item cannot be null.");
            }
        }

        private readonly bool strictGrading;
        public List<Pair> Pairs { get; }

        public MatchingQuestion(string text, double score, List<Pair> pairs, bool strictGrading)
            : base(text, score)
        {
            if (pairs == null)
                throw new ArgumentNullException(nameof(pairs), "Pairs cannot be null.");

            if (pairs.Count < 2)
                throw new InvalidOperationException("There must be at least 2 pairs.");

            Pairs = pairs;
            this.strictGrading = strictGrading;
        }

        public override double CheckAnswer(object userAnswer)
        {
            if (userAnswer is not List<int> userAnswerIndices)
                throw new ArgumentException("Answer must be a list of integers representing indices of matched pairs.");

            if (userAnswerIndices.Count != Pairs.Count)
                throw new ArgumentException("The number of answers must match the number of pairs.");

            int correctPairs = 0;

            for (int i = 0; i < Pairs.Count; i++)
            {
                int rightIndex = userAnswerIndices[i];
                if (rightIndex < 0 || rightIndex >= Pairs.Count)
                    throw new ArgumentOutOfRangeException(nameof(userAnswer), "One or more indices are out of range.");

                if (Pairs[rightIndex].Right == Pairs[i].Right)
                    correctPairs++;
            }

            if (strictGrading)
            {
                return (correctPairs == Pairs.Count) ? Score : 0.0;
            }
            else
            {
                double scorePerPair = Score / Pairs.Count;
                return Math.Round(correctPairs * scorePerPair, 2);
            }
        }
    }
}
