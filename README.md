# FormalLanguageParser
a C# solution to parse and interperate Formal language Rule sets and check if an input string matches the rules of the language

# How it works
---
## basic idea
the idea of the algorithm is that we start from the main rules of the set. then we iterate through the input string characters one by one and go through every possible permutation of the grammar that is derieved from the main rules.
we filter these permutations, first by checking if the character at that we are iterating through in the input string matches the character in that index in the permutation and then checking if the permutation is longer than the input text
whenever in the iteration process, we encounter a character that corresponds to a label for a rule within the grammar we remove that rule from the permutations list and add all of its permutations back into the list and check the list again

## algorithm steps
1. create a list of strings and call it permutations
2. add all of the main rules of the grammar to the permutations list
3. iterate through the characters of your input string and do the following
   
   1. compare the character to the character at the exact index in all the rules in the permutations list
	 2. if the characters match, do nothing
   3. if the characters dont match and there is a rule with the same label as the character that you just checked do the following
      
      1. remove that permutation from the list
      2. copy the permutation string for each rule that has the same label as that character and replace that character within that string with the text or body of the rule
      3. add all created permutations back to the permutations list
      4. recompare restart the permutation character checking process
         
	 5. if the characters dont match and there isnt a rule with the same label as the character you just checked, remove the permutation
   6. if the permutatutions list is empty. this means that the string does not belong to the grammar

## Quick example

say we have the following grammar : 

* S->aAb
* A->Aa
* A->a

and we want to check if the following strings are from this grammar :

* aaab
* abb

### aaab
we first add the main rule to the permutations list

aAb

then we iterate through the input string character by character and compare each character to the corresponding character in every permutation that is placed within the same index

↓
aaab
↓
aAb

here the characters match so we dont do anything and simply move on to the next character

 ↓
aaab
 ↓
aAb

here the characters dont match so we follow the below steps

if the character in the permutation corresponds to a rule. meaning if there is a rule with the same label as that character, we replace that character with every permutation of that rule and add those permutations to our permutations list
if the chatacter in the permutation does not correspond to a rule, we simply delete the permutation

aAb -> ab
ab -> aAab & aab

we then go through the permutations list again

 ↓
aaab
 ↓
aab
aAab

here the first item of the list has matching characters with the input text so we dont do anything to it but for the second item we repeat the previous step

aAab -> aaab & aAaab

the length of the second permutation of "aAab" is longer than our input string so we dont add it to the permutations list, and we go through the permutations again

 ↓
aaab
 ↓
aab
aaab

all characters on all permutations match

  ↓
aaab
  ↓
aab
aaab

now here the first item has a mismatched character, so we remove it

   ↓
aaab
   ↓
aaab

all characters match
this string belongs to the grammar

### abb
we first add the main rule to the permutations list

aAb

then we iterate through the input string character by character and compare each character to the corresponding character in every permutation that is placed within the same index

↓
abb
↓
aAb

here the characters match so we dont do anything and simply move on to the next character

 ↓
abb
 ↓
aAb

here the characters dont match so we follow the below steps

if the character in the permutation corresponds to a rule. meaning if there is a rule with the same label as that character, we replace that character with every permutation of that rule and add those permutations to our permutations list
if the chatacter in the permutation does not correspond to a rule, we simply delete the permutation

aAb -> ab
ab -> aAab & aab

the first permutation of "aAb" is longer than the input string so we dont add it to the permutations list
we then re-check all permutations

 ↓
abb
 ↓
aab

here the characters are missmatched and there isnt an corresponding rule for the character 'a' so delete the permutation
with this our list becomes empty meaning the string does not belong to the grammar
