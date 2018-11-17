using RandomSentenceGenerator.Grammars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomSentenceGenerator
{
    public class FileTranslator
    {
        public List<Terminal> ToTerminals(string fileName, string fileContent)
        {
            if (fileName.Contains(".l"))
                return FromLexToTerminals(fileContent);

            return null;
        }

        public Grammar ToGrammar(string fileName, string fileContent, List<Terminal> terminals)
        {
            if (fileName.Contains(".y") && terminals.Count != 0)
                return FromYaccFileToGrammar(fileContent, terminals);

            return null;
        }

        private Grammar FromYaccFileToGrammar(string fileContent, List<Terminal> terminals)
        {
            var grammarFile = fileContent.Split("%%")[1];
            grammarFile = grammarFile.TrimStart().TrimEnd();
            var grammarRules = grammarFile.Split("\n");

            var grammar = new Dictionary<NonTerminal, List<List<ISymbol>>>();
            List<List<ISymbol>> rules = new List<List<ISymbol>>();
            foreach (var rule in grammarRules)
            {
                if (rule.Contains(":"))
                {
                    var splitedRule = rule.Split(":");
                    var nonTerminal = new NonTerminal(splitedRule[0]);
                    rules = new List<List<ISymbol>>();
                    var leftSideSymbols = splitedRule[1].TrimStart().Split(" ");
                    var inlineRule = new List<ISymbol>();
                    foreach (var leftSideSymbol in leftSideSymbols)
                    {
                        if (leftSideSymbol == ";") break;
                        if (leftSideSymbol == string.Empty ||
                            leftSideSymbol.Contains("{") ||
                            leftSideSymbol.Contains("}")) continue;

                        var supposedTerminal = terminals.FirstOrDefault(x => x.Name == leftSideSymbol);

                        if (!supposedTerminal.Equals(default(Terminal)))
                        {
                            inlineRule.Add(supposedTerminal);
                            continue;
                        }
                    
                        if(leftSideSymbol.Contains("'") && leftSideSymbol.Contains("'"))
                        {
                            supposedTerminal = terminals.FirstOrDefault(x => x.Name == "*yytext");
                            if(!supposedTerminal.Equals(default(Terminal)))
                            {
                                inlineRule.Add(supposedTerminal);
                                continue;
                            }
                        }

                        inlineRule.Add(new NonTerminal(leftSideSymbol));
                    }
                    rules.Add(inlineRule);
                    grammar.Add(nonTerminal, rules);
                }
                else if (rule == ";")
                    continue;
                else
                {
                    var leftSideSymbols = rule.Replace("|", "").TrimStart().Split(" ");
                    var inlineRule = new List<ISymbol>();
                    foreach (var leftSideSymbol in leftSideSymbols)
                    {
                        if (leftSideSymbol == ";") break;
                        if (leftSideSymbol == string.Empty ||
                            leftSideSymbol.Contains("{") ||
                            leftSideSymbol.Contains("}")) continue;

                        var supposedTerminal = terminals.FirstOrDefault(x => x.Name == leftSideSymbol);

                        if (!supposedTerminal.Equals(default(Terminal)))
                        {
                            inlineRule.Add(supposedTerminal);
                            continue;
                        }

                        if (leftSideSymbol.Contains("'") && leftSideSymbol.Contains("'"))
                        {
                            supposedTerminal = terminals.FirstOrDefault(x => x.Name == "*yytext");
                            if (!supposedTerminal.Equals(default(Terminal)))
                            {
                                inlineRule.Add(supposedTerminal);
                                continue;
                            }
                        }

                        inlineRule.Add(new NonTerminal(leftSideSymbol));
                    }
                    rules.Add(inlineRule);
                }
            }
            var head = grammar.Keys.FirstOrDefault(x => x.Name.ToLower() == "head");
            if (head.Equals(default(NonTerminal)))
                head = grammar.Keys.First();

            var startingSymbols = new List<ISymbol>() { head };
            return new Grammar(startingSymbols, grammar);
        }

        private List<Terminal> FromLexToTerminals(string fileContent)
        {
            var lexerFile = fileContent.Split("%%")[1];
            lexerFile = lexerFile.TrimStart().TrimEnd();
            var regexRules = lexerFile.Split("\n");
            var terminals = new List<Terminal>();
            foreach (var rule in regexRules)
            {
                if (string.IsNullOrWhiteSpace(rule) || !rule.Contains("return"))
                    continue;
                var terminalName = rule.Split("return")[1].Split(";")[0].Trim();
                var regex = rule.Split("{")[0].Trim();
                terminals.Add(new Terminal(terminalName, regex));
            }
            return terminals;
        }
    }
}
