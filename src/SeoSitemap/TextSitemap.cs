using System;
using System.Collections.Generic;
using System.Text;

namespace SeoSocial.Sitemap
{
    /// <summary>
    /// https://support.google.com/webmasters/answer/75712?hl=pt-BR&ref_topic=4581190
    /// </summary>
    public class TextSitemap
    {
        IList<Uri> list = new List<Uri>();

        public void Add(Uri uri)
        {
            list.Add(uri);
        }

        public void Add(string url)
        {
            list.Add(new Uri(url));
        }

        public string Generate()
        {
            return string.Join(Environment.NewLine, list);
        }
    }
}
