using System;
using System.Collections.Generic;
using System.IO;

namespace DSL
{
    class Program
    {
        static void Main(string[] args)
        {
            string correctFilePath = "./../../../../../correct-test.txt";
            string incorrectFilePath = "./../../../../../incorrect-test.txt";


            Console.WriteLine("------------------Analyze correct program code------------------");
            Analyze(correctFilePath);


            Console.WriteLine("------------------Analyze incorrect program code------------------");
            Analyze(incorrectFilePath);


        }

        private static void Analyze(string filePath)
        {

            StreamReader sr = new StreamReader(filePath);
            string text = sr.ReadToEnd();

            Console.WriteLine(text);
            Console.WriteLine();

            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(text);
            if (lexicalAnalyzer.Analyze())
            {
               
                Console.ForegroundColor = ConsoleColor.Green;
                showTokens(lexicalAnalyzer.Tokens);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lexicalAnalyzer.ErrorMsg);
                Console.ResetColor();
            }

            Console.WriteLine();
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
