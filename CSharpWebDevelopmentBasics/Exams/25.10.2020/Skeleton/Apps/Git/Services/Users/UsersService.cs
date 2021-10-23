using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Git.Data;
using Git.Data.Models;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateUser(string username, string email, string password)
        {
            User user = new User()
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password)
            };

            db.Users.Add(user);
            db.SaveChanges();

            return user.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !db.Users.Any(user => user.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            string hashedPassword = ComputeHash(password);
            User user = db.Users.FirstOrDefault(user => user.Username == username && user.Password == hashedPassword);

            return user?.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !db.Users.Any(user => user.Username == username);
        }

        private string ComputeHash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
