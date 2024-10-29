using NUnit.Framework;
using System;
using System.Collections.Generic;
using OopDreamTeam;

namespace OopDreamTeam.Tests
{
    public class CheckboxQuestionTests
    {

        [Test]
        public void Constructor_ShouldThrowException_WhenNoCorrectAnswer()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", false),
                new CheckboxQuestion.Option("Option2", false)
            };
            Assert.Throws<InvalidOperationException>(() => new CheckboxQuestion("Sample Question", 10, options, true));
        }

        [Test]
        public void CheckAnswer_ShouldReturnFullScore_WhenAllCorrectAnswersSelected_StrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", true),
                new CheckboxQuestion.Option("Option3", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, true);
            var userAnswer = new List<bool> { true, true, false };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(10, score);
        }

        [Test]
        public void CheckAnswer_ShouldReturnZero_WhenAllSelectedAnswerIncorrect_StrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", true)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, true);
            var userAnswer = new List<bool> { false, false, false };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(0, score);
        }

        [Test]
        public void CheckAnswer_ShouldReturnPartialScore_WhenPartialCorrectAnswersSelected_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", false),
                new CheckboxQuestion.Option("Option4", true),
                new CheckboxQuestion.Option("Option5", false),
                new CheckboxQuestion.Option("Option6", true),
                new CheckboxQuestion.Option("Option7", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            var userAnswer = new List<bool> { true, false, false, true, false, false, true };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(4.16, score, 0.01);
        }

        [Test]
        public void CheckAnswer_ShouldThrowException_WhenAnswerCountDoesNotMatchOptionsCount()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, true);
            var userAnswer = new List<bool> { true };

            Assert.Throws<ArgumentException>(() => question.CheckAnswer(userAnswer));
        }


        [Test]
        public void Constructor_ShouldThrowException_WhenAllOptionsAreIncorrect()
        {
            var options = new List<CheckboxQuestion.Option>
    {
        new CheckboxQuestion.Option("Option1", false),
        new CheckboxQuestion.Option("Option2", false)
    };

            Assert.Throws<InvalidOperationException>(() =>
                new CheckboxQuestion("Sample Question", 10, options, true));
        }

        [Test]
        public void CheckAnswer_ShouldReturnCorrectScore_WhenAllOptionsAreCorrect_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", true),
                new CheckboxQuestion.Option("Option3", true)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            var userAnswer = new List<bool> { false, true, true };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(6.66, score, 0.01);
        }

        [Test]
        public void CheckAnswer_ShouldReturnZero_WhenAllOptionsAreSelectedFalse_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", true)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            var userAnswer = new List<bool> { false, false };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(0, score);
        }

        [Test]
        public void CheckAnswer_ShouldReturnCorrectScore_WhenMixedAnswers_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", true),
                new CheckboxQuestion.Option("Option4", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            var userAnswer = new List<bool> { true, true, true, false };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(5, score);
        }

        [Test]
        public void CheckAnswer_ShouldReturnCorrectScore_WhenAllCorrectAnswersSelected_ButAlsoSelectedIncorrectAnswers_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", false),
                new CheckboxQuestion.Option("Option4", true)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            var userAnswer = new List<bool> { true, true, false, true };

            double score = question.CheckAnswer(userAnswer);

            Assert.AreEqual(5, score);
        }
    }
}
