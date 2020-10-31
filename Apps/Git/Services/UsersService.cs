using Git.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateUser(string username, string email, string password)
        {
            var User = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password),
            };

            this.db.Users.Add(User);
            this.db.SaveChanges();
            return User.Id;
        }

        public string GetUserId(string username, string password)
        {
            var User = this.db.Users.FirstOrDefault(x => x.Username == username && x.Password == ComputeHash(password));
            return User.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            var User = this.db.Users.FirstOrDefault(x => x.Email == email);
            if (User == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            var User = this.db.Users.FirstOrDefault(x => x.Username == username);
            if (User == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
