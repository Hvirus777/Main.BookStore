using Main.BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Main.BookStore.Repository
{
    public interface IAccountRepository
    { 
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
    }
}