using NUnit.Framework;
using System;
using System.Collections.Generic;
using OopDreamTeam;

namespace OopDreamTeam.Tests
{
    public class QuestionTests
    {
        [Test]
        public void InputTextQuestion_Correct()
        {
            var question = new InputTextQuestion("Sample Question", 10, "Correct Answer");
            question.UserAnswer = "cORRect ansWeR";

            double score = question.CheckAnswer();

            Assert.AreEqual(10, score);
        }

        [Test]
        public void InputTextQuestion_Incorrect()
        {
            var question = new InputTextQuestion("Sample Question", 10, "Correct Answer");
            question.UserAnswer = "Wrong Answer";

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void SingleChoiceQuestion_Correct()
        {
            var options = new List<SingleChoiceQuestion.Option>
            {
                new SingleChoiceQuestion.Option("Option1", false),
                new SingleChoiceQuestion.Option("Option2", true),
                new SingleChoiceQuestion.Option("Option3", false)
            };
            var question = new SingleChoiceQuestion("Sample Question", 5, options);
            question.UserAnswer = 1;

            double score = question.CheckAnswer();

            Assert.AreEqual(5, score);
        }

        [Test]
        public void SingleChoiceQuestion_Incorrect()
        {
            var options = new List<SingleChoiceQuestion.Option>
            {
                new SingleChoiceQuestion.Option("Option1", false),
                new SingleChoiceQuestion.Option("Option2", true),
                new SingleChoiceQuestion.Option("Option3", false)
            };
            var question = new SingleChoiceQuestion("Sample Question", 5, options);
            question.UserAnswer = 0;

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void CheckboxQuestion_AllCorrect_StrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", true),
                new CheckboxQuestion.Option("Option3", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, true);
            question.UserAnswer = new List<bool> { true, true, false };

            double score = question.CheckAnswer();

            Assert.AreEqual(10, score);
        }

        [Test]
        public void CheckboxQuestion_OneIncorrect_StrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", true),
                new CheckboxQuestion.Option("Option3", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, true);
            question.UserAnswer = new List<bool> { false, true, false };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void CheckboxQuestion_PartialCorrect_NotStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", false),
                new CheckboxQuestion.Option("Option4", true),
                new CheckboxQuestion.Option("Option5", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            question.UserAnswer = new List<bool> { true, false, true, true, true };

            double score = question.CheckAnswer();

            Assert.AreEqual(3.33, score, 0.01);
        }

        [Test]
        public void CheckboxQuestion_BalancedButOnlyTrue_NonStrictGrading()
        {
            var options = new List<CheckboxQuestion.Option>
            {
                new CheckboxQuestion.Option("Option1", true),
                new CheckboxQuestion.Option("Option2", false),
                new CheckboxQuestion.Option("Option3", false),
                new CheckboxQuestion.Option("Option4", true),
                new CheckboxQuestion.Option("Option5", false)
            };
            var question = new CheckboxQuestion("Sample Question", 10, options, false);
            question.UserAnswer = new List<bool> { true, true, true, true, true };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void MatchingQuestion_AllCorrect_StrictGrading()
        {
            var pairs = new List<MatchingQuestion.Pair>
            {
                new MatchingQuestion.Pair("Left1", "Right1"),
                new MatchingQuestion.Pair("Left2", "Right2"),
                new MatchingQuestion.Pair("Left3", "Right3")
            };
            var question = new MatchingQuestion("Sample Question", 10, pairs, true);
            question.UserAnswer = new List<int> { 0, 1, 2 };

            double score = question.CheckAnswer();

            Assert.AreEqual(10, score);
        }

        [Test]
        public void MatchingQuestion_OnlyOneCorrect_StrictGrading()
        {
            var pairs = new List<MatchingQuestion.Pair>
            {
                new MatchingQuestion.Pair("Left1", "Right1"),
                new MatchingQuestion.Pair("Left2", "Right2"),
                new MatchingQuestion.Pair("Left3", "Right3")
            };
            var question = new MatchingQuestion("Sample Question", 10, pairs, true);
            question.UserAnswer = new List<int> { 0, 2, 1 };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void MatchingQuestion_PartialCorrect_NotStrictGrading()
        {
            var pairs = new List<MatchingQuestion.Pair>
            {
                new MatchingQuestion.Pair("Left1", "Right1"),
                new MatchingQuestion.Pair("Left2", "Right2"),
                new MatchingQuestion.Pair("Left3", "Right3"),
                new MatchingQuestion.Pair("Left4", "Right4"),
                new MatchingQuestion.Pair("Left5", "Right5"),
                new MatchingQuestion.Pair("Left6", "Right6")
            };
            var question = new MatchingQuestion("Sample Question", 10, pairs, false);
            question.UserAnswer = new List<int> { 5, 1, 2, 3, 4, 0 };

            double score = question.CheckAnswer();

            Assert.AreEqual(6.67, score, 0.01);
        }

        [Test]
        public void MatchingQuestion_AllIncorrect_NotStrictGrading()
        {
            var pairs = new List<MatchingQuestion.Pair>
            {
                new MatchingQuestion.Pair("Left1", "Right1"),
                new MatchingQuestion.Pair("Left2", "Right2"),
                new MatchingQuestion.Pair("Left3", "Right3"),
                new MatchingQuestion.Pair("Left4", "Right4"),
                new MatchingQuestion.Pair("Left5", "Right5"),
                new MatchingQuestion.Pair("Left6", "Right6")
            };
            var question = new MatchingQuestion("Sample Question", 10, pairs, false);
            question.UserAnswer = new List<int> { 5, 4, 3, 2, 1, 0 };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void OrderingQuestion_AllCorrect_StrictGrading()
        {
            var correctOrder = new List<string> { "Option1", "Option2", "Option3" };
            var question = new OrderingQuestion("Sample Question", 10, correctOrder, OrderingQuestion.GradingMode.Strict);
            question.UserAnswer = new List<string> { "Option1", "Option2", "Option3" };

            double score = question.CheckAnswer();

            Assert.AreEqual(10, score);
        }

        [Test]
        public void OrderingQuestion_OnlyOneCorrect_StrictGrading()
        {
            var correctOrder = new List<string> { "Option1", "Option2", "Option3" };
            var question = new OrderingQuestion("Sample Question", 10, correctOrder, OrderingQuestion.GradingMode.Strict);
            question.UserAnswer = new List<string> { "Option1", "Option3", "Option2" };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }

        [Test]
        public void OrderingQuestion_PartialCorrect_NotStrictGrading()
        {
            var correctOrder = new List<string> { "Option1", "Option2", "Option3", "Option4", "Option5", "Option6" };
            var question = new OrderingQuestion("Sample Question", 10, correctOrder, OrderingQuestion.GradingMode.NotStrict);
            question.UserAnswer = new List<string> { "Option6", "Option2", "Option1", "Option3", "Option4", "Option5" };

            double score = question.CheckAnswer();

            Assert.AreEqual(1.67, score, 0.01);
        }

        [Test]
        public void OrderingQuestion_SameAnswerPartialCorrect_LenientGrading()
        {
            var correctOrder = new List<string> { "Option1", "Option2", "Option3", "Option4", "Option5", "Option6" };
            var question = new OrderingQuestion("Sample Question", 10, correctOrder, OrderingQuestion.GradingMode.Lenient);
            question.UserAnswer = new List<string> { "Option6", "Option2", "Option1", "Option3", "Option4", "Option5" };

            double score = question.CheckAnswer();

            Assert.AreEqual(6, score);
        }

        [Test]
        public void OrderingQuestion_AllIncorrect_LenientGrading()
        {
            var correctOrder = new List<string> { "Option1", "Option2", "Option3", "Option4", "Option5", "Option6" };
            var question = new OrderingQuestion("Sample Question", 10, correctOrder, OrderingQuestion.GradingMode.Lenient);
            question.UserAnswer = new List<string> { "Option6", "Option5", "Option4", "Option3", "Option2", "Option1" };

            double score = question.CheckAnswer();

            Assert.AreEqual(0, score);
        }
    }
}
