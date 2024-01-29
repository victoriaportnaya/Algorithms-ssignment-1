using System.Collections;
using System.Collections.Generic;

// calculator in work
public class TryCalculator
{
    public static void Main()
    {
        Console.WriteLine("Type your math expression >>");
        string expression = Console.ReadLine();
        var tokens = Tokenizer.Tokenize(expression);
        var rpn = new ToRPN();
        var rpntokens = rpn.Rpnizer(tokens);

        var evaluator = new Evaluator();
        int result = evaluator.Calculate(rpntokens);

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
public class Queue<T>: IEnumerable<T>
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


    public Queue()
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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }


}
// tokenizer
public class Tokenizer
{
    public static HashSet<char> Operators = new HashSet<char> { '+', '-', '*', '/', ')', '(' };

    public static Queue<string> Tokenize(string expression)
    {
        Queue<string> tokens = new Queue<string>();
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
    private Queue<string> result;

    public ToRPN()
    {
        stack = new Stack<char>();
        result = new Queue<string>();
    }
    public Queue<string> Rpnizer(Queue<string> tokens)
    {
        foreach (var token in tokens)
        {
            if (int.TryParse(token, out _))
            {
                result.Enqueue(token);
            }
            else if (Tokenizer.Operators.Contains(token[0]))
            {
                char operatorChar = token[0];
                while (stack.Count > 0 && OperatorsPriority.GetPriority(stack.Peek()) > OperatorsPriority.GetPriority(operatorChar))
                {
                    result.Enqueue(stack.Pop().ToString());
                }
                stack.Push(operatorChar);
            }

            else if (token == "(")
            {
                stack.Push(token[0]);
            }

            else if (token == ")")
            {
                while (stack.Peek() != "(")
                {
                    result.Enqueue(stack.Pop().ToString());
                }

                if (!stack.IsEmpty())
                {
                    stack.Pop();
                }
            }
        }

        while (stack.Count > 0)
        {
            result.Enqueue(stack.Pop().ToString());
        }

        return result;
    }
}

// evaluate RPN
public class Evaluator
{
    private Stack<int> newStack;

    public Evaluator()
    {
        newStack = new Stack<int>();
    }

    public int Calculate(Queue<string> result)
    {
        foreach (var ch in result)
        {
            if (int.TryParse(ch, out int number))

                newStack.Push(number);

            else

            {
                int right = newStack.Pop();
                int left = newStack.Pop();

                switch (ch)
                {
                    case '+':
                        newStack.Push(left + right);
                        break;
                    case '-':
                        newStack.Push(left - right);
                        break;
                    case '*':
                        newStack.Push(left * right);
                        break;
                    case '/':
                        if (right == 0)
                            throw new InvalidOperationException("Cannot divide by zero!");

                        newStack.Push(left / right);
                        break;
                    default:
                        throw new InvalidOperationException("The operation is not supported!");
                }

            }

        }

        if (newStack.Count > 1)
            throw new InvalidOperationException("Something wrong with your expression!");

        return newStack.Pop();
    }
}

