namespace Ciezarki.MVVM.Model
{
    internal class User
    {
        private int _id;
        private string _username;
        private string _email;
        private string _password;
        private DateTime _createdAt;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public DateTime CreatedAt
        {
            get { return CreatedAt; }
            set { _createdAt = value; }
        }

        public User()
        {
            CreatedAt = DateTime.Now;
        }

        public User(int id, string username, string email, string password, DateTime createdAt)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"{Id}: {Username} ({Email}), Created at: {CreatedAt.ToShortTimeString}";
        }

        public ICollection<ProgressLog> ProgressLog { get; set; }
        public ICollection<UserWorkout> UserWorkout { get; set; }
    }
}
