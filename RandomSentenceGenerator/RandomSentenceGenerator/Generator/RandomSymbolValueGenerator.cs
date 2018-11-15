using Fare;
using RandomSentenceGenerator.Grammars;

namespace RandomSentenceGenerator.Generator
{
    public class RandomSymbolValueGenerator
    {
        public string Generate(Terminal terminal)
        {
            var generator = new Xeger(terminal.RegexRule);
            return generator.Generate();
        }
    }
}
