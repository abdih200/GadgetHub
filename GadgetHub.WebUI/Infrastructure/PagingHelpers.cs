﻿using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Infrastructure
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("btn btn-primary active");
                }
                else
                {
                    tag.AddCssClass("btn btn-outline-primary");
                }

                result.Append(tag.ToString() + " ");
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
