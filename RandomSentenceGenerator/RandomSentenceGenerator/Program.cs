using NDesk.Options;
using RandomSentenceGenerator.Files;
using RandomSentenceGenerator.Generator;
using RandomSentenceGenerator.Grammars;
using System;
using System.Collections.Generic;

namespace RandomSentenceGenerator
{
    class Program
    {
        private const string DEFAULT_LEKSER = "lekser.l";
        
        static void Main(string[] args)
        {
            string grammarFileName = null;
            string lekserFileName = null;
            bool help = false;

            var options = new OptionSet() {
               { "g|grammar=",      v => grammarFileName = v },
               { "l|lekser=",      v => lekserFileName = v },
               { "h|?|help",   v => help = v != null },
            };
            List<string> extra = options.Parse(args);

            if (help)
            {
                options.WriteOptionDescriptions(Console.Out);
                return;
            }

            if (string.IsNullOrWhiteSpace(grammarFileName))
            {
                Console.WriteLine("Missing grammar file!");
                return;
            }

            var fileLoaded = FileLoader.Load(grammarFileName, out var grammarFileContent);
            if (!fileLoaded)
                return;

            string lekserFileContent;
            if (string.IsNullOrWhiteSpace(lekserFileName))
                fileLoaded = FileLoader.Load(DEFAULT_LEKSER, out lekserFileContent);
            else
                fileLoaded = FileLoader.Load(lekserFileName, out lekserFileContent);

            if (!fileLoaded)
                return;

            var fileTranslator = new Translator();
            var terminals = fileTranslator.ToTerminals(lekserFileName, lekserFileContent);
            var grammar = fileTranslator.ToGrammar(grammarFileName, grammarFileContent, terminals);


            var generator = new SentenceGenerator();
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine(generator.GetCorrectSentence(grammar));
            }

            Console.WriteLine("TODO: ");
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Help");
        }
    }
}
