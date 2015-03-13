//Formula.cs
//Created by Elliot Hatch, September, 2014

// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax; variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {

        private List<Tuple<object, TokenType>> m_tokens;
        private Func<string, string> m_normalizer;
        private Func<string, bool> m_validator;

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            m_tokens = new List<Tuple<object, TokenType>>();
            m_normalizer = normalize;
            m_validator = isValid;
            int unmatchedParenthesesCount = 0;
            int index = 1;
            foreach(string str in GetTokens(formula))
            {
                //strip whitespace
                string token = Regex.Replace(str, @"\s+", "");

                double doubleValue;
                if(Char.IsLetter(token[0]) || token[0] == '_')
                {
                    if (Regex.IsMatch(token, "^[a-zA-z_]+[a-zA-z_0-9]*$") && m_validator(m_normalizer(token)))
                    {
                        m_tokens.Add(new Tuple<object, TokenType>(m_normalizer(token), TokenType.Variable));
                    }
                    else
                    {
                        throw new FormulaFormatException("Token #" + index + ": Invalid variable name '" + token + "'.");
                    }

                    if (m_tokens.Count > 1 && m_tokens[m_tokens.Count - 2].Item2 != TokenType.Operator)
                    {
                        throw new FormulaFormatException("Token #" + index +
                            ": A number or variable must be followed by an operator, instead found variable '" + token + "'.");
                    }
                }
                else if(Double.TryParse(token, out doubleValue))
                {
                    if (m_tokens.Count > 0 && (m_tokens.Last().Item2 != TokenType.Operator || m_tokens.Last().Item1 as string == ")"))
                    {
                        throw new FormulaFormatException("Token #" + index +
                            ": A number or variable must be followed by an operator, instead found number '" + token + "'.");
                    }

                    m_tokens.Add(new Tuple<object, TokenType>(doubleValue, TokenType.Number));
                }
                else
                {
                    if (token != "+" && token != "-" && token != "*" && token != "/" && token != "(" && token != ")")
                        throw new FormulaFormatException("Token #" + index + ": Invalid oprerator '" + token + "'.");

                    if (m_tokens.Count > 0)
                    {
                        if (m_tokens.Last().Item2 == TokenType.Operator)
                        {
                            //the only time two operators in a row are valid are +( )+ )) ((
                            // watch out for invalid +) (+ )+ )( ()
                            string lastToken = m_tokens.Last().Item1 as string;
                            if(token == ")" && lastToken != ")")
                                throw new FormulaFormatException("Token #" + index + 
                                    ": Right parenthesis must be preceded by a number, variable or right parenthesis, instead found '" + token + "'.");
                            if (token == "(" && lastToken == ")")
                                throw new FormulaFormatException("Token #" + index + ": Left parenthesis must be followed by a number or variable, instead found '" + token + "'.");
                           if(token != "(" && token != ")" && lastToken != ")")
                               throw new FormulaFormatException("Token #" + index + ": An operator must be followed by a number or variable, instead found '" + token + "'.");
                        }
                        else
                        {
                            // 5(
                            if (token == "(")
                                throw new FormulaFormatException("Token #" + index + ": Left parenthesis must be preceded by an operator, instead found '" + token + "'.");
                        }
                    }
                    else if (token != "(")
                        throw new FormulaFormatException("Token #" + index + ": Unmatched operator '" + token + "'.");

                    if(token == ")")
                    {
                        if(unmatchedParenthesesCount == 0)
                            throw new FormulaFormatException("Token #" + index + ": Unmatched right parenthesis.");
                        unmatchedParenthesesCount--;
                    }
                    if(token == "(")
                        unmatchedParenthesesCount++;
                    m_tokens.Add(new Tuple<object, TokenType>(token, TokenType.Operator));
                }
                index++;
            }
            if (m_tokens.Count == 0)
                throw new FormulaFormatException("Formula must contain at least one number or variable.");
            if(unmatchedParenthesesCount != 0)
                throw new FormulaFormatException("Token #" + index + ": Unmatched left parenthesis.");
            if(m_tokens.Last().Item2 == TokenType.Operator && m_tokens.Last().Item1 as string != ")")
                throw new FormulaFormatException("Token #" + index + ": Unmatched operator '" + m_tokens.Last().Item1 as string + "'.");
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<double> valueStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            foreach(Tuple<object,TokenType> token in m_tokens)
            {
                switch(token.Item2)
                {
                    case TokenType.Number:
                        evaluateNumber(Convert.ToDouble(token.Item1), valueStack, operatorStack);
                        break;
                    case TokenType.Operator:
                        evaluateOperator(token.Item1 as string, valueStack, operatorStack);
                        break;
                    case TokenType.Variable:
                        try
                        {
                            evaluateNumber(lookup(token.Item1 as string), valueStack, operatorStack);
                        }
                        catch(ArgumentException)
                        {
                            return new FormulaError("Failed vairable lookup: '" + token.Item1 + "'.");
                        }
                        break;
                }
            }

            double finalValue = 0.0;
            if(operatorStack.Count == 0)
                finalValue = valueStack.Pop();
            else
            {
                double topVal = valueStack.Pop();
                double secondVal = valueStack.Pop();
                string topOp = operatorStack.Pop();

                finalValue = evaluateSimpleExpression(secondVal, topVal, topOp);
            }

            if(Double.IsInfinity(finalValue) || Double.IsNaN(finalValue))
                return new FormulaError("Division by Zero.");

            return finalValue;
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            HashSet<string> variables = new HashSet<string>();
            foreach (Tuple<object, TokenType> token in m_tokens)
            {
                if(token.Item2 == TokenType.Variable)
                    variables.Add(token.Item1 as string);
            }
            return variables;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Tuple<object, TokenType> token in m_tokens)
            {
                stringBuilder.Append(token.Item1);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens, which are compared as doubles, and variable tokens,
        /// whose normalized forms are compared as strings.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            Formula f = obj as Formula;
            if (ReferenceEquals(f, null))
                return false;

            if (m_tokens.Count != f.m_tokens.Count)
                return false;

            for (int i = 0; i < m_tokens.Count; i++)
            {
                if (m_tokens[i].Item2 != f.m_tokens[i].Item2)
                    return false;
                if (!m_tokens[i].Item1.Equals(f.m_tokens[i].Item1))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            if (ReferenceEquals(f1, null))
            {
                return ReferenceEquals(f2, null);
            }
            else
            {
                return f1.Equals(f2);
            }
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return !(f1 == f2);
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }

        private static void evaluateNumber(double value, Stack<double> valueStack, Stack<string> operatorStack)
        {
            if(operatorStack.Count == 0)
            {
                valueStack.Push(value);
                return;
            }
            string topOp = operatorStack.Peek();
            if (topOp == "*" || topOp == "/")
            {
                double topVal = valueStack.Pop();
                operatorStack.Pop();
                valueStack.Push(evaluateSimpleExpression(topVal, value, topOp));
            }
            else
            {
                valueStack.Push(value);
            }
        }
        private static void evaluateOperator(string op, Stack<double> valueStack, Stack<string> operatorStack)
        {
            if(valueStack.Count < 2 && op != ")")
            {
                operatorStack.Push(op);
                return;
            }

            if (op == "+" || op == "-")
            {

                string topOp = operatorStack.Peek();
                if (topOp == "+" || topOp == "-")
                {
                    double topVal = valueStack.Pop();
                    double secondVal = valueStack.Pop();
                    operatorStack.Pop();
                    valueStack.Push(evaluateSimpleExpression(secondVal, topVal, topOp));
                }
                operatorStack.Push(op);
            }
            else if (op == "*" || op == "/" || op == "(")
            {
                operatorStack.Push(op);
            }
            else if(op == ")")
            {
                string topOp = operatorStack.Peek();
                if(topOp == "+" || topOp == "-")
                {
                    double topVal = valueStack.Pop();
                    double secondVal = valueStack.Pop();
                    operatorStack.Pop();
                    valueStack.Push(evaluateSimpleExpression(secondVal, topVal, topOp));
                }

                operatorStack.Pop();

                if (operatorStack.Count > 0 && valueStack.Count >= 2)
                {
                    string secondOp = operatorStack.Peek();
                    if (secondOp == "*" || secondOp == "/")
                    {
                        double topVal2 = valueStack.Pop();
                        double secondVal2 = valueStack.Pop();
                        operatorStack.Pop();
                        valueStack.Push(evaluateSimpleExpression(secondVal2, topVal2, secondOp));
                    }
                }
            }
        }

        private static double evaluateSimpleExpression(double first, double second, string op)
        {
            if(op == "+")
            {
                return first + second;
            }
            else if (op == "-")
            {
                return first - second;
            }
            else if (op == "*")
            {
                return first * second;
            }
            else if (op == "/")
            {
                return first / second;
            }
            //code should never hit this line
            return 0.0;

        }

        private enum TokenType { Number, Operator, Variable };
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}

