namespace ProjectAPI.Models
{
    public class UserAccounts
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        private UserAccounts() { }
        public static UserAccounts Create(string username, string password, bool isAdmin = false)
        {
            return new UserAccounts
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                IsAdmin = isAdmin
            };
        }
        public bool VerifyPassword(string pwd) => BCrypt.Net.BCrypt.EnhancedVerify(pwd, this.Password);
    }
}