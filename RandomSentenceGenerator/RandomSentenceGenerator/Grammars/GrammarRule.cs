using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RandomSentenceGenerator.Grammars
{
    public interface ISymbol
    {
    }

    public struct NonTerminal : ISymbol
    {
        public NonTerminal(string value) : this()
        {
            Value = value;
        }

        public string Value { get; }

        //public override bool Equals(object obj)
        //{
        //    if (obj is NonTerminal nonTerminal)
        //        return Value == nonTerminal.Value;
        //    return false;
        //}

        //public override
    }

    public struct Terminal : ISymbol
    {
        public Terminal(string regexRule) : this()
        {
            RegexRule = regexRule;
        }

        public string RegexRule { get; }
    }
}
