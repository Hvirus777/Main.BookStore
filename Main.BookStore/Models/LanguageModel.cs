using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.BookStore.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Disable { get; set; }
        public SelectListGroup Group { get; set; }
    }
}
