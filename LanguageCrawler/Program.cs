using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace LanguageCrawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //ContentRetriever google = new ContentRetriever("https://en.wikipedia.org");
            //await google.GenerateContent();
            //WordAccumulator wordCount = new WordAccumulator(google.innerText);
            //Console.WriteLine("There are {0} words in the asyncstring.", wordCount.AllWords.Length);
            //wordCount.GetWordCount();
            //wordCount.PrintWords();
            UrlCrawler google = new UrlCrawler("https://en.wikipedia.org");
            try
            {
                await google.CrawlThroughUrls();
                google.PrintHttpErrors();
            }
            catch(HttpRequestException except)
            {
                Console.WriteLine(except.Message);
                Console.WriteLine(except.StackTrace);
            }
            catch (SystemException except)
            {
                Console.WriteLine(except.Message);
                Console.WriteLine(except.StackTrace);
            }
        }
    }
}
