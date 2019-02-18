using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace SeoSocial.Sitemap
{
    class SiteMap
    {
        public Uri Url { get; set; }
        public DateTime? LastModified { get; set; }
    }

    /// <summary>
    /// https://www.siteground.com/tutorials/sitemap/index/
    /// </summary>
    public class IndexSitemap
    {
        List<SiteMap> list = new List<SiteMap>();

        public void AddUrl(string url)
        {
            this.list.Add(new SiteMap
            {
                Url = new Uri(url)
            });
        }

        public void AddUrl(string url, DateTime lastModified)
        {
            this.list.Add(new SiteMap
            {
                Url = new Uri(url),
                LastModified = lastModified
            });
        }

        public string Generate()
        {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(ns + "sitemapindex", new XAttribute("xmlns", ns.NamespaceName))
            );

            foreach (var item in list)
            {
                var sm = new XElement(ns + "sitemap");
                sm.Add(new XElement(ns + "loc", item.Url));

                if (item.LastModified.HasValue)
                    sm.Add(new XElement(ns + "lastmod", item.LastModified.ToString()));

                doc.Root.Add(sm);
            }

            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriterUtf8(builder))
            {
                doc.Save(writer);
            }

            return builder.ToString();
        }
    }
}
