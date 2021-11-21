using System;
using System.Threading.Tasks;

namespace LanguageCrawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ContentRetriever google = new ContentRetriever("https://en.wikipedia.org/wiki/Nintendo");
            await google.GenerateContent();
            WordAccumulator wordCount = new WordAccumulator(google.innerText);
            Console.WriteLine("There are {0} words in the asyncstring.", wordCount.AllWords.Length);
            wordCount.GetWordCount();
            wordCount.PrintWords();
        }
    }
}
