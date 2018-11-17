namespace RandomSentenceGenerator.Grammars
{
    public interface ISymbol
    {
        string Name { get; }
    }

    public struct NonTerminal : ISymbol
    {
        public NonTerminal(string name) : this()
        {
            Name = name;
        }

        public string Name { get; }
    }

    public struct Terminal : ISymbol
    {
        public Terminal(string name, string regexRule) : this()
        {
            Name = name;
            RegexRule = regexRule;
        }

        public string Name { get; }
        public string RegexRule { get; }
    }
}
