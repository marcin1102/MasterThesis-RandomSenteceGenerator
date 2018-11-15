using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSentenceGenerator.Grammars
{
    public class Grammar
    {
        public Grammar(List<ISymbol> headSymbols, Dictionary<NonTerminal, List<List<ISymbol>>> rules)
        {
            HeadSymbols = headSymbols;
            Rules = rules;
        }

        public List<ISymbol> HeadSymbols { get; }
        public Dictionary<NonTerminal, List<List<ISymbol>>> Rules { get; }

        public string GetCorrectSentence()
        {
            var rand = new Random();
            var startingRuleCount = HeadSymbols.Count;

            var sentenceBuilder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {

            }
            var startingRuleIndex = rand.Next(0, startingRuleCount);
            var startingSymbol = HeadSymbols.ElementAt(startingRuleCount);
            return null;
        }
    }
}
