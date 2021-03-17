using System;
using System.Collections.Generic;
using System.IO;

namespace DSL
{
    class Program
    {
        static void Main(string[] args)
        {
            string correctFilePath = "./../../../../../syntax-analyzer-correct-test.txt";
            string incorrectFilePath = "./../../../../../syntax-analyzer-incorrect-test.txt";


            Console.WriteLine("------------------Analyze correct program code------------------");
            Analyze(correctFilePath);


            Console.WriteLine("------------------Analyze incorrect program code------------------");
            Analyze(incorrectFilePath);

            /*string correctFilePath = "./../../../../../syntax-analyzer-correct-test.txt";
            Analyze(correctFilePath);*/
            


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
                showErrorMsg(lexicalAnalyzer.ErrorMsg);
            }

            Console.WriteLine();

            SyntaxAnalyzer syntaxAnalyzer = new SyntaxAnalyzer(lexicalAnalyzer.Tokens);
            try
            {
                syntaxAnalyzer.Analyze();
                showExpressions(syntaxAnalyzer.Expressions);
            }
            catch(Exception exception)
            {
                showErrorMsg(exception.Message);
            }
        }

        private static void showErrorMsg(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }

        private static void showExpressions(List<AbstractExpression> expressions)
        {
            foreach(var expression in expressions)
            {
                if(expression is AssignmentExpression)
                {
                    Console.WriteLine($"type: {expression.type}");
                    AssignmentExpression assignmentExpression = (AssignmentExpression)expression;
                    Console.WriteLine("left: ");
                    if(assignmentExpression.identifier != null)
                        Console.WriteLine($"\tidentifier: {assignmentExpression.identifier}");
                    else if(assignmentExpression.propertiesExpression != null)
                    {
                        Console.WriteLine($"\ttype - {assignmentExpression.propertiesExpression.type}");
                        Console.WriteLine($"\tkeyWord: {assignmentExpression.propertiesExpression.keyWord}");
                        Console.WriteLine($"\tidentifier: {assignmentExpression.propertiesExpression.identifier}");
                    }

                    Console.WriteLine("right: ");
                    Expression right = assignmentExpression.Right;
                    showExpression(right);
                }
                else if(expression is InitializationExpression)
                {
                    showInitialization(expression);
                } else if(expression is DeclarationExpression)
                {
                    DeclarationExpression declarationExpression = (DeclarationExpression)expression;
                    Console.WriteLine($"type - {declarationExpression.type}");
                    Console.WriteLine("left");
                    showInitialization(declarationExpression.initializationExpression, "\t");

                    Console.WriteLine("right: ");
                    showExpression(declarationExpression.expression);
                } else if(expression is OperationExpression)
                {
                    OperationExpression operationExpression = (OperationExpression)expression;
                    Console.WriteLine($"type - {operationExpression.type}");
                    Console.WriteLine($"identifier - {operationExpression.identifier}");
                    Console.WriteLine($"method name - {operationExpression.methodName}");


                    Console.WriteLine("attributes");
                    foreach(var attr in operationExpression.attributes)
                    {
                        Console.WriteLine($"\tattribute: {attr}");
                    }


                }
                Console.WriteLine();
            }
        }

        private static void showExpression(Expression right)
        {
            Console.WriteLine($"\ttype: {right.type}");
            if (right.Identifier != null)
                Console.WriteLine($"\tidentifier: {right.Identifier}");
            else
            {
                Console.WriteLine($"\tkeyWord: {right.keyWord}");
                foreach (var def in right.definitionExpressions)
                {
                    Console.WriteLine($"\t\ttype: {def.type}");
                    Console.WriteLine($"\t\tkeyWord: {def.keyWord}");
                    Console.WriteLine($"\t\tidentifier: {def.identifier}");

                }
            }
        }

        private static void showInitialization(AbstractExpression expression, string t = "")
        {
            InitializationExpression initializationExpression = (InitializationExpression)expression;
            Console.WriteLine(t+ $"type: {initializationExpression.type}");
            Console.WriteLine(t+ $"keyword: {initializationExpression.keyWord}");
            Console.WriteLine(t+ $"identifier: {initializationExpression.identifier}");
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
