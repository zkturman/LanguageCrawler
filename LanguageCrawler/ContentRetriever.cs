using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LanguageCrawler
{
    class ContentRetriever
    {
        private string url;
        private string siteBody;
        public string SiteBody
        {
            get => siteBody;
        }

        public string innerText;

        public ContentRetriever(string url)
        {
            this.url = url;
        }

        public async Task GenerateContent()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            siteBody = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(siteBody);
            innerText = htmlDocument.DocumentNode.InnerText;
        }
    }
}
