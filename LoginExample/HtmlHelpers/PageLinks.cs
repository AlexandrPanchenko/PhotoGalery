using LoginExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoginExample.HtmlHelpers
{
    public static class PageLinks
    {
        public static MvcHtmlString PageLink(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageURL)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageURL(i));
                tag.InnerHtml = i.ToString();
                if (pagingInfo.CurrentPage == i)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");

                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());

            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
