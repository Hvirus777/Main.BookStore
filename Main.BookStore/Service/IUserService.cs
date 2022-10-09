namespace Main.BookStore.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool isAuthenticated();
    }
}