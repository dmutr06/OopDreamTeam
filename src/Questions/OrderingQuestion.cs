﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OopDreamTeam
{
    public class OrderingQuestion : BaseQuestion
    {
        public List<string> CorrectOrder { get; }
        public List<string> ShuffledOrder { get; }
        private readonly string gradingMode;

        public OrderingQuestion(string text, double score, List<string> correctOrder, string gradingMode)
            : base(text, score)
        {
            if (correctOrder == null)
                throw new ArgumentNullException(nameof(correctOrder), "Correct order cannot be null.");

            if (correctOrder.Count < 2)
                throw new InvalidOperationException("There must be at least 2 options to order.");

            if (gradingMode != "strict" && gradingMode != "not-strict" && gradingMode != "lenient")
                throw new ArgumentException("Invalid grading mode. Use 'very-strict', 'strict', or 'lenient'.");

            CorrectOrder = new List<string>(correctOrder);
            ShuffledOrder = CorrectOrder.OrderBy(_ => Guid.NewGuid()).ToList();
            this.gradingMode = gradingMode;
        }

        public override double CheckAnswer(object userAnswer)
        {
            if (userAnswer is not List<string> userOrder)
                throw new ArgumentException("Answer must be a list of strings representing the ordered options.");

            if (userOrder.Count != CorrectOrder.Count)
                throw new ArgumentException("The number of answers must match the number of options.");

            switch (gradingMode)
            {
                case "strict":
                    return CheckStrict(userOrder);

                case "not-strict":
                    return CheckNotStrict(userOrder);

                case "lenient":
                    return CheckLenient(userOrder);

                default:
                    throw new InvalidOperationException("Invalid grading mode.");
            }
        }

        private double CheckStrict(List<string> userOrder)
        {
            return userOrder.SequenceEqual(CorrectOrder) ? Score : 0.0;
        }

        private double CheckNotStrict(List<string> userOrder)
        {
            int correctCount = userOrder
                .Where((answer, index) => answer == CorrectOrder[index])
                .Count();

            double scorePerOption = Score / CorrectOrder.Count;
            return Math.Round(correctCount * scorePerOption, 2);
        }

        private double CheckLenient(List<string> userOrder)
        {
            int totalPairs = CorrectOrder.Count * (CorrectOrder.Count - 1) / 2;
            int correctPairs = 0;

            for (int i = 0; i < userOrder.Count; i++)
            {
                for (int j = i + 1; j < userOrder.Count; j++)
                {
                    string userBefore = userOrder[i];
                    string userAfter = userOrder[j];

                    int correctBeforeIndex = CorrectOrder.IndexOf(userBefore);
                    int correctAfterIndex = CorrectOrder.IndexOf(userAfter);

                    if (correctBeforeIndex < correctAfterIndex)
                        correctPairs++;
                }
            }

            double scorePerPair = Score / totalPairs;
            return Math.Round(correctPairs * scorePerPair, 2);
        }
    }
}