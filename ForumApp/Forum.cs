using System;
using System.Collections.Generic;
using System.Linq;

namespace ForumApp
{
    public class Forum
    {
        private readonly List<(string QuestionId, string Content, string AuthorLogin)> questions;
        private readonly List<(string AnswerId, string Content, string QuestionId, string AuthorLogin)> answers;
        private readonly List<User> subscribers;
        private int questionCounter;
        private int answerCounter;

        public event Action<ForumEvent> ForumEvent;

        public Forum()
        {
            questions = new List<(string, string, string)>();
            answers = new List<(string, string, string, string)>();
            subscribers = new List<User>();
            questionCounter = 0;
            answerCounter = 0;
        }

        public void AddUser(User user)
        {
            if (!subscribers.Contains(user))
            {
                subscribers.Add(user);
            }
        }

        public void AddQuestion(string content, string authorLogin)
        {
            string questionId = $"Q{++questionCounter}";
            questions.Add((questionId, content, authorLogin));
            Notify(new ForumEvent(ForumEventType.QuestionAdded, authorLogin, questionId));
        }

        public void AddAnswer(string content, string questionId, string authorLogin)
        {
            if (!questions.Any(q => q.QuestionId == questionId))
            {
                throw new ArgumentException($"Question {questionId} does not exist.");
            }

            string answerId = $"A{++answerCounter}";
            string questionAuthorLogin = questions.First(q => q.QuestionId == questionId).AuthorLogin;
            answers.Add((answerId, content, questionId, authorLogin));
            Notify(new ForumEvent(ForumEventType.AnswerAdded, authorLogin, questionId, answerId, questionAuthorLogin));
        }

        private void Notify(ForumEvent forumEvent)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.HandleForumEvent(forumEvent);
            }
        }

        public List<(string QuestionId, string Content, string AuthorLogin)> GetQuestions()
        {
            return questions.ToList();
        }

        public List<(string QuestionId, string Content, string AuthorLogin)> GetQuestionsByUser(string userLogin)
        {
            return questions.Where(q => q.AuthorLogin == userLogin).ToList();
        }

        public List<(string AnswerId, string Content, string QuestionId, string AuthorLogin)> GetAnswersByQuestion(string questionId)
        {
            return answers.Where(a => a.QuestionId == questionId).ToList();
        }

        public List<(string AnswerId, string Content, string QuestionId, string AuthorLogin)> GetAnswers()
        {
            return answers.ToList();
        }
    }
}