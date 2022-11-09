using System.Text.RegularExpressions;
namespace CalculatorDLL
{
    public class Expression
    {
        public const double PI = 3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679;
        private string _expr;
        public string expr
        {
            get { return _expr; }
            set
            {
                _expr = value;
            }
        }
        public Expression()
        {
            expr = "0";
        }
        public Expression(string expr)
        {
            if (expr == null) throw new ArgumentNullException("Expression is null");
            this.expr = expr;
        }
        public double Calculate()
        {
            return Expr(new StringCalc(_expr, 0));
        }
        private double Brackets(StringCalc num)
        {
            char c = num.str[num.index];
            ++num.index;
            if (c == '(')
            {
                double x = Expr(num);
                ++num.index;
                return x;
            }
            else
            {
                --num.index;
                return Number(num);
            }
        }
        private double Number(StringCalc num)
        {
            double res = 0;
            for (; ; )
            {
                if (num.index >= num.str.Length)
                {
                    return res;
                }
                char c = num.str[num.index];
                ++num.index;
                if (c >= '0' && c <= '9')
                    res = res * 10 + c - '0';
                else if (c == '.' || c == ',')
                {
                    for (int i = 1; ; ++i)
                    {
                        if (num.index >= num.str.Length)
                        {
                            return res;
                        }
                        char c1 = num.str[num.index];
                        ++num.index;
                        if (c1 >= '0' && c1 <= '9')
                            res = (c1 - '0') * (int)Math.Pow(0.1, i);
                        else
                        {
                            --num.index;
                            return res;
                        }
                    }
                }
                else
                {
                    --num.index;
                    return res;
                }
            }
        }
        private double Factorial(int num)
        {
            if (num != 0) return num * Factorial(num - 1);
            return 1;
        }
        private double Factor(StringCalc num)
        {
            double x = Brackets(num);
            for (; ; )
            {
                if (num.index >= num.str.Length)
                {
                    return x;
                }
                char c = num.str[num.index];
                ++num.index;
                switch (c)
                {
                    case '*':
                        x *= Brackets(num);
                        break;
                    case '/':
                        x /= Brackets(num);
                        break;
                    case '!':
                        x = Factorial((int)x);
                        break;
                    case '^':
                        x = Math.Pow(x, Brackets(num));
                        break;
                    case 'c':
                        if (num.str[num.index + 1] == 'o')
                        {
                            if (num.str[num.index + 2] == 's')
                            {
                                if (num.str[num.index + 3] == '(')
                                {
                                    num.index += 3;
                                    x = Math.Cos((double)Brackets(num));
                                    break;
                                }
                            }
                        }
                        else if (num.str[num.index + 1] == 't')
                        {
                            if (num.str[num.index + 2] == 'g')
                            {
                                if (num.str[num.index + 3] == '(')
                                {
                                    num.index += 3;
                                    double temp = Brackets(num);
                                    x = Math.Cos(temp) / Math.Sin(temp);
                                    break;
                                }
                            }
                        }
                        break;
                    case 's':
                        if (num.str[num.index + 1] == 'i')
                            if (num.str[num.index + 2] == 'n')
                                if (num.str[num.index + 3] == '(')
                                {
                                    num.index += 3;
                                    x = Math.Sin((double)Brackets(num));
                                    break;
                                }
                        break;
                    case 't':
                        if (num.str[num.index + 1] == 'o')
                            if (num.str[num.index + 2] == 's')
                                if (num.str[num.index + 3] == '(')
                                {
                                    num.index += 3;
                                    x = Math.Tan((double)Brackets(num));
                                    break;
                                }
                        break;
                    case 'a':
                        if (num.str[num.index + 1] == 'r')
                        {
                            if (num.str[num.index + 2] == 'c')
                            {
                                switch (num.str[num.index + 3])
                                {
                                    case 'c':
                                        if (num.str[num.index + 4] == 'o')
                                        {
                                            if (num.str[num.index + 5] == 's')
                                            {
                                                if (num.str[num.index + 6] == '(')
                                                {
                                                    num.index += 6;
                                                    x = Math.Acos(Brackets(num));
                                                    break;
                                                }
                                            }
                                        }
                                        if (num.str[num.index + 4] == 't')
                                        {
                                            if (num.str[num.index + 5] == 'g')
                                            {
                                                if (num.str[num.index + 6] == '(')
                                                {
                                                    num.index += 6;
                                                    double temp = Brackets(num);
                                                    x = PI / 2 - Math.Atan(Brackets(num));
                                                }
                                            }
                                        }
                                    break;
                                    case 's':
                                        if (num.str[num.index + 4] == 'i')
                                        {
                                            if (num.str[num.index + 5] == 'n')
                                            {
                                                if (num.str[num.index + 6] == '(')
                                                {
                                                    num.index += 6;
                                                    x = Math.Asin(Brackets(num));
                                                }
                                            }
                                        }
                                    break;
                                    case 't':
                                        if (num.str[num.index + 4] == 'g')
                                        {
                                            if (num.str[num.index + 5] == '(')
                                            {
                                                num.index += 5;
                                                x = Math.Atan(Brackets(num));
                                                break;
                                            }
                                        }
                                    break;
                                }
                            }
                        }
                    break;
                    default:
                        --num.index;
                        return x;
                }
            }
        }
        private double Expr(StringCalc num)
        {
            double x = Factor(num);
            for (; ; )
            {
                if (num.index >= num.str.Length)
                {
                    return x;
                }
                char c = num.str[num.index];

                ++num.index;
                switch (c)
                {
                    case '+':
                        x += Factor(num);
                        break;
                    case '-':
                        x -= Factor(num);
                        break;
                    default:
                        --num.index;
                        return x;
                }
            }
        }
    }
    public class MatAction
    {
        public int fOperand { get { return fOperand; } set { fOperand = value; } }
        public int sOperand { get { return sOperand; } set { sOperand = value; } }
        public char action
        {
            get { return action; }
            set
            {
                if (action != '+' && action != '-' && action != '*' && action != '/')
                {
                    throw new ArgumentException("Action is not +, -, * or /");
                }
                action = value;
            }
        }
        public MatAction(ref int firstOperand, char _action, ref int secondOperand)
        {
            fOperand = firstOperand;
            sOperand = secondOperand;
            if (action != '+' && action != '-' && action != '*' && action != '/')
            {
                throw new ArgumentException("Action is not +, -, * or /");
            }
            action = _action;
        }
        public MatAction()
        {
            fOperand = 0;
            sOperand = 0;
            action = '+';
        }
        public int Result()
        {
            if (action == '+')
            {
                return fOperand + sOperand;
            }
            if (action == '-')
            {
                return fOperand - sOperand;
            }
            if (action == '*')
            {
                return fOperand * sOperand;
            }
            if (action == '/')
            {
                return fOperand / sOperand;
            }
            return fOperand + sOperand;
        }
        public static int Result(int _fOperand, char _action, int _sOperand)
        {
            if (_action == '+')
            {
                return _fOperand + _sOperand;
            }
            if (_action == '-')
            {
                return _fOperand - _sOperand;
            }
            if (_action == '*')
            {
                return _fOperand * _sOperand;
            }
            if (_action == '/')
            {
                return _fOperand / _sOperand;
            }
            return _fOperand + _sOperand;
        }
    }
    internal class StringCalc
    {
        public string str { get; set; }
        public int index { get; set; }
        public StringCalc(string str, int index)
        {
            this.str = str;
            this.index = index;
        }
    }
}