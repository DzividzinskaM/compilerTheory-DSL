using System;
using System.Collections.Generic;
using System.IO;

namespace DSL
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader sr = new StreamReader("./../../../../../correct-test.txt");
            string text = sr.ReadToEnd();

            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(text);
            if (lexicalAnalyzer.Analyze())
            {
                Console.WriteLine(text);
                Console.WriteLine();
                showTokens(lexicalAnalyzer.Tokens);
            }
            else
            {
                Console.WriteLine(lexicalAnalyzer.ErrorMsg);
            }
           
        }


        static void showTokens(Queue<Token> tokens)
        {
            foreach(Token token in tokens)
            {
                Console.WriteLine($"{token.Type}: \t{token.Value}");
            }

        }

    }
}
