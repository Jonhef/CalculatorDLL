# CalculatorDLL
**Expression**  
Expression() - standart constructor(expr = 0)  
Expression(string expr) - constructor with one argument  
Expression.Calculate() - calculate your expression  
Expression.expr - string with your expression  
Expression.PI - const double with number PI
**MatAction**  
MatAction() - standart constructor(first operand = 0, action = +, second operand = 0)  
MatAction(ref int firstOperand, char action, ref int secondOperand) - constructor with arguments  
MatAction.Result() - performs the operation(fOperand action sOperand)  
static MatAction.Result(fOperand, action, sOperand) - performs the operation
