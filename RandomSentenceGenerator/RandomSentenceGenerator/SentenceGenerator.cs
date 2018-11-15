using RandomSentenceGenerator.Generator;
using RandomSentenceGenerator.Grammars;
using RandomSentenceGenerator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSentenceGenerator
{
    public class SentenceGenerator
    {
        private readonly Random random = new Random();
        private readonly RandomSymbolValueGenerator symbolValueGenerator = new RandomSymbolValueGenerator();

        public string GetCorrectSentence(Grammar grammar)
        {
            var startingRuleCount = grammar.HeadSymbols.Count;
            var startingRuleIndex = random.Next(0, startingRuleCount);
            var startingSymbol = grammar.HeadSymbols.ElementAt(startingRuleIndex);

            var sequence = new List<Terminal>();
            GetSymbolSequence(startingSymbol, grammar, ref sequence);

            var sb = new StringBuilder();
            foreach (var symbol in sequence)
            {
                sb.Append(symbolValueGenerator.Generate(symbol));
                sb.Append(" ");
            }

            return sb.ToString();
        }

        private void GetSymbolSequence(ISymbol startingSymbol, Grammar grammar, ref List<Terminal> sequence)
        {
            if(startingSymbol is Terminal terminal)
            {
                sequence.Add(terminal);
                return;
            }

            var nonTerminal = (NonTerminal)startingSymbol;
            var rules = grammar.Rules[nonTerminal];
            var rulesCount = rules.Count;
            var randomRuleIndex = random.Next(rulesCount);
            var rule = rules.ElementAt(randomRuleIndex);
                
            foreach (var symbol in rule)
            {
                GetSymbolSequence(symbol, grammar, ref sequence);
            }
        }
    }
}
