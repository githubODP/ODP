namespace Identidade.Domain.Interface
{
    public interface ILoggedInUsersService
    {
        void AddUser(string email);
        void RemoveUser(string email);
        List<string> GetLoggedInUsers();

    }
}
