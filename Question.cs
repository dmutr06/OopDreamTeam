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

    public class CheckboxAnswerEvaluator
    {
        private readonly bool strictGrading;

        public CheckboxAnswerEvaluator(bool strictGrading)
        {
            this.strictGrading = strictGrading;
        }

        public double Evaluate(HashSet<int> correctAnswerIndices, List<int> userAnswerIndices, double maxScore)
        {
            int totalCorrect = correctAnswerIndices.Count;
            int selectedCorrect = userAnswerIndices.Count(userIndex => correctAnswerIndices.Contains(userIndex));
            int selectedIncorrect = userAnswerIndices.Count(userIndex => !correctAnswerIndices.Contains(userIndex));

            if (strictGrading)
            {
                return (selectedCorrect == totalCorrect && selectedIncorrect == 0) ? maxScore : 0;
            }
            else
            {
                double scorePerCorrect = maxScore / totalCorrect;
                double scorePerIncorrect = maxScore / totalCorrect;

                double score = selectedCorrect * scorePerCorrect - selectedIncorrect * scorePerIncorrect;
                return Math.Max(0, Math.Round(score, 2));
            }
        }
    }

    public class CheckboxQuestion : BaseQuestion
    {
        private HashSet<int> correctAnswerIndices;
        private CheckboxAnswerEvaluator evaluator;
        private int nextOptionIndex = 0;
        private bool hasCorrectAnswer = false;

        public CheckboxQuestion(string text, double score, bool strictGrading)
            : base(text, score)
        {
            correctAnswerIndices = new HashSet<int>();
            evaluator = new CheckboxAnswerEvaluator(strictGrading);
        }

        public int AddOption(bool isCorrect)
        {
            int index = nextOptionIndex++;

            if (isCorrect)
            {
                correctAnswerIndices.Add(index);
                hasCorrectAnswer = true;
            }

            return index;
        }

        public override double CheckAnswer(List<int> userAnswerIndices)
        {
            if (!hasCorrectAnswer)
            {
                throw new InvalidOperationException("Cannot evaluate answer: question has no correct answers.");
            }

            return evaluator.Evaluate(correctAnswerIndices, userAnswerIndices, Score);
        }

        public List<int> GetOptionIndices()
        {
            return Enumerable.Range(0, nextOptionIndex).ToList();
        }
    }
}