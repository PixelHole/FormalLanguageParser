namespace GrammarUnderstander;

static class Program
{
    private static StandardGrammar grammar;
    private static string[] Inputs;

    public static void Main(string[] args)
    {
        CreateGrammar();
        GetInputs();
        CheckInputs();
    }

    private static void CreateGrammar()
    {
        try
        {
            grammar = new StandardGrammar(GetRules());
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            CreateGrammar();
        }
    }
    private static List<string> GetRules()
    {
        List<string> rules = new List<string>();

        Console.WriteLine("Enter Rule Count : ");
        int ruleCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter rules in the following format -> RuleLabel(Char)->Rule(String)");
        for (int i = 0; i < ruleCount; i++)
        {
            rules.Add(Console.ReadLine());
        }

        return rules;
    }
    private static void GetInputs()
    {
        Console.WriteLine("Enter input count :");
        int inputCount = int.Parse(Console.ReadLine());

        Inputs = new string[inputCount];

        Console.WriteLine("enter inputs(one input per line)");
        for (int i = 0; i < inputCount; i++)
        {
            Inputs[i] = Console.ReadLine();
        }
    }
    private static void CheckInputs()
    {
        foreach (string input in Inputs)
        {
            Console.WriteLine(grammar.isStringValid(input) ? "Yes" : "No");
        }
    }
}