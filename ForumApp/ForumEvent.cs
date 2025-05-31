namespace ForumApp
{
    public class ForumEvent
    {
        public ForumEventType EventType { get; }
        public string UserLogin { get; }
        public string QuestionId { get; }
        public string AnswerId { get; }
        public string QuestionAuthorLogin { get; }

        public ForumEvent(ForumEventType eventType, string userLogin, string questionId, string answerId = null, string questionAuthorLogin = null)
        {
            EventType = eventType;
            UserLogin = userLogin;
            QuestionId = questionId;
            AnswerId = answerId;
            QuestionAuthorLogin = questionAuthorLogin;
        }
    }
}