using Main.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Main.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetAllLanguage();
    }
}