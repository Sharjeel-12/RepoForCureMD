namespace Patient_Visit_Manager_App.Services
{
    public interface ILoginServiceInterface
    {
        Task<string> createAccount(string username, string password);
        Task<bool> SignIntoAccount(string username, string password);
    }
}
