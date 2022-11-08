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
                //for (int i = 0; i < value.Length; ++i)
                //{
                //    for (int j = 58; j <= 255; ++j)
                //    {
                //        if (value[i] == (char)j)
                //        {
                //            throw new ArgumentException("Expression have symbol");
                //        }
                //    }
                //    for (int j = 0; j <= 39; ++j)
                //    {
                //        if (value[i] == (char)j)
                //        {
                //            throw new ArgumentException("Expression have symbol");
                //        }
                //    }
                //    if (value[i] == (char)44 || value[i] == (char)46) throw new ArgumentException("Expression have symbol");
                //}
                //for (int i = 58; i <= 255; ++i)
                //{
                //    if (value.Contains((char)i)) throw new ArgumentException("Expression have symbol");
                //}
                //for (int i = 0; i <= 39; ++i)
                //{
                //    if (value.Contains((char)i)) throw new ArgumentException("Expression have symbol");
                //}
                //if (value.Contains((char)44) || value.Contains((char)46)) throw new ArgumentException("Expression have symbol");
                _expr = value;
            }
        }
        public Expression()
        {
            expr = "0";
        }
        public Expression(string expr)
        {
            this.expr = expr;
        }
        public float Calculate()
        {
            StringCalc num = new StringCalc(_expr, 0);
            float res = Expr(num);
            return res;
        }
        //private float Expr(StringCalc num)
        //{
        //    float x = factor(num);
        //    for (; ; )
        //    {
        //        char c = num.str[num.index];
        //        switch (c)
        //        {
        //            case '+':
        //                x += factor(num);
        //                break;
        //            case '-':
        //                x -= factor(num);
        //                break;
        //            default:
        //                --(num.index);
        //                return x;
        //        }
        //    }
        //}
        private float skobki(StringCalc num)
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
                return number(num);
            }
        }
        private float number(StringCalc num)
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
        private float factorial(int num)
        {
            if (num != 0) return num * factorial(num - 1);
            return 1;
        }
        private float factor(StringCalc num)
        {
            float x = skobki(num);
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
                        x *= skobki(num);
                        break;
                    case '/':
                        x /= skobki(num);
                        break;
                    case '!':
                        x = factorial((int)x);
                        break;
                    case '^':
                        x = (float)Math.Pow(x, skobki(num));
                        break;
                    default:
                        --num.index;
                        return x;
                }
            }
        }
        private float Expr(StringCalc num)
        {
            float x = factor(num);
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
                        x += factor(num);
                        break;
                    case '-':
                        x -= factor(num);
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