using Microsoft.AspNetCore.Razor.TagHelpers;




namespace Main.BookStore.Helper
{

    //this will work using 'AND' functionality. eg. Target tags whos tag AND attribute is 'big'
    // [HtmlTargetElement("big", Attributes = "big")]

    //this will work using 'OR' functionality. ForEg. Target tags whos tag is big OR target tags whos attribute is 'big'
    [HtmlTargetElement("big")]
    [HtmlTargetElement(Attributes = "big")]
    public class BigTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h3";
            output.Attributes.RemoveAll("big"); 
        }
    }
}
