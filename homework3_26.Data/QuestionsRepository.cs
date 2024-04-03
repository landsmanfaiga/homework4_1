using Microsoft.EntityFrameworkCore;

namespace homework3_26.Data
{
    public class QuestionsRepository
    {
        private string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetQuestions()
        {
            using var ctx = new QuestionsDataContext(_connectionString);
            return ctx.Questions.Include(q => q.Answers).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).ToList();
        }

        public Question GetQuestion(int id)
        {
            using var ctx = new QuestionsDataContext(_connectionString);
            return ctx.Questions.Include(q => q.Answers).Include(q=> q.User).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).FirstOrDefault(q => q.Id == id);
        }
        
        public User GetByEmail(string email)
        {
            using var ctx = new QuestionsDataContext (_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isMatch = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isMatch)
            {
                return null;
            }

            return user;
        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            var ctx = new QuestionsDataContext(_connectionString);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public void AddQuestion(Question question, string tags)
        {
            using var ctx = new QuestionsDataContext(_connectionString);
            ctx.Questions.Add(question);
            ctx.SaveChanges();
            var tagsList = tags.Split(',');
            foreach (string tag in tagsList)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                ctx.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            ctx.SaveChanges();
        }

        private Tag GetTag(string name)
        {
            using var ctx = new QuestionsDataContext(_connectionString);
            return ctx.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var ctx = new QuestionsDataContext(_connectionString);
            var tag = new Tag { Name = name };
            ctx.Tags.Add(tag);
            ctx.SaveChanges();
            return tag.Id;
        }

        public void AddAnswer(Answer answer)
        {
            var ctx = new QuestionsDataContext(_connectionString);
            ctx.Answers.Add(answer);
            ctx.SaveChanges();

        }
    }
}