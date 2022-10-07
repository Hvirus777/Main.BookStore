using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Main.BookStore.Helper
{
    public class CustomEmailTagHelper : TagHelper
    {

        public string MyEmail { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "a";
            //output.Attributes.SetAttribute("href", "mailto:hvirus777@gmail.com");

            //Passing value from View helper dynamically 
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Content.SetContent("my-email");
        }
    }
}
