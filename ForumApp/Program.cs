using System;

namespace ForumApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Forum forum = new Forum();
            ForumStatistics stats = new ForumStatistics(forum);

            User user1 = new User("abc");
            User user2 = new User("xyz");
            User user3 = new User("pqr");

            forum.AddUser(user1);
            forum.AddUser(user2);
            forum.AddUser(user3);

            user1.SubscribeToAll();
            user2.SubscribeToOwnQuestions();
            user3.SubscribeToAll();

            Console.WriteLine("\nAdding questions and answers:");
            forum.AddQuestion("What is C#?", "abc");
            forum.AddAnswer("C# is a programming language.", "Q1", "xyz");
            forum.AddAnswer("It's developed by Microsoft.", "Q1", "pqr");
            forum.AddQuestion("What is .NET?", "xyz");
            forum.AddAnswer(".NET is a framework.", "Q2", "abc");

            Console.WriteLine("\nAll questions:");
            foreach (var question in forum.GetQuestions())
            {
                Console.WriteLine($"Question {question.QuestionId} by {question.AuthorLogin}: {question.Content}");
            }

            Console.WriteLine("\nQuestions by user abc:");
            foreach (var question in forum.GetQuestionsByUser("abc"))
            {
                Console.WriteLine($"Question {question.QuestionId}: {question.Content}");
            }

            Console.WriteLine("\nAnswers for question Q1:");
            foreach (var answer in forum.GetAnswersByQuestion("Q1"))
            {
                Console.WriteLine($"Answer {answer.AnswerId} by {answer.AuthorLogin}: {answer.Content}");
            }
        }
    }
}