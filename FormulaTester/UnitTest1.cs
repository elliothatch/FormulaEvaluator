//UnitTest1.cs
//Created by Elliot Hatch, September 2014
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace FormulaTester
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Formula syntax test: No numbers
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest1()
        {
            Formula f = new Formula("+");
        }

        /// <summary>
        /// Formula syntax test: Extra operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest2()
        {
            Formula f = new Formula("2+5+");
        }

        /// <summary>
        /// Formula syntax test: Extra operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest2A()
        {
            Formula f = new Formula("+2+5");
        }

        /// <summary>
        /// Formula syntax test: Extra parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest3()
        {
            Formula f = new Formula("2+5*7)");
        }

        /// <summary>
        /// Formula syntax test: Extra parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest3A()
        {
            Formula f = new Formula("2+5*7(");
        }

        /// <summary>
        /// Formula syntax test: Extra parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest3B()
        {
            Formula f = new Formula("(2+5*7");
        }

        /// <summary>
        /// Formula syntax test: Extra parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest3C()
        {
            Formula f = new Formula("()");
        }
        /// <summary>
        /// Formula syntax test: Extra parenthesis
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest3D()
        {
            Formula f = new Formula(")2+5*7");
        }

        /// <summary>
        /// Formula syntax test: Invalid variable name
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest4()
        {
            Formula f = new Formula("2x");
        }

        /// <summary>
        /// Formula syntax test: Invalid operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest5()
        {
            Formula f = new Formula("%");
        }

        /// <summary>
        /// Formula syntax test: Missing operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest6()
        {
            Formula f = new Formula("2 3 + 3");
        }

        /// <summary>
        /// Formula syntax test: Missing operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest6A()
        {
            Formula f = new Formula("2 3 + 3 +");
        }

        /// <summary>
        /// Formula syntax test: Missing operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest6B()
        {
            Formula f = new Formula("-2 3 + 3");
        }

        /// <summary>
        /// Formula syntax test: Missing operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest7()
        {
            Formula f = new Formula("x 2");
        }

        /// <summary>
        /// Formula syntax test: Missing operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest8()
        {
            Formula f = new Formula("x y");
        }

        /// <summary>
        /// Formula syntax test: Validation failed
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest9()
        {
            Formula f = new Formula("x", s => s, s => false);
        }

        /// <summary>
        /// Validation failed
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest10()
        {
            Formula f = new Formula("2 + x", s => s, s => false);
        }

        /// <summary>
        /// Formula syntax test: Parentheses without operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest11()
        {
            Formula f = new Formula("5+7+(5)8");
        }

        /// <summary>
        /// Formula syntax test: Parentheses without operator
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest12()
        {
            Formula f = new Formula("5+7(5)+8");
        }

        /// <summary>
        /// Formula syntax test: Empty formula
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest13()
        {
            Formula f = new Formula("");
        }

        /// <summary>
        /// Validation succeeded
        /// </summary>
        [TestMethod()]
        public void public_constructorTest14()
        {
            Formula f = new Formula("1 + 2 + A2 + B2", s => s, s => s[1] == '2');
        }

        /// <summary>
        /// Validation failed
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest15()
        {
            Formula f = new Formula("1 + 2 + A2 + B1", s => s, s => s[1] == '2');
        }

        /// <summary>
        /// Formula syntax test: parens )(
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest16()
        {
            Formula f = new Formula("(5 + 2)(2 - 4)");
        }

        /// <summary>
        /// Formula syntax test: parens ()
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest17()
        {
            Formula f = new Formula("(5 + 2 ()) * 2");
            Formula g = new Formula("(5 + 2 + ()) * 2");
        }

        /// <summary>
        /// Formula syntax test: parens +())
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest18()
        {
            Formula f = new Formula("(5 + 2 + ()) * 2");
        }


        /// <summary>
        /// Formula syntax test: parens +)
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void public_constructorTest19()
        {
            Formula f = new Formula("2 + (5/) 3");
        }

        /// <summary>
        /// ToString test: Number
        /// </summary>
        [TestMethod()]
        public void public_toStringTest1()
        {
            Formula f = new Formula("5");
            Assert.AreEqual("5", f.ToString());
        }

        /// <summary>
        /// ToString test: Number with decimal
        /// </summary>
        [TestMethod()]
        public void public_toStringTest2()
        {
            Formula f = new Formula("5.0");
            Assert.AreEqual("5", f.ToString());
        }

        /// <summary>
        /// ToString test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_toStringTest3()
        {
            Formula f = new Formula("5.00000000000");
            Assert.AreEqual("5", f.ToString());
        }

        /// <summary>
        /// ToString test: Number
        /// </summary>
        [TestMethod()]
        public void public_toStringTest3A()
        {
            Formula f = new Formula(".5");
            Assert.AreEqual("0.5", f.ToString());
        }

        /// <summary>
        /// ToString test: scientific notation
        /// </summary>
        [TestMethod()]
        public void public_toStringTest3B()
        {
            Formula f = new Formula("5.23e15");
            Assert.AreEqual("5.23E+15", f.ToString());
        }

        /// <summary>
        /// ToString test: scientific notation
        /// </summary>
        [TestMethod()]
        public void public_toStringTest3C()
        {
            Formula f = new Formula("5.23E+10");
            Assert.AreEqual("52300000000", f.ToString());
        }

        /// <summary>
        /// ToString test: scientific notation
        /// </summary>
        [TestMethod()]
        public void public_toStringTest3D()
        {
            Formula f = new Formula("5.23e-10");
            Assert.AreEqual("5.23E-10", f.ToString());
        }

        /// <summary>
        /// ToString test: number formula
        /// </summary>
        [TestMethod()]
        public void public_toStringTest4()
        {
            Formula f = new Formula("5 + 3");
            Assert.AreEqual("5+3", f.ToString());
        }

        /// <summary>
        /// ToString test: number formula
        /// </summary>
        [TestMethod()]
        public void public_toStringTest5()
        {
            Formula f = new Formula("5.0000000000 + 3.000000000");
            Assert.AreEqual("5+3", f.ToString());
        }

        /// <summary>
        /// ToString test: variable formula
        /// </summary>
        [TestMethod()]
        public void public_toStringTest6()
        {
            Formula f = new Formula("A1");
            Assert.AreEqual("A1", f.ToString());
        }

        /// <summary>
        /// ToString test: formula
        /// </summary>
        [TestMethod()]
        public void public_toStringTest7()
        {
            Formula f = new Formula("5 - A1A");
            Assert.AreEqual("5-A1A", f.ToString());
        }

        /// <summary>
        /// ToString test: formula
        /// </summary>
        [TestMethod()]
        public void public_toStringTest8()
        {
            Formula f = new Formula("ABC123 / X1Z2");
            Assert.AreEqual("ABC123/X1Z2", f.ToString());
        }

        /// <summary>
        /// ToString test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_toStringTest9()
        {
            Formula f = new Formula("a1b2", s => s.ToUpper(), s => true);
            Assert.AreEqual("A1B2", f.ToString());
        }

        /// <summary>
        /// ToString test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_toStringTest10()
        {
            Formula f = new Formula("2 + a1b2 * 3 / cd123", s => s.ToUpper(), s => true);
            Assert.AreEqual("2+A1B2*3/CD123", f.ToString());
        }

        /// <summary>
        /// ToString test: trailing 0
        /// </summary>
        [TestMethod()]
        public void public_toStringTest11()
        {
            Formula f = new Formula("10.0250");
            Assert.AreEqual("10.025", f.ToString());
        }

        /// <summary>
        /// GetVariables test: no variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest1()
        {
            Formula f = new Formula("5");
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        /// <summary>
        /// GetVariables test: no variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest2()
        {
            Formula f = new Formula("5 + 2 * 3");
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        /// <summary>
        /// GetVariables test
        /// </summary>
        [TestMethod()]
        public void public_variablesTest3()
        {
            Formula f = new Formula("A1");
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test
        /// </summary>
        [TestMethod()]
        public void public_variablesTest4()
        {
            Formula f = new Formula("A1 + B2");
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test
        /// </summary>
        [TestMethod()]
        public void public_variablesTest5()
        {
            Formula f = new Formula("A1 + 2 * B2");
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: repeated variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest6()
        {
            Formula f = new Formula("A1 + A1");
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: repeated variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest7()
        {
            Formula f = new Formula("A1 + A1 + B1 * 2 / B2");
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: no variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest8()
        {
            Formula f = new Formula("5", s => s.ToUpper(), s => true);
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        /// <summary>
        /// GetVariables test: no variables
        /// </summary>
        [TestMethod()]
        public void public_variablesTest9()
        {
            Formula f = new Formula("5 + 2 * 3", s => s.ToUpper(), s => true);
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        /// <summary>
        /// GetVariables test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_variablesTest10()
        {
            Formula f = new Formula("a1", s => s.ToUpper(), s => true);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_variablesTest11()
        {
            Formula f = new Formula("a1 + b2", s => s.ToUpper(), s => true);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }


        /// <summary>
        /// GetVariables test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_variablesTest12()
        {
            Formula f = new Formula("a1 + 2 * b2", s => s.ToUpper(), s => true);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: repeated variables after normalizer
        /// </summary>
        [TestMethod()]
        public void public_variablesTest13()
        {
            Formula f = new Formula("A1 + a1", s => s.ToUpper(), s => true);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// GetVariables test: repeated variables after normalizer
        /// </summary>
        [TestMethod()]
        public void public_variablesTest14()
        {
            Formula f = new Formula("A1 + a1 + b1 * 2 / B2", s => s.ToUpper(), s => true);
            IEnumerator<string> e = f.GetVariables().GetEnumerator();
            e.MoveNext();
            Assert.AreEqual("A1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B1", e.Current);
            e.MoveNext();
            Assert.AreEqual("B2", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// Evaluate test: number
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest1()
        {
            Formula f = new Formula("2");
            Assert.AreEqual(2.0, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: decimal
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest1A()
        {
            Formula f = new Formula("0.2");
            Assert.AreEqual(0.2, f.Evaluate(s => 0.0));
        }


        /// <summary>
        /// Evaluate test: scientific notation
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest1B()
        {
            Formula f = new Formula("2.23E+12");
            Assert.AreEqual(2.23e+12, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: scientific notation
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest1C()
        {
            Formula f = new Formula("2.23e-12");
            Assert.AreEqual(2.23e-12, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: variable
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest2()
        {
            Formula f = new Formula("A4");
            Assert.AreEqual(10.0, f.Evaluate(s => 10.0));
        }

        /// <summary>
        /// Evaluate test: add
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest3()
        {
            Formula f = new Formula("5 + 3");
            Assert.AreEqual(8.0, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: add
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest4()
        {
            Formula f = new Formula("5.0 + 3.0");
            Assert.AreEqual(8.0, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: subtract
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest5()
        {
            Formula f = new Formula("5.5 - 3.98");
            Assert.AreEqual(1.52, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: multiply
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest6()
        {
            Formula f = new Formula("5.1 * 3.9");
            Assert.AreEqual(5.1 * 3.9, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: divide
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest7()
        {
            Formula f = new Formula("11.44 / 5.2");
            Assert.AreEqual(11.44 / 5.2, f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest8()
        {
            Assert.AreEqual(2.0*6.0+3.0, new Formula("2*6+3").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest9()
        {
            Assert.AreEqual(2.0+6.0*3.0, new Formula("2+6*3").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest10()
        {
            Assert.AreEqual((2.0+6.0)*3.0, new Formula("(2+6)*3").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest11()
        {
            Assert.AreEqual(2.0*(3.0+5.0), new Formula("2*(3+5)").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest12()
        {
            Assert.AreEqual(2.0+(3.0+5.0), new Formula("2+(3+5)").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest13()
        {
            Assert.AreEqual(2.0+(3.0+5.0*9.0), new Formula("2+(3+5*9)").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest14()
        {
            Assert.AreEqual(2.0+3.0*(3.0+5.0), new Formula("2+3*(3+5)").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: order of operations
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest15()
        {
            Assert.AreEqual(2.0+3.0*5.0+(3.0+4.0*8.0)*5.0+2.0, new Formula("2+3*5+(3+4*8)*5+2").Evaluate(s => 0));
        }

        /// <summary>
        /// Evaluate test: variable
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest16()
        {
            Formula f = new Formula("3.2 + A1");
            Assert.AreEqual(3.2 + 4.2, f.Evaluate(s => 4.2));
        }

        /// <summary>
        /// Evaluate test: order of operations/parentheses/negative
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest17()
        {
            Formula f = new Formula("(1.2 * (((1.15 + 1.15) * 0.2) - 9.1) / (3.4 + (3)))");
            Assert.AreEqual((1.2 * (((1.15 + 1.15) * 0.2) - 9.1) / (3.4 + (3.0))), f.Evaluate(s => 0.0));
        }

        /// <summary>
        /// Evaluate test: repeated variables/negative
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest18()
        {
            Formula f = new Formula("(1.2 * (((B1 + B1) * 0.2) - A1) / (3.4 + (3)))");
            Assert.AreEqual((1.2 * (((1.15 + 1.15) * 0.2) - 9.1) / (3.4 + (3.0))), f.Evaluate(s => { if (s == "A1") return 9.1; else return 1.15; }));
        }

        /// <summary>
        /// Evaluate test: divide by zero
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest19()
        {
            Formula f = new Formula("5 / 0");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        /// <summary>
        /// Evaluate test: invalid variable
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest20()
        {
            Formula f = new Formula("5 + A1");
            Assert.IsInstanceOfType(f.Evaluate(s => { throw new ArgumentException(); }), typeof(FormulaError));
        }

        /// <summary>
        /// Evaluate test: divide by zero with variable
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest21()
        {
            Formula f = new Formula("5 / A1");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        /// <summary>
        /// Evaluate test: parentheses
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest22()
        {
            Formula f = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6.0, f.Evaluate(s => 1));
        }

        /// <summary>
        /// Evaluate test: parentheses
        /// </summary>
        [TestMethod()]
        public void public_evaluateTest23()
        {
            Formula f = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(18.0, f.Evaluate(s => 3));
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest1()
        {
            Formula f = new Formula("1");
            Formula g = new Formula("1.0000");
            Assert.IsTrue(f.Equals(g));
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest1A()
        {
            Formula f = new Formula("1");
            Formula g = new Formula("1.0000");
            Assert.IsTrue(f == g);
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest1B()
        {
            Formula f = new Formula("1.0000");
            Formula g = new Formula("2.0000");
            Assert.IsFalse(f == g);
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTestC()
        {
            Formula f = new Formula("1.0000");
            Formula g = new Formula("2.0000");
            Assert.IsFalse(f.Equals(g));
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest2()
        {
            Formula f = new Formula("1 + 2.0000");
            Formula g = new Formula("1.0000 + 2");
            Assert.IsTrue(f.Equals(g));
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest2A()
        {
            Formula f = new Formula("1 + 2.0000");
            Formula g = new Formula("1.0000 + 2");
            Assert.IsTrue(f == g);
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest2B()
        {
            Formula f = new Formula("2 + 1.0000");
            Formula g = new Formula("1.0000 + 1");
            Assert.IsFalse(f.Equals(g));
        }

        /// <summary>
        /// Equals test: trailing zeroes
        /// </summary>
        [TestMethod()]
        public void public_equalsTest2C()
        {
            Formula f = new Formula("2 + 1.0000");
            Formula g = new Formula("1.0000 + 2");
            Assert.IsFalse(f == g);
        }

        /// <summary>
        /// Equals test: operators
        /// </summary>
        [TestMethod()]
        public void public_equalsTest3()
        {
            Formula f = new Formula("1 + 1 * 1");
            Formula g = new Formula("1 - 1");
            Assert.IsFalse(f.Equals(g));
        }

        /// <summary>
        /// Equals test: operators
        /// </summary>
        [TestMethod()]
        public void public_equalsTest3A()
        {
            Formula f = new Formula("1 + 1 * 1");
            Formula g = new Formula("1 - 1");
            Assert.IsFalse(f == g);
        }

        /// <summary>
        /// Equals test: operators
        /// </summary>
        [TestMethod()]
        public void public_equalsTest34()
        {
            Formula f = new Formula("1 + 1 * 1");
            Formula g = new Formula("a - 1 * 2");
            Assert.IsFalse(f.Equals(g));
        }

        /// <summary>
        /// Equals test: white space
        /// </summary>
        [TestMethod()]
        public void public_equalsTest4()
        {
            Formula f = new Formula("a * 2 + b");
            Formula g = new Formula("a*          2+b");
            Assert.IsTrue(f.Equals(g));
        }

        /// <summary>
        /// Equals test: white space
        /// </summary>
        [TestMethod()]
        public void public_equalsTest4A()
        {
            Formula f = new Formula("a * 2 + b");
            Formula g = new Formula("a*          2+b");
            Assert.IsTrue(f == g);
        }

        /// <summary>
        /// Equals test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_equalsTest5()
        {
            Formula f = new Formula("a * 2 + b", s => s.ToUpper(), s => true);
            Formula g = new Formula("A*          2+B");
            Assert.IsTrue(f.Equals(g));
        }

        /// <summary>
        /// Equals test: normalizer
        /// </summary>
        [TestMethod()]
        public void public_equalsTest5A()
        {
            Formula f = new Formula("a * 2 + b", s => s.ToUpper(), s => true);
            Formula g = new Formula("A*          2+B");
            Assert.IsFalse(f != g);
        }

        /// <summary>
        /// Equals test: different objects
        /// </summary>
        [TestMethod()]
        public void public_equalsTest6()
        {
            Formula f = new Formula("1 + a");
            string s = "1 + a";
            Assert.IsFalse(f.Equals(s));
        }

        /// <summary>
        /// Equals test: null objects
        /// </summary>
        [TestMethod()]
        public void public_equalsTest7()
        {
            Formula f = new Formula("1 + a");
            Formula g = null;
            Assert.IsFalse(g == f);
        }
        

        
        /// <summary>
        /// ToString and Equals test: new Formula(f.ToString()).Equals(f) should be true
        /// </summary>
        [TestMethod()]
        public void public_toStringEqualsTest1()
        {
            Formula f = new Formula("a * 2.00100 + (b + 2.400e09) / 0");
            Formula g = new Formula(f.ToString());
            Assert.IsTrue(f.Equals(g));
            Assert.IsTrue(f == g);
        }
        
        /// <summary>
        /// GetHashCode test: different formulas hash differently
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest1()
        {
            Formula f = new Formula("1");
            Formula g = new Formula("2");
            Assert.AreNotEqual(f.GetHashCode(), g.GetHashCode());
        }

        /// <summary>
        /// GetHashCode test: different formulas hash differently
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest2()
        {
            Formula f = new Formula("1");
            Formula g = new Formula("a");
            Assert.AreNotEqual(f.GetHashCode(), g.GetHashCode());
        }
        /// <summary>
        /// GetHashCode test: different formulas hash differently
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest3()
        {
            Formula f = new Formula("1 + 2");
            Formula g = new Formula("1 - 2");
            Assert.AreNotEqual(f.GetHashCode(), g.GetHashCode());
        }
        /// <summary>
        /// GetHashCode test: different formulas hash differently
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest4()
        {
            Formula f = new Formula("a * 2 + b / 9");
            Formula g = new Formula("a / 2 * 9 - b");
            Assert.AreNotEqual(f.GetHashCode(), g.GetHashCode());
        }

        /// <summary>
        /// GetHashCode test: same formulas hash the same
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest5()
        {
            Formula f = new Formula("1");
            Formula g = new Formula("1");
            Assert.AreEqual(f.GetHashCode(), g.GetHashCode());
        }

        /// <summary>
        /// GetHashCode test: same formulas hash the same
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest6()
        {
            Formula f = new Formula("a");
            Formula g = new Formula("a");
            Assert.AreEqual(f.GetHashCode(), g.GetHashCode());
        }

        /// <summary>
        /// GetHashCode test: same formulas hash the same
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest7()
        {
            Formula f = new Formula("1 + 2");
            Formula g = new Formula("1 + 2");
            Assert.AreEqual(f.GetHashCode(), g.GetHashCode());
        }

        /// <summary>
        /// GetHashCode test: same formulas hash the same
        /// </summary>
        [TestMethod()]
        public void public_getHashCodeTest8()
        {
            Formula f = new Formula("a * 2 + b / 9");
            Formula g = new Formula("a * 2 + b / 9");
            Assert.AreEqual(f.GetHashCode(), g.GetHashCode());
        }


    }
}

