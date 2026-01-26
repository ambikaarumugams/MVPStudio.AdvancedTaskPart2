namespace MarsAdvancedTask.Framework.Models
{
    public sealed class UsersData
    {
        public List<UserAccount> Users { get; set; } = new();

        public UserAccount Get(string key)
        {
            var user = Users.FirstOrDefault(x => x.Key == key);
            if (user == null) throw new Exception($"User key not found: {key}");
            return user;
        }
    }

    public sealed class UserAccount
    {
        public string Key { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
