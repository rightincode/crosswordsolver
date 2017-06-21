using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace crosswordsolver
{
    public class WordDictionary
    {
        private Dictionary<int, string> _wordDictionary;
        private long _elapsedTicks;
        
        public Dictionary<int, string> wordDictionary
        {
            get
            {
                return _wordDictionary;
            }
        }

        public long elapsedTicks
        {
            get { return _elapsedTicks; }
        }

        public WordDictionary()
        {            
            LoadDictionary();            
        }

        public List<string> FindMatch(int length, string inputString)
        {
            List<string> matchedWordsList = new List<string>();
            Stopwatch stopwatch = new Stopwatch();

            Regex searchExp = new Regex(BuildRegexPattern(inputString, length));

            stopwatch.Reset();
            stopwatch.Start();

            var matchedWordsByLength = _wordDictionary[length];
            MatchCollection matchedWords = searchExp.Matches(matchedWordsByLength);

            stopwatch.Stop();
            _elapsedTicks = stopwatch.ElapsedTicks;
            
            foreach (Match match in matchedWords)
            {
                matchedWordsList.Add(match.Value);
            }

            return matchedWordsList;            ;
        }

        private void LoadDictionary()
        {
            _wordDictionary = new Dictionary<int, string>();

            using(StreamReader wordFile = new StreamReader("dictionary.txt"))
            {
                while((!wordFile.EndOfStream))
                {
                    var word = wordFile.ReadLine();
                    if (word.Length > 0)
                    {
                        int wordLength = word.Length;
                        if (!_wordDictionary.ContainsKey(wordLength))
                        {
                            _wordDictionary.Add(wordLength, "");
                        }

                        _wordDictionary[wordLength] = _wordDictionary[wordLength] + word + " ";
                    }
                    
                }
            }

            FindMatch(5, ".A..E");      //test
        }

        private string BuildRegexPattern(string inputString, int patternLength)
        {
            string regexPattern = "";
            
            if (inputString.Length > patternLength)
            {
                inputString = inputString.Substring(0, patternLength);
            }

            StringBuilder sb = new StringBuilder(inputString);
            for (var i = inputString.Length; i < patternLength; i++)
            {
                sb.Append(".");
            }

            regexPattern = sb.ToString().Replace(".", "[A-Z]").ToUpper();

            return regexPattern;
        }
    }
}
