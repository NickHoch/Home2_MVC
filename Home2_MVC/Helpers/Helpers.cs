using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Home2_MVC.Helpers
{
    public static class Helpers
    {
        public static MvcHtmlString NumericUpDown(this HtmlHelper htmlHelper, string min, string max, string value)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("min", min);
            tag.MergeAttribute("max", max);
            tag.MergeAttribute("value", value);
            tag.AddCssClass("numericUpDown");
            return new MvcHtmlString(tag.ToString());
        }
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string id, string price, string url, string innerHtml)
        {
            TagBuilder tag = new TagBuilder("button");
            tag.MergeAttribute("data-id", id);
            tag.MergeAttribute("data-price", price);
            tag.MergeAttribute("data-url", url);
            tag.InnerHtml = innerHtml;
            return new MvcHtmlString(tag.ToString());
        }
    }
}