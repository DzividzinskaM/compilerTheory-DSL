using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSL
{

    public enum TokenType
    {
        Identifier,
        Delimiter, 
        KeyWord,
        Number,
        Operand,
        String

    }

    class LexicalAnalyzer
    {
        private enum States { START, NUM, DLM, END, ID, ERROR, STR }

        private string _currentToken = "";
        private char _currentSymbol;
        private string[] _delimitres = { ".", ":", ";", ",", "(", ")", "=", "\"", "\"", "{", "}" };
        private string[] _keyWords = {"full-name", "address", "phone", "RNTRC", "NACE-code", "group", "tax-system", "private-entrepreneur",
                "type-one-tax-payer", "SFS-code", "year", "quarter", "month", "income", "specified-amount", "submission-date",
                "type-unified-social-tax", "start-date", "last-date", "table-1-unified-social-tax", "income-declared", "income-amount",
                "type-1DF", "employee-by-employment-contract", "employee-by-civil-contract", "section-1-DF1", "employee-ITN", "income-accured",
                "income-paid", "income-sign", "date-adopt", "date-dismiss", "sign", "unified-social-tax-report", "one-tax-payer-report", "DF1-report"
        };

        private string[] _operands = { "new", "add-new-NACE-code", "set-main-NACE-code", "change-single-tax-percent",
                "clarify", "show", "save", "add-month-data", "add-new-record", "delete-record",
                "clarify-add-new-record", "clarify-delete-record"};

        public string ErrorMsg = "";

        private States _state;
        private StringReader _sr;

        public Queue<Token> Tokens { get; }

        private string _text;

        public LexicalAnalyzer(string text)
        {

            _text = text;
            _sr = new StringReader(_text);

            Tokens = new Queue<Token>();
        }


        private bool IsDelimiter()
        {
            return Array.Exists(_delimitres, delimiter => delimiter == _currentToken);
               
        }

        private bool IsKeyWord()
        {
            return Array.Exists(_keyWords, key => key == _currentToken);
        }

        private bool IsOperand()
        {
            return Array.Exists(_operands, operand => operand == _currentToken);
        }

        private void AddNewToken(TokenType type)
        {
            Token token = new Token(type, _currentToken, _lineNumber);
            Tokens.Enqueue(token);
        }

        private void ClearCurrentToken()
        {
            _currentToken = "";
        }

        private void AddToCurrentToken()
        {
            _currentToken += _currentSymbol;
        }

        private int count = 0;

        private int _lineNumber = 0;
        private int _currentLineLength = 0;
        private int _currentLengthCount = 0;
        private string _currLine;

        private void GetNext()
        {
            //char[] currentLocal = new char[1];
            if (count == _text.Length)
                _state = States.END;
            else
            {    
                if(_currentLineLength == _currentLengthCount)
                {
                    _currLine = "";
                    while (_currLine == "")
                    {
                        _currLine = _sr.ReadLine();
                        _lineNumber++;
                        if (_currLine == null)
                        {
                            _state = States.END;
                            return;
                        }
                    }
                    _currentLineLength = _currLine.Length;
                    if(_currLine == null)
                    {
                        _state = States.END;
                        return;
                    }
                    _currentLengthCount = 0;
                    
                }

                _currentSymbol = _currLine[_currentLengthCount];
                _currentLengthCount++;               
            }
           

        }


        public bool Analyze()
        {

            _state = States.START;
            GetNext();
            char quotes = new char();

            while (true)
            {
                switch (_state)
                {
                    case States.START:
                        ClearCurrentToken();
                        if (_currentSymbol == ' ' || _currentSymbol == '\n' || _currentSymbol == '\t'
                            || _currentSymbol == '\0' || _currentSymbol == '\r')
                            GetNext();
                        else if (isSingleQuotes(_currentSymbol) || isDoubleQuotes(_currentSymbol))
                        {
                            quotes = _currentSymbol;
                            AddToCurrentToken();
                            _state = States.STR;
                            GetNext();
                        }
                        else if (char.IsLetter(_currentSymbol))
                        {
                            AddToCurrentToken();
                            _state = States.ID;
                            GetNext();
                        }
                        else if (char.IsDigit(_currentSymbol))
                        {
                            _state = States.NUM;
                            AddToCurrentToken();
                            GetNext();
                        }
                        else
                        {
                            AddToCurrentToken();
                            _state = States.DLM;
                        }
                        break;

                    case States.ID:
                        if (char.IsLetterOrDigit(_currentSymbol) || _currentSymbol == '-')
                        {
                            AddToCurrentToken();
                            GetNext();
                        }
                        else
                        {
                            if (IsKeyWord())
                                AddNewToken(TokenType.KeyWord);
                            else if (IsOperand())
                                AddNewToken(TokenType.Operand);
                            else
                            {
                                if (_currentToken.IndexOf('-') == -1)
                                    AddNewToken(TokenType.Identifier);
                                else
                                {
                                    _state = States.ERROR;
                                    ErrorMsg = "Symbol '-' isn't available for identifier";
                                }
                            }
                            _state = States.START;

                        }
                        break;
                    case States.STR:
                        if (char.IsLetterOrDigit(_currentSymbol) || _currentSymbol == '-' || _currentSymbol == '.'
                            || _currentSymbol == ' ')
                        {
                            AddToCurrentToken();
                            GetNext();
                        }
                        else if (isSingleQuotes(_currentSymbol) || isDoubleQuotes(_currentSymbol))
                        {
                            bool firstIsDoubleQ = isDoubleQuotes(quotes);
                            bool lastIsDoubleQ = isDoubleQuotes(_currentSymbol);
                            bool firstIsSingleQ = isSingleQuotes(quotes);
                            bool lastIsSingleQ = isSingleQuotes(_currentSymbol);
                            if ((firstIsDoubleQ && lastIsDoubleQ)
                                || (firstIsSingleQ && lastIsSingleQ))
                            {
                                AddToCurrentToken();
                                AddNewToken(TokenType.String);
                                _state = States.START;
                                quotes = new char();
                            }
                            else
                            {
                                _state = States.ERROR;
                                ErrorMsg = "First and last quotes are different";
                            }
                            GetNext();
                        }
                        else
                        {
                            _state = States.ERROR;
                            ErrorMsg = "You missed quotes";
                        }
                        break;
                    case States.NUM:
                        if (char.IsDigit(_currentSymbol))
                        {
                            AddToCurrentToken();
                            GetNext();
                        }
                        else
                        {
                            AddNewToken(TokenType.Number);
                            _state = States.START;
                        }
                        break;
                    case States.DLM:
                        if (IsDelimiter())
                        {
                            AddNewToken(TokenType.Delimiter);
                            _state = States.START;
                            GetNext();

                        }
                        else
                        {
                            _state = States.ERROR;
                            ErrorMsg = $"Symbol '{_currentToken}' isn't correct";
                        }
                        break;
                    case States.ERROR:
                        if (ErrorMsg.Length == 0)
                            ErrorMsg = "Something isn't correct";
                        ErrorMsg = $"Line { _lineNumber}: " + ErrorMsg;
                        _state = States.END;
                        break;
                    case States.END:
                        if (ErrorMsg.Length != 0)
                            return false;
                        else return true;
                }

            }

        }

        private bool isSingleQuotes(char sym)
        {
            return sym == '\'';
        }

        private bool isDoubleQuotes(char sym)
        {
            return sym == '\"';
        }

    }
}
