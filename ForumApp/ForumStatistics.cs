using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumApp
{
    public class ForumStatistics
    {
        public int TotalQuestions { get; private set; }
        public int TotalAnswers { get; private set; }
        public double AverageAnswersPerQuestion { get; private set; }
        public int QuestionsWithoutAnswers { get; private set; }
        public int QuestionsWithAnswers { get; private set; }

        private readonly Dictionary<string, int> answersPerQuestion;

        public ForumStatistics(Forum forum)
        {
            answersPerQuestion = new Dictionary<string, int>();
            TotalQuestions = 0;
            TotalAnswers = 0;
            AverageAnswersPerQuestion = 0;
            QuestionsWithoutAnswers = 0;
            QuestionsWithAnswers = 0;

            User statsUser = new User("ForumStatistics");
            statsUser.SubscribeToAll();
            forum.AddUser(statsUser);
            forum.ForumEvent += HandleForumEvent;
        }

        private void HandleForumEvent(ForumEvent forumEvent)
        {
            if (forumEvent.EventType == ForumEventType.QuestionAdded)
            {
                TotalQuestions++;
                QuestionsWithoutAnswers++;
                answersPerQuestion[forumEvent.QuestionId] = 0;
                UpdateAverageAnswersPerQuestion();
            }
            else if (forumEvent.EventType == ForumEventType.AnswerAdded)
            {
                TotalAnswers++;
                answersPerQuestion[forumEvent.QuestionId]++;
                if (answersPerQuestion[forumEvent.QuestionId] == 1)
                {
                    QuestionsWithoutAnswers--;
                    QuestionsWithAnswers++;
                }
                UpdateAverageAnswersPerQuestion();
            }

            Console.WriteLine($"Statistics updated: Questions={TotalQuestions}, Answers={TotalAnswers}, AvgAnswersPerQuestion={AverageAnswersPerQuestion:F2}, QuestionsWithoutAnswers={QuestionsWithoutAnswers}, QuestionsWithAnswers={QuestionsWithAnswers}");
        }

        private void UpdateAverageAnswersPerQuestion()
        {
            AverageAnswersPerQuestion = TotalQuestions > 0 ? (double)TotalAnswers / TotalQuestions : 0;
        }
    }
}