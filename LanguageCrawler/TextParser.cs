using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LanguageCrawler
{
    public class TextParser
    {
        private string text;
        private AppLanguage language;
        public TextParser(string text)
        {
            this.text = Regex.Replace(text, "\\s+", " ");
            language = AppLanguage.English;
        }

        public string[] ParseTextWords()
        {
            return text.Split(" ");
        }

        public string[] ParseTextSentences()
        {
            return null;
        }

        public string[] ParseTextParagraphs()
        {
            return text.Split("\n");
        }

        private void determineLanguage()
        {

        }
    }
}
