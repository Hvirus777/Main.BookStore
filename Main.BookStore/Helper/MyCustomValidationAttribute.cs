using System.ComponentModel.DataAnnotations;

namespace Main.BookStore.Helper
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {
        public string Text { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                string bookValue = value.ToString();

                //if (bookValue.Contains("mvc"))
                if (bookValue.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "BookName doest not contain the desired filed valeue.");
        }
    }
}
