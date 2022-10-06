using System.ComponentModel.DataAnnotations;

namespace Main.BookStore.Enums
{
    public enum LanguageEnum
    {

        // 'Text' = 'Value'  

        [Display(Name ="Hindi Language")]
        Hindi = 10,

        [Display(Name = "English Language")]
        English = 20,
        
        [Display(Name = "Gujarati Language")]
        Gujarati = 30,
        
        [Display(Name = "Tamil Language")]
        Tamil = 40,
        
        [Display(Name = "Dutch Language")]
        Dutch = 50,
        
        [Display(Name = "Urdu Language")]
        Urdu = 60
    }
}
