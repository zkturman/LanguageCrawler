using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace LanguageCrawler
{
    class WordAccumulator
    {
        private string[] allWords;
        public string[] AllWords
        {
            get => allWords;
        }
        private Dictionary<string, int> wordCount;
        private string[] decreasingPopularity;
        public WordAccumulator(string text)
        {
            TextParser parser = new TextParser(text);
            allWords = parser.ParseTextWords();
        }

        private void parseText()
        {

        }

        public void GetWordCount()
        {
            wordCount = allWords.GroupBy(x => x.ToLower()).ToDictionary(g => g.Key, g => g.Count()).Where(x => (x.Value > 5) && (x.Key.All(char.IsLetter))).ToDictionary(x => x.Key, x => x.Value);
            selectionSort();
        }

        private void sortWordsByPopulariity()
        {

        }

        private void mergeSort()
        {

        }

        private void insertionSort()
        {

        }

        private void selectionSort()
        {
            decreasingPopularity = wordCount.Keys.ToArray();
            for (int i = 0; i < decreasingPopularity.Length; i++)
            {
                int max = findMax(decreasingPopularity, i);
                string tmp = decreasingPopularity[i];
                decreasingPopularity[i] = decreasingPopularity[max];
                decreasingPopularity[max] = tmp;
            }
        }

        private int findMax(string[] words, int startIndex)
        {
            int max = startIndex;
            for(int i = startIndex + 1; i < words.Length; i++)
            {
                if (wordCount[words[i]] > wordCount[words[max]])
                {
                    max = i;
                }
            }
            return max;
        }

        private void bubbleSort()
        {

        }

        private void quickSort()
        {
            
        }

        public void PrintWords()
        {
            for(int i = 0; i < decreasingPopularity.Length; i++)
            {
                string word = decreasingPopularity[i];
                Console.WriteLine(word + " - " + wordCount[word]);
            }
        }
    }
}
