using Main.BookStore.Data;
using Main.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.BookStore.Repository
{
    public class LanguageRepository
    {

        private readonly BookStoreContext _context = null;

        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetAllLanguage()
        {
            return await _context.language.Select(x => new LanguageModel()
            {
                Name = x.Name,
                Id = x.Id,
                Description = x.Description
            }).ToListAsync();



        }
    }
}
