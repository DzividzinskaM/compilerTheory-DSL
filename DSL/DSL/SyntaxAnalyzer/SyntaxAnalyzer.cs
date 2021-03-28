using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL
{
    public class SyntaxAnalyzer
    {
        Queue<Token> Tokens { get; set; }

        public List<AbstractExpression> Expressions { get; set; }

        private STATE state;

        private enum STATE { start, initialization, definition, expression, declaration, assignment, operation, end}


        public SyntaxAnalyzer(Queue<Token> tokens)
        {
            Tokens = tokens;

            Expressions = new List<AbstractExpression>();
        }

        public void Analyze()
        {
        
            while(Tokens.Count != 0)
                Parse();
        }

    

        private AbstractExpression Parse()
        {
            List<Token> currentTokens = new List<Token>();
            List<AbstractExpression> definition = new List<AbstractExpression>();
            Token token = Tokens.Dequeue();

            Token assignmentToken = new Token();
            bool initialization = false;
            bool operation = false;

            while(token.Value != ";")
            {

                if(token.Value == "=")
                    assignmentToken = token;
                if (token.Type == TokenType.KeyWord && Tokens.Peek().Type == TokenType.Identifier)
                    initialization = true;
                if (token.Type == TokenType.Operand)
                    operation = true;

                currentTokens.Add(token);
                token = Tokens.Dequeue();
            }



            if (currentTokens.IndexOf(assignmentToken) != -1 && initialization)
                Expressions.Add(parseDeclarationExpression(currentTokens, currentTokens.IndexOf(assignmentToken)));
            else if (currentTokens.IndexOf(assignmentToken) != -1)
                Expressions.Add(parseAssignmentExpression(currentTokens, currentTokens.IndexOf(assignmentToken)));
            else if (initialization)
                Expressions.Add(parseInitialization(currentTokens));
            else if (operation)
                Expressions.Add(parseOperation(currentTokens));
            else
                throw new Exception("Syntax analyzer can't read. Incorrect syntax");


         
            return null;
           
        }

        private AbstractExpression parseOperation(List<Token> currentTokens)
        {
            OperationExpression expression = new OperationExpression();

            if (currentTokens[0].Type != TokenType.Identifier)
                throw new Exception("Need an identifier for calling method");
            expression.identifier = currentTokens[0].Value;
            if (currentTokens[2].Type != TokenType.Operand)
                throw new Exception("Incorrect type of method");
            expression.methodName = currentTokens[2].Value;

            int startIndex = -1;
            int lastIndex = -1;

            for(int i=0; i<currentTokens.Count; i++)
            {
                if (currentTokens[i].Value == "(")
                    startIndex = i;
                if (currentTokens[i].Value == ")")
                    lastIndex = i;
            }

            if (startIndex != -1 && lastIndex == -1)
                throw new Exception("Need a pair of '('");


            expression.attributes = parseAttributes(currentTokens.GetRange(startIndex +1, lastIndex-startIndex));
            

            return expression;
        }

        private List<string> parseAttributes(List<Token> currentTokens)
        {
            List<string> attrs = new List<string>();

            string currentValue = "";
            for(int i=0; i<currentTokens.Count; i++)
            {
                if(currentTokens[i].Value =="," || currentTokens[i].Value == ")")
                {
                    attrs.Add(currentValue);
                    currentValue = "";
                }
                else
                {
                    currentValue += currentTokens[i].Value;
                   
                }
            }

            //attrs.Add(currentValue);

            return attrs;
        }

        private AbstractExpression parseDeclarationExpression(List<Token> currentTokens, int assignmentIndex)
        {
            DeclarationExpression expression = new DeclarationExpression();

            List<Token> left = currentTokens.GetRange(0, assignmentIndex);

            List<Token> right = currentTokens.GetRange(assignmentIndex+1, currentTokens.Count-assignmentIndex-1);

            expression.initializationExpression = (InitializationExpression)parseInitialization(left);

            expression.expression = (Expression)parseExpression(right);

            return expression;           
        }

        private AbstractExpression parseInitialization(List<Token> currentTokens)
        {
            InitializationExpression expression = new InitializationExpression();

            if (currentTokens[0].Type == TokenType.KeyWord)
                expression.keyWord = currentTokens[0].Value;
            else
                throw new Exception("When initialize variables start with type of data");

            if (currentTokens[1].Type == TokenType.Identifier)
                expression.identifier = currentTokens[1].Value;
            else
                throw new Exception("Incorrect identifier");

            return expression;

        }

        private AbstractExpression parseAssignmentExpression(List<Token> currentTokens, int index)
        {
            AssignmentExpression expression = new AssignmentExpression();

            List<Token> left = getLeftExpression(currentTokens, ref expression, index);

            List<Token> right = getRightExpression(currentTokens, ref expression, index);
                new List<Token>();
            
            return expression;
           
        }

        private List<Token> getRightExpression(List<Token> currentTokens, ref AssignmentExpression expression, int index)
        {
            List<Token> right = new List<Token>();

            for (int i = index + 1; i < currentTokens.Count; i++)
            {
                right.Add(currentTokens[i]);
            }
            expression.Right = (Expression)parseExpression(right);

            return right;

        }

        private List<Token> getLeftExpression(List<Token> currentTokens, ref AssignmentExpression expression, int index)
        {
            List<Token> left = new List<Token>();

            for (int i = 0; i < index; i++)
            {
                left.Add(currentTokens[i]);
            }

            if (left.Count == 1 && left[0].Type == TokenType.Identifier)
                expression.identifier = left[0].Value;
            else if (left[1].Value == ".")
                expression.propertiesExpression = parsePropertyExpression(left);
            else
                throw new Exception("Left part of assignment expression isn't correct");

            return left;
        }

        private AbstractExpression parseExpression(List<Token> currentTokens)
        {
            Expression expression = new Expression();


           
            if (currentTokens.Count == 3 && currentTokens[1].Type == TokenType.Identifier)
            {
                for(int i=0; i<currentTokens.Count; i++)
                {
                    expression.Identifier += currentTokens[i].Value;
                }
            }
            
            int startIndex = -1;
            int lastIndex = -1;

            for(int i=0; i<currentTokens.Count; i++)
            {
                if (currentTokens[i].Value == "{")
                    startIndex = i;
                if (currentTokens[i].Value == "}")
                    lastIndex = i;

            }

            if (currentTokens.Count == 1)
            {
                expression.Identifier = currentTokens[0].Value;
            }
            else if(startIndex !=-1)
            {
                if (lastIndex == -1)
                    throw new Exception("Must be a pair for '}'");

                if (currentTokens[0].Type == TokenType.KeyWord)
                    expression.keyWord = currentTokens[0].Value;
                else
                    throw new Exception("Expression must contains data type");

                expression.definitionExpressions = new List<DefinitionExpression>();
                startIndex++;

                int index = startIndex;
                for (int i = startIndex; i<lastIndex; i++)
                {
                    if(currentTokens[i+1].Value == "," || currentTokens[i+1].Value == "}")
                    {
                        expression.definitionExpressions.Add(parseDefinitionExpression(currentTokens.GetRange(index, i - index+1)));
                        index = i + 2;
                    }
                }

                
            }


            return expression;
        }

        private DefinitionExpression parseDefinitionExpression(List<Token> currentTokens)
        {
            DefinitionExpression expression = new DefinitionExpression();

            if (currentTokens[0].Type == TokenType.KeyWord)
                expression.keyWord = currentTokens[0].Value;
            else
                throw new Exception("Left part of definition expression must be a key word");

           
            string identifierValue = "";
            for(int i=2; i < currentTokens.Count; i++)
            {
                Token token = currentTokens[i];
                if (token.Type == TokenType.Delimiter || token.Type == TokenType.Identifier || token.Type == TokenType.Number)
                    identifierValue += currentTokens[i].Value;
                else
                    throw new Exception("Incorrect type in right part of definition expression");
            }


            expression.identifier = identifierValue;
            return expression;
        }

        private PropertyExpression parsePropertyExpression(List<Token> left)
        {
            PropertyExpression expression = new PropertyExpression();
            if (left[0].Type == TokenType.Identifier)
                expression.identifier = left[0].Value;
            else
            {
                throw new Exception("In property value first parameter must be identifier");
            }
            if (left[2].Type == TokenType.KeyWord)
                expression.keyWord = left[2].Value;
            else
                throw new Exception("In property value second parameter must be key word");

            return expression;

        }
    }

}
