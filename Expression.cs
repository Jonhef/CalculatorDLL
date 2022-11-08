using System.Text.RegularExpressions;
namespace CalculatorDLL
{
    public class Expression
    {
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
        public float Calculate()
        {
            return Expr(new StringCalc(_expr, 0));
        }
        private float Brackets(StringCalc num)
        {
            char c = num.str[num.index];
            ++num.index;
            if (c == '(')
            {
                float x = Expr(num);
                ++num.index;
                return x;
            }
            else
            {
                --num.index;
                return Number(num);
            }
        }
        private float Number(StringCalc num)
        {
            int res = 0;
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
                else
                {
                    --num.index;
                    return res;
                }
            }
        }
        private float Factorial(int num)
        {
            if (num != 0) return num * Factorial(num - 1);
            return 1;
        }
        private float Factor(StringCalc num)
        {
            float x = Brackets(num);
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
                        x = (float)Math.Pow(x, Brackets(num));
                        break;
                    default:
                        --num.index;
                        return x;
                }
            }
        }
        private float Expr(StringCalc num)
        {
            float x = Factor(num);
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