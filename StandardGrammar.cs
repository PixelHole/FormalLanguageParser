namespace GrammarUnderstander;

public class StandardGrammar
{
    private List<GrammarRule> Rules;
    
    public StandardGrammar(List<string> rawRules)
    {
        Rules = ParseRules(rawRules);
    }
    
    /*
     * standard grammar rule format
     * LABEL(single character)->BODY1(string)|BODY2(string)|BODY3(string)...
     * LABEL : the label of the rule, used to identify the rule within a string. this is a single character
     *      (e.g. in S->aAb A->aA. in rule S, A points to another rule)
     * BODY : the body of the rule, the string that will be used when creating a string with this rule
     *      (e.g. in S->aAb, "aAb" is the body)
     *  '|'  : Body separator character, use this to give multiple bodies to the same rule
     *
     * *you can have multiple rules with the same label
     * * the '_' character indicates the empty character(i.e null or lambda λ)
     */
    private List<GrammarRule> ParseRules(List<string> rawRules)
    {
        List<GrammarRule> rules = new List<GrammarRule>();

        foreach (var rawRule in rawRules)
        {
            string[] sections = rawRule.Split("->");
            if (sections.Length != 2) throw new ArgumentException("please enter the rule in the correct format");

            if (sections[1].Contains('|'))
            {
                foreach (var ruleBody in sections[1].Split('|'))
                {
                    rules.Add(new GrammarRule(sections[0].Trim()[0], ruleBody.Trim()));
                }
            }
            else
            {
                rules.Add(new GrammarRule(sections[0].Trim()[0], sections[1].Trim()));
            }
        }

        return rules;
    }
    
    // you wanna look at here ↓
    public bool isStringValid(string input)
    {
        // List of all permutations
        List<string> permutations = new List<string>();

        /*
         add all main rules (rules that usually start with S. in code, rules that have the same label as the first rule)
         to the permutations list
        */
        foreach (var mainRule in Rules.FindAll(rule => rule.Label == Rules[0].Label))
        {
            permutations.Add(mainRule.Body);
        }

        // for each character of the input check all permutations at that index
        for (int i = 0; i < input.Length; i++)
        {
            int checkIndex = 0;
            
            // while loop to check all permutations
            while (checkIndex < permutations.Count)
            {
                // save a copy of the current permutation that we are checking
                string permutation = permutations[checkIndex];

                // if the permutation at that index is the same as the input, move on to the next
                if (permutation[i] == input[i])
                {
                    checkIndex++;
                    continue;
                }

                /*
                 * from here on, all logic is based on the fact that the permutation character at that index does not
                 * match the input character at said index
                 */
                
                // check if there is a rule with the same label as the character in the permutation
                if (Rules.Exists(rule => rule.Label == permutation[i]))
                {
                    // if found grab all the rules that have that label
                    var foundRules = Rules.FindAll(rule => rule.Label == permutation[i]);
                    
                    // remove that permutation from the permutations list to avoid duplicates 
                    permutations.Remove(permutation);
                    // remove the rule label from the permutation so we can insert new permutations
                    permutation = permutation.Remove(i, 1);
                    
                    foreach (var rule in foundRules)
                    {
                        // insert the body of each rule at the index that we removed the label
                        string newMutation = permutation.Insert(i, rule.Body == "_" ? string.Empty : rule.Body);
                        
                        /*
                         if the generated string is longer than the input, dont insert it
                         this is to avoid infinite loops and also save time
                         */
                        if (newMutation.Length <= input.Length)
                        {
                            // checkIndex = 0;
                            permutations.Add(newMutation);
                        }
                    }
                    
                    // if we have generated new mutations, start the checking process from scratch
                    // this can definitely be improved but i cannot be fucked
                    // if(checkIndex == 0) continue;
                    continue;
                }
                
                // if the character does not match and it is not a rule, remove the mutation and move on
                permutations.Remove(permutation);
            }

            // if no mutations are in currently in the list this means that the input string does not follow the grammar
            // therefore you can safely return false
            if (permutations.Count == 0) return false;
        }

        return true;
    }
}

internal class GrammarRule
{
    public char Label { get; }
    public string Body { get; }

    public GrammarRule(char label, string body)
    {
        Label = label;
        Body = body;
    }
}