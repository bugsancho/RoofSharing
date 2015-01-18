namespace RoofSharing.Web.Infrastructure.Extensions
{
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string imgUrl, string alt, object htmlAttributes, int? width = null, int? height = null)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var tag = new TagBuilder("Img");
            
            tag.Attributes.Add("src", imgUrl);
            tag.Attributes.Add("alt", alt);

            foreach (var attr in attrs)
            {
                tag.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            if (width != null)
            {
                tag.Attributes.Add("width", width.ToString());
            }

            if (height != null)
            {
                tag.Attributes.Add("height", height.ToString());
            }
            
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString FacebookImage(this HtmlHelper helper, string imgUrl, string alt, object htmlAttributes, int? width = null, int? height = null)
        {
            if (width != null && height != null)
            {
                imgUrl = imgUrl + "?width=" + width + "&height=" + height;
            }
            else if (width != null)
            {
                imgUrl = imgUrl + "?width=" + width;
            }
            else if (height != null)
            {
                imgUrl = imgUrl + "?height=" + height;
            }
            return Image(helper, imgUrl, alt, htmlAttributes, width, height);
        }
    }
}