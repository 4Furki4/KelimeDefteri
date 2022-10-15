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
        public PagingInfo? PageModel { get; set; }
        public string? PageAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder tag = new("div");
            
        }
    }
}
