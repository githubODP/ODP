using Identidade.Domain.Interface;


namespace Identidade.Infra.Data
{
    public class LoggedInUsersService : ILoggedInUsersService
    {
        private static readonly HashSet<string> _loggedInUsers = new();

        public void AddUser(string email)
        {
            lock (_loggedInUsers)
            {
                _loggedInUsers.Add(email);
            }
        }

        public void RemoveUser(string email)
        {
            lock (_loggedInUsers)
            {
                _loggedInUsers.Remove(email);
            }
        }

        public List<string> GetLoggedInUsers()
        {
            lock (_loggedInUsers)
            {
                return _loggedInUsers.ToList();
            }
        }
    }

}
