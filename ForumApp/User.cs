namespace ForumApp
{
    public class User
    {
        public string Login { get; }
        public bool SubscribedToAll { get; private set; }
        public bool SubscribedToOwnQuestions { get; private set; }

        public User(string login)
        {
            Login = login;
        }

        public void SubscribeToAll()
        {
            SubscribedToAll = true;
        }

        public void UnsubscribeFromAll()
        {
            SubscribedToAll = false;
        }

        public void SubscribeToOwnQuestions()
        {
            SubscribedToOwnQuestions = true;
        }

        public void UnsubscribeFromOwnQuestions()
        {
            SubscribedToOwnQuestions = false;
        }

        public void HandleForumEvent(ForumEvent forumEvent)
        {
            if (forumEvent.EventType == ForumEventType.QuestionAdded)
            {
                if (SubscribedToAll)
                {
                    Console.WriteLine($"User {Login} notified: User {forumEvent.UserLogin} added question {forumEvent.QuestionId}");
                }
            }
            else if (forumEvent.EventType == ForumEventType.AnswerAdded)
            {
                if (SubscribedToAll)
                {
                    Console.WriteLine($"User {Login} notified: User {forumEvent.UserLogin} added answer {forumEvent.AnswerId} to question {forumEvent.QuestionId} by {forumEvent.QuestionAuthorLogin}");
                }
                if (SubscribedToOwnQuestions && Login == forumEvent.QuestionAuthorLogin)
                {
                    Console.WriteLine($"User {Login} notified: User {forumEvent.UserLogin} added answer {forumEvent.AnswerId} to your question {forumEvent.QuestionId}");
                }
            }
        }
    }
}