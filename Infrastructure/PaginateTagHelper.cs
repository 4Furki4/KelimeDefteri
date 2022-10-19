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
        public PagingInfo? PageModel { get; set; }
        public string? PageAction { get; set; }
        public bool IsPageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = string.Empty;
        public string PageClassNormal { get; set; } = string.Empty;
        public string PageClassSelected { get; set; } = string.Empty;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // tagHelper that helps to create buttons with anchor elements that redirect to paginated url like .../AllRecords/?recordPage=1
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new("div");
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new("a"); //Create anchor elements with amount of total page.
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { recordPage = i }); // Create url with PageAction and page number
                    if (IsPageClassesEnabled) // Configure classes if enabled
                    {
                        tag.Attributes["style"] = i != PageModel.CurrentPage ? "background-color:#620277; border-0:#8F00FF" : ""; // Custom background color for selected and normal buttons.
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
