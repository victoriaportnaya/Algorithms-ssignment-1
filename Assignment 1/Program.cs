using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Data;

// calculator in work
public class TryCalculator
{
    public static void Main()
    {
        Console.WriteLine("Type your math expression >>");
        string expression = Console.ReadLine() ?? string.Empty;
        var tokens = Tokenizer.Tokenize(expression);
        var rpn = new ToRPN();
        var rpntokens = rpn.Rpnizer(tokens);
        foreach (var token in rpntokens)
        {
            Console.Write(token);
        }

        var evaluator = new Evaluator();
        double result = evaluator.Calculate(rpntokens);

        Console.WriteLine($"Your math expression is equal to {result}");
    }

}


///implement stack 
public class Stack<T>
{
    private int top = 0;
    private T[] stack;

    public Stack(int size = 15)
    {
        stack = new T[size];
    }

    public bool IsEmpty() => top == 0;

    public int Count => top;
    public void Push(T item)
    {
        if (top >= stack.Length)
            throw new Exception("Stack Overflow!");
        stack[top] = item;
        top++;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new Exception("No cells available!");
        top--;
        return stack[top];
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new Exception("Nothing to peek!");
        return stack[top - 1];
    }
}
// queue 
public class MyQueue<T> : IEnumerable<T>
{
    private Node? head;
    private Node? tail;

    private class Node
    {
        public T item;
        public Node? Next;

        public Node(T item)
        {
            this.item = item;
            this.Next = null;
        }
    }


    public MyQueue()
    {
        head = null;
        tail = null;

    }

    public void Enqueue(T item)
    {
        var temp = new Node(item);

        if (head == null)
            head = tail = temp;
        else
        {
            tail.Next = temp;
            tail = temp;
        }
    }

    public T Dequeue()
    {
        if (head == null)
            throw new Exception("Nothing to dequeue!");

        T item = head.item;
        head = head.Next;

        if (head == null)
            tail = null;

        return item;
    }

    public bool IsEmpty() => head == null;

    public IEnumerator<T> GetEnumerator()
    {
        Node? current = head;
        while (current != null)
        {
            yield return current.item;
            current = current.Next;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

}
// tokenizer
public class Tokenizer
{
    public static HashSet<char> Operators = new HashSet<char> { '+', '-', '*', '/', ')', '(' };

    public static MyQueue<string> Tokenize(string expression)
    {
        MyQueue<string> tokens = new MyQueue<string>();
        StringBuilder currentToken = new StringBuilder();

        foreach (char c in expression)
        {
            if (Operators.Contains(c))
            {
                if (currentToken.Length > 0)
                {
                    tokens.Enqueue(currentToken.ToString());
                    currentToken.Clear();
                }
                tokens.Enqueue(c.ToString());
            }

            else if (!char.IsWhiteSpace(c))
            {
                currentToken.Append(c);
            }
        }
        if (currentToken.Length > 0)
        {
            tokens.Enqueue(currentToken.ToString());
        }

        return tokens;

    }
}

// to PRN shunting yard 
class OperatorsPriority
{
    private static Dictionary<char, int> priority = new Dictionary<char, int>
    {
        {'+', 1},
        {'-', 1},
        {'*', 2},
        {'/', 2},
        {')', 0},
        {'(', 0}
    };

    public static int GetPriority(char op)
    {
        if (!priority.TryGetValue(op, out int priorityValue))
        {
            throw new ArgumentException("Invalid operator!");
        }

        return priorityValue;
    }

}


public class ToRPN
{
    private Stack<char> stack;
    private MyQueue<string> result;

    public ToRPN()
    {
        stack = new Stack<char>();
        result = new MyQueue<string>();
    }

    public MyQueue<string> Rpnizer(MyQueue<string> tokens)
    {
        foreach (var token in tokens)
        {
            if (double.TryParse(token, out double _))
            {
                result.Enqueue(token);
            }
            else if (Tokenizer.Operators.Contains(token[0]))
            {
                char operatorChar = token[0];


                if (operatorChar == '(')
                {
                    stack.Push(operatorChar);
                }

                else if (operatorChar == ')')
                {
                    while (!stack.IsEmpty() && stack.Peek() != '(')
                    {
                        result.Enqueue(stack.Pop().ToString());
                    }

                    if (!stack.IsEmpty() && stack.Peek() == '(')
                    {
                        stack.Pop();
                    }
                }

                else
                {
                    while (!stack.IsEmpty() && (OperatorsPriority.GetPriority(stack.Peek()) >=
                                                OperatorsPriority.GetPriority(operatorChar)))
                    {
                        result.Enqueue(stack.Pop().ToString());
                    }

                    stack.Push(operatorChar);
                }
            }
        }


        while (!stack.IsEmpty())
        {
            result.Enqueue(stack.Pop().ToString());
        }


        return result;
    }
}



// evaluate RPN
public class Evaluator
{
    private Stack<double> newStack;

    public Evaluator()
    {
        newStack = new Stack<double>();
    }

    public double Calculate(MyQueue<string> result)
    {
        foreach (var ch in result)
        {
            if (double.TryParse(ch, out double number))

                newStack.Push(number);

            else
            {
                if (newStack.Count < 2)
                {
                    throw new InvalidOperationException("Operands");
                }


                double right = newStack.Pop();
                double left = newStack.Pop();

                switch (ch)
                {
                    case "+":
                        newStack.Push(left + right);
                        break;
                    case "-":
                        newStack.Push(left - right);
                        break;
                    case "*":
                        newStack.Push(left * right);
                        break;
                    case "/":
                        if (right == 0)
                            throw new InvalidOperationException("Cannot divide by zero!");

                        newStack.Push(left / right);
                        break;
                    default:
                        throw new InvalidOperationException("The operation is not supported!");
                }

            }
        }

        if (newStack.Count != 1)
        {
            throw new InvalidOperationException("Invalid!");
        }
        return newStack.Pop();





    }
}






