using Main.BookStore.Models;
using System.Threading.Tasks;

namespace Main.BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}