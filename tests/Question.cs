using NUnit.Framework;
using System;
using System.Collections.Generic;
using OopDreamTeam;

namespace OopDreamTeam.Tests
{
    public class CheckboxAnswerEvaluatorTests
    {
        [Test]
        public void Evaluate_StrictGrading_CorrectAnswer_ReturnsMaxScore()
        {
            var evaluator = new CheckboxAnswerEvaluator(true);
            var correctAnswers = new HashSet<int> { 0, 1 };
            var userAnswers = new List<int> { 0, 1 };
            double maxScore = 10.0;

            double score = evaluator.Evaluate(correctAnswers, userAnswers, maxScore);

            Assert.AreEqual(maxScore, score);
        }

        [Test]
        public void Evaluate_StrictGrading_IncorrectAnswer_ReturnsZero()
        {
            var evaluator = new CheckboxAnswerEvaluator(true);
            var correctAnswers = new HashSet<int> { 0, 1 };
            var userAnswers = new List<int> { 0, 2 };
            double maxScore = 10.0;

            double score = evaluator.Evaluate(correctAnswers, userAnswers, maxScore);

            Assert.AreEqual(0, score);
        }

        [Test]
        public void Evaluate_LenientGrading_PartialCorrectAnswers_ReturnsPartialScore()
        {
            var evaluator = new CheckboxAnswerEvaluator(false);
            var correctAnswers = new HashSet<int> { 0, 1, 2 };
            var userAnswers = new List<int> { 0, 2 };
            double maxScore = 9.0;

            double score = evaluator.Evaluate(correctAnswers, userAnswers, maxScore);

            Assert.AreEqual(6.0, score);
        }

        [Test]
        public void Evaluate_LenientGrading_WithIncorrectAnswers_DeductsScore()
        {
            var evaluator = new CheckboxAnswerEvaluator(false);
            var correctAnswers = new HashSet<int> { 0, 1, 2 };
            var userAnswers = new List<int> { 0, 3 };
            double maxScore = 9.0;

            double score = evaluator.Evaluate(correctAnswers, userAnswers, maxScore);

            Assert.AreEqual(0, score);
        }
    }

    public class CheckboxQuestionTests
    {
        [Test]
        public void CheckAnswer_WithCorrectAnswer_ReturnsFullScore()
        {
            var question = new CheckboxQuestion("Sample question", 10.0, true);
            var option1 = question.AddOption(true);
            var option2 = question.AddOption(false);
            var option3 = question.AddOption(true);

            double score = question.CheckAnswer(new List<int> { option1, option3 });

            Assert.AreEqual(10.0, score);
        }

        [Test]
        public void CheckAnswer_NoCorrectAnswers_ThrowsInvalidOperationException()
        {
            var question = new CheckboxQuestion("Sample question", 10.0, true);
            question.AddOption(false);

            Assert.Throws<InvalidOperationException>(() => question.CheckAnswer(new List<int> { 0 }));
        }

        [Test]
        public void AddOption_AddsCorrectAndIncorrectOptions()
        {
            var question = new CheckboxQuestion("Sample question", 5.0, false);

            var correctOption = question.AddOption(true);
            var incorrectOption = question.AddOption(false);

            Assert.Contains(correctOption, question.GetOptionIndices());
            Assert.Contains(incorrectOption, question.GetOptionIndices());
        }

        [Test]
        public void CheckAnswer_IncorrectAnswer_ReturnsZeroScoreInStrictMode()
        {
            var question = new CheckboxQuestion("Sample question", 10.0, true);
            var option1 = question.AddOption(true);
            var option2 = question.AddOption(false);

            double score = question.CheckAnswer(new List<int> { option2 });

            Assert.AreEqual(0, score);
        }
    }
}
