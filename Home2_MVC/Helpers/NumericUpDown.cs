using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Home2_MVC.Helpers
{
    //<input type = "number" min="0" max="5" value="1" />
    //<button data-id=@("tr" + Model.Id) data-price="@Model.Price" data-url="@Url.Action("AddToBucket", "Home")">Buy</button>
    public static class ListHelper
    {
        public static MvcHtmlString NumericUpDown(this HtmlHelper html, string min, string max, string defaultValue)
        {
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "number");
            input.MergeAttribute("min", min);
            input.MergeAttribute("max", max);
            input.MergeAttribute("value", defaultValue);
            return new MvcHtmlString(input.ToString());
        }
        public static MvcHtmlString BucketButton(this HtmlHelper html, string id, string price, string url, string text)
        {
            TagBuilder button = new TagBuilder("button");
            button.MergeAttribute("")
            return new MvcHtmlString(button.ToString());
        }
    }
}