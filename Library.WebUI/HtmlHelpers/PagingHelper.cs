using Library.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.HtmlHelpers
{
    public static class PagingHelper
    {
        /*public static MvcHtmlString PageLinks(
            this HtmlHelper helper,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            int start = Math.Max(pagingInfo.CurrentPage - 2, 1);
            int end = Math.Min(start + 5, pagingInfo.TotalPages);
            *//*TagBuilder taga = new TagBuilder("a");
            if(pagingInfo.CurrentPage > 1)
            {
                taga.Attributes.Add("href", pageUrl(pagingInfo.CurrentPage - 1));
                taga.SetInnerText("Previous");
                taga.AddCssClass("page-link");
            }
            else
            {
                taga.SetInnerText("Previous");
                taga.AddCssClass("disable");
            }
            TagBuilder tagli1 = new TagBuilder("li");
            tagli1.AddCssClass("page-link");
            tagli1.InnerHtml=taga.ToString();

            sb.Append(tagli1.ToString());*//*
            for(int i= start; i < end; ++i)
            {
                TagBuilder tagA = new TagBuilder("a");
                tagA.Attributes.Add("href", pageUrl(i));
                tagA.SetInnerText(i.ToString());
                tagA.AddCssClass("page-link");

                TagBuilder tagLi = new TagBuilder("li");
                if(pagingInfo.CurrentPage ==i)
                {
                    tagLi.AddCssClass("page-item active");
                }
                else
                {
                    tagLi.AddCssClass("page-item");
                }
                tagLi.InnerHtml=tagA.ToString();
                sb.Append(tagLi.ToString());
            }
            *//*TagBuilder taganext = new TagBuilder("a");
            if (pagingInfo.CurrentPage > 1)
            {
                taganext.Attributes.Add("href", pageUrl(pagingInfo.CurrentPage + 1));
                taganext.SetInnerText("Next");
                taganext.AddCssClass("page-link");
            }
            else
            {
                taganext.SetInnerText("Previous");
                taganext.AddCssClass("disable");
            }
            TagBuilder tagli2 = new TagBuilder("li");
            tagli2.AddCssClass("page-link");
            tagli2.InnerHtml = taganext.ToString();

            sb.Append(tagli2.ToString());*//*

            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.InnerHtml = sb.ToString();
            tagUl.AddCssClass("pagination");

            TagBuilder tagNav = new TagBuilder("nav");
            tagNav.InnerHtml = tagUl.ToString();

            return MvcHtmlString.Create(tagNav.ToString());
        }*/
        public static MvcHtmlString PageLinks(//mvchtmlstring o day de browser hieu la 1 the html nếu xài string thì nó chỉ hiểu là string
            this HtmlHelper helper,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();

            int start = Math.Max(pagingInfo.CurrentPage - 2, 1);
            int end = Math.Min(start + 5, pagingInfo.TotalPages);
            for (int i = start; i <= end; i++)
            {
                //<a href="ListProduct?page=1">1</a>
                TagBuilder tagA = new TagBuilder("a");
                tagA.SetInnerText(i.ToString());
                tagA.Attributes.Add("href", pageUrl(i));
                tagA.AddCssClass("page-link");

                TagBuilder tagLi = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                {
                    tagLi.AddCssClass("page-item active");
                }
                else
                {
                    tagLi.AddCssClass("page-item");
                }


                tagLi.InnerHtml = tagA.ToString();

                sb.Append(tagLi.ToString());
            }

            TagBuilder tagUl = new TagBuilder("ul");
            tagUl.InnerHtml = sb.ToString();
            tagUl.AddCssClass("pagination");

            TagBuilder tagNav = new TagBuilder("nav");
            tagNav.InnerHtml = tagUl.ToString();


            return MvcHtmlString.Create(tagNav.ToString());
        }
    }
}