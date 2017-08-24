using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using HtmlAgilityPack;

namespace HtmlElementStripper
{
    public class GallerBuilder
    {
        private static string _cssGalleryCode;

        private static string CssGalleryCode
        {
            get { return _cssGalleryCode ?? (_cssGalleryCode = LoadCssGalleryCode()); }
        }

        private static string LoadCssGalleryCode()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var file = Path.Combine(dir, "Resources\\GalleryCss.txt");
            return File.ReadAllText(file);
        }

        internal static HtmlNode BuildImageGalleryNode(IEnumerable<string> imageLinks)
        {
            var galleryImagesNode = HtmlNode.CreateNode("<header></header>");

            var imageNodes = galleryImagesNode.ChildNodes;
            foreach (var imgLink in imageLinks)
                imageNodes.Add(HtmlNode.CreateNode(string.Format(@"<div><img src=""{0}"" alt></div>", imgLink)));

            var galleryCssNode =  HtmlNode.CreateNode("<style>"+CssGalleryCode + "</style>");

            var res =  HtmlNode.CreateNode("<div></div>");
            res.ChildNodes.Add(galleryImagesNode);
            res.ChildNodes.Add(galleryCssNode);

            return res;
        }
    }
}