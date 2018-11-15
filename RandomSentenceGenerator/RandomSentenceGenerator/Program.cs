using Fare;
using NDesk.Options;
using RandomSentenceGenerator.Grammars;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RandomSentenceGenerator
{
    class Program
    {
        private const string DEFAULT_LEKSER = "lekser.l";
        
        static void Main(string[] args)
        {
            var glowa = new NonTerminal("S");
            var rules = new List<List<ISymbol>>();
            rules.Add(new List<ISymbol> { new Terminal("(int|char|double)"), new Terminal("[a-zA-Z_]+[0-9]*"), new Terminal(";")});
            rules.Add(new List<ISymbol> { new Terminal("(int|char|double)"), new Terminal("[a-zA-Z_]+[0-9]*"),
                new Terminal("="), new Terminal("[0-9]+"), new Terminal(";")});
            var dict = new Dictionary<NonTerminal, List<List<ISymbol>>>();
            dict.Add(glowa, new List<List<ISymbol>> { new List<ISymbol> { new NonTerminal("DEKLARACJA") } });
            dict.Add(new NonTerminal("DEKLARACJA"), rules);
            var grammar = new Grammar(new List<ISymbol>() { glowa }, dict);

            var generator = new SentenceGenerator();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(generator.GetCorrectSentence(grammar));
            }

            //string grammarFileName = null;
            //string lekserFileName = null;
            //bool help = false;

            //var options = new OptionSet() {
            //   { "grammar=",      v => grammarFileName = v },
            //   { "lekser=",      v => lekserFileName = v },
            //   { "h|?|help",   v => help = v != null },
            //};
            //List<string> extra = options.Parse(args);

            //if(help)
            //{
            //    options.WriteOptionDescriptions(Console.Out);
            //    return;
            //}
            
            //if (string.IsNullOrWhiteSpace(grammarFileName))
            //{
            //    Console.WriteLine("Missing grammar file!");
            //    return;
            //}

            //var fileLoaded = FileLoader.Load(grammarFileName, out var grammarFileContent);
            //if (!fileLoaded)
            //    return;
            
            //if(string.IsNullOrWhiteSpace(lekserFileName))
            //    fileLoaded = FileLoader.Load(DEFAULT_LEKSER, out var lekserFileContent);
            //else
            //    fileLoaded = FileLoader.Load(lekserFileName, out var lekserFileContent);

            //if (!fileLoaded)
            //    return;
            
            //Console.WriteLine("TODO: ");

        }

        private static void ShowHelp()
        {
            Console.WriteLine("Help");
        }
    }
}
