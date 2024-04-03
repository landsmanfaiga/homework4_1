using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework3_26.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Answer> Answers { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<Answer> Answers { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        

        public Question Question { get; set; }
        public User User { get; set; }
    }

    public class QuestionsTags
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }

}
