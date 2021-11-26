using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;

namespace LanguageCrawler
{
    class UrlCrawler
    {
        private string baseUrl;
        HashSet<string> urlSet;
        List<string> urlsToProcess;

        private int currentGeneration = 0;
        private const int maxGenerations = 2;

        private const int maxUrls = 2000;

        HttpClient baseClient;
        private UrlPackage package;

        public UrlCrawler(string url)
        {
            baseUrl = url;
            urlSet = new HashSet<string>();
            urlSet.Add(url);
            urlsToProcess = new List<string>();
            urlsToProcess.Add(url);
            package = new UrlPackage(url);   
        }

        public async Task CrawlThroughUrls()
        {
            using (baseClient = new HttpClient())
            {
                baseClient.BaseAddress = new Uri(baseUrl);
                //baseClient.Timeout = TimeSpan.FromSeconds(300);
                while (urlsToProcess.Count > 0 && currentGeneration < maxGenerations)
                {
                    await searchGenerationUrls();
                    currentGeneration++;
                }
            }
            Console.WriteLine("Found {0} links.", urlSet.Count);
            Console.WriteLine("Did we meet maximum? " + isMaxAttempts().ToString());
            package.AssociatedAddresses = urlSet.ToList();
        }

        private async Task searchGenerationUrls()
        {
            if (!isMaxAttempts())
            {
                int max = urlsToProcess.Count;

                Console.WriteLine("\nCurrent Generation: {0}, Urls to process: {1}", currentGeneration, max);
                var generationUrls = urlsToProcess.ToArray();
                urlsToProcess.Clear();
                await Task.WhenAll(generationUrls.Select(async i => await findAllPageLinks(i)));
            }
        }

        private async Task findAllPageLinks(string url)
        {
            HttpResponseMessage response = await baseClient.GetAsync(url);
            string siteBody = await response.Content.ReadAsStringAsync();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(siteBody);

            var links = htmlDocument.DocumentNode.Descendants("a");
            int i = 0;
            await Task.WhenAll(links.ToArray().Select(async i =>
            {
                if (!isMaxAttempts())
                {
                    await tryAddUrl(i.GetAttributeValue("href", "broken link"));
                }
            }));

            Console.WriteLine("\n***{0} --> {1}",url, urlSet.Count);
        }

        private async Task tryAddUrl(string urlToTry)
        {
            urlToTry = removePrefixedSlash(urlToTry);
            if (canTryAddress(urlToTry))
            {
                try
                {
                    HttpResponseMessage response = await baseClient.GetAsync(urlToTry);
                    if (response.IsSuccessStatusCode)
                    {
                        urlsToProcess.Add(urlToTry);
                        urlSet.Add(urlToTry);
                        Console.Write(".");
                    }
                }
                catch (HttpRequestException hre)
                {
                    Console.WriteLine("\n{0} threw an error: {1}.", urlToTry, hre.Message);
                    Console.WriteLine("\n{0}.", hre.InnerException.Message);
                }
                catch (OperationCanceledException oce)
                {
                    Console.WriteLine("\n{0} threw an error: {1}.", urlToTry, oce.Message);
                }
            }
        }

        private bool isMaxAttempts()
        {
            return urlSet.Count > maxUrls;
        }

        private bool canTryAddress(string address)
        {
            bool check = !isMaxAttempts() && address != "broken link";
            return check && !urlSet.Contains(address);
        }

        private string removePrefixedSlash(string url)
        {
            if (url[0] == '/')
            {
                StringBuilder stringBuilder = new StringBuilder(url);
                stringBuilder.Remove(0, 1);
                url = stringBuilder.ToString();
            }
            return url;
        }

        public UrlPackage GetPackage()
        {
            return package;
        }
    }
}
