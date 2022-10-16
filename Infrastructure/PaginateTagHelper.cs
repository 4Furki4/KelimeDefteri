using KelimeDefteri.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace KelimeDefteri.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginateTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string? PageAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // tagHelper that helps to create buttons with anchor elements that redirect to paginated url like .../AllRecords/?recordPage=1

            TagBuilder tag = new("div");

            for (int i = 0; i < PageModel.TotalPages; i++)
            {
                TagBuilder tagBuilder = new("a");
                tagBuilder.Attributes["asp-action"] = PageAction;
            }
            
        }
    }
}
