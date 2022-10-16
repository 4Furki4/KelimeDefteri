using KelimeDefteri.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace KelimeDefteri.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginateTagHelper : TagHelper
    {
        public IUrlHelperFactory urlHelperFactory;
        public PaginateTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string? PageAction { get; set; }
       
    public int MyProperty { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // tagHelper that helps to create buttons with anchor elements that redirect to paginated url like .../AllRecords/?recordPage=1
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new("div");
                for (int i = 0; i < PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new("a"); //Create anchor elements with amount of total page.
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { recordPage = i }); // Create url with PageAction and page number
                    
                    tag.InnerHtml.Append(i.ToString()); 
                    result.InnerHtml.AppendHtml(tag);
                    
                }
                output.Content.AppendHtml(result.InnerHtml);
            }
            
            
        }
    }
}
