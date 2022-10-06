using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.BookStore.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Disable { get; set; }
        public SelectListGroup Group { get; set; }
    }
}
