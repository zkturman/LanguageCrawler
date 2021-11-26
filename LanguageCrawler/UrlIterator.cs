using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageCrawler
{
    class UrlIterator
    {
        private UrlPackage package;
        private string baseUrl;
        private const int maxIterations = 2000;
        public UrlIterator(string baseUrl)
        {
            package = new UrlPackage();
            this.baseUrl = baseUrl;
        }

        public async Task IterateUrls()
        {
            for(int i = 0; i < maxIterations; i++)
            {
                string newUrl = makeUrl(i);
                await tryUrl(newUrl);
            }
        }

        private string makeUrl(int i)
        {
            return "";
        }

        private async Task tryUrl(string urlToTry)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.GetAsync(new Uri(urlToTry));
            }
        }
    }
}
