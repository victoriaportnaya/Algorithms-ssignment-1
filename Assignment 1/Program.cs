using System;
using System.Collections.Generic;
using System.Tetx;


// implement stack 
public class Stack<T>
{
    private int top = 0;
    private int size;
    private T[] stack;

    public Stack(int size = 15)
    {
        this.size = size;
        stack = new T[size];
    }

    public bool IsEmpty() => top == 0;

    public void Push(T item)
    {
        if (top >= size)
            throw new Exception("Stack Overflow!")
        stack[top] = item;
        top++;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new Exception("No cells available!")
        top--;
        return stack[top];
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new Exception("Nothing to peek!")
        return stack[top - 1];
    }
}
// queue 
public class Queue<T>
{
    private class Node
    {
        public T item;
        public Node Next;

        public Node(T item)
        {
            this.Item = item;
            this.Next = null;
        }
    }

    private Node head;
    private Node tail;

    public void Enqueue(T item)
    {
        var temp = new Node(item);

        if (head = null)
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
        T item = head.Item;
        head = head.Next;
        if (head == null)
            tail = null;

        return item;
    }

    public bool IsEmpty() => head == null;
}
// tokenizer
public class Tokenizer
{ 
    public static HashSet<char> operators = new HashSet<char> {"+", "-", "*", "/", "(", ")"};

    public static List<string> Tokenize(string expression)
    {
        List<string> tokens = new List<string>();
        StringBuilder currentToken = new StringBuilder();

        foreach (char c in expression)
        {
            if (operators.Contains(c))
            {
                if (currentToken.Length > 0)
                {
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear(); []
                }
                tokens.Add(c.ToString());
            }

            else if (!char.IsWhiteSpace(c))
            {
                currentToken.Append(c);
            }
        }
        if (currentToken.Length > 0)
        {
            tokens.Add(currentToken.ToString());   
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
    };

    public static int GetPriority(char op)
    {
        if (priority.TryGetValue(op, out int priorityValue))
        {
            return priorityValue;
        }

        throw new ArgumentException($"Invalid operator!")
    }

}


public class ToRPN
{
    private Stack<char> stack;
    private List<string> result;

    public ToRPN()
    {
        stack = new Stack<char>();
        result = new List<string>();
    }
    public List<string> Rpnizer(List<string> tokens)
    {
        foreach (var token in tokens)
            {
            if (int.TryParse(token, out _))
                {
                result.Add(token);
                }
            else if (Tokenizer.operators.Contains(token[0]))
            {
                char operatorChar = token[0];
                while (stack.Count > 0 && OperatorsPriority.GetPriority(stack.Peek()) > OperatorsPriority.GetPriority(operatorChar))
                    {
                    result.Add(stack.Pop().ToString();
                    }
                stack.Push(operatorChar);
            }

            else if (token == "(")
            {
                stack.Push(token);
            }

            else if (token == ")")
            {
                 while (stack.Peek() != "(" && stack.Count > 0)
                {
                    result.Add(stack.Pop().ToString());
                }

                 if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop().ToString())
        }

        return result;
    }
}

// evaluate RPN
public class Evaluator
{
    private Stack<int> newStack = new Stack<int>();

    public int Calculator(List<string> result)
    {
        foreach (var ch in result)
        {
            if (!Tokenizer.operators.Contains(ch))
                
                newStack.Push(int.Parse(ch.ToString()));

            else
              
            {
                int right = stack.Pop();
                int left = stack.Pop();

                switch(ch)
                {
                    case '+':
                        stack.Push(left + right);
                        break;
                    case '-':
                        stack.Push(left - right);
                        break;
                    case '*':
                        stack.Push(left * right);
                        break;
                    case '/':
                        if (right == 0)
                            throw new InvalidOperationException("Cannot divide by zero!");

                        stack.Push(left / right);
                        break;
                    default:
                        throw new InvalidOperationException("The operation is not supported!");
                }
                    
            }

        }

        return stack.Pop();
    }
}

// calculator in work
public class TryCalculator
{
    public static void Main()
    {
        Console.WriteLine("Type your math expression >>");
        string expression = Console.ReadLine();

        var modified = ToRPN.Rpnizer(Tokenizer.Tokenize(expression));
        int result = Evaluator.Calculator(modified);
        Console.WriteLine($"Your math expression is equal to {result}");
    }

}