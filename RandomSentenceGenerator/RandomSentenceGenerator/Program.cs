using NDesk.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
               { "grammar=",      v => grammarFileName = v },
               { "lekser=",      v => lekserFileName = v },
               { "h|?|help",   v => help = v != null },
            };
            List<string> extra = options.Parse(args);

            if(help)
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
            
            if(string.IsNullOrWhiteSpace(lekserFileName))
                fileLoaded = FileLoader.Load(DEFAULT_LEKSER, out var lekserFileContent);
            else
                fileLoaded = FileLoader.Load(lekserFileName, out var lekserFileContent);

            if (!fileLoaded)
                return;
            
            Console.WriteLine("TODO: ");

        }

        private static void ShowHelp()
        {
            Console.WriteLine("Help");
        }
    }
}
