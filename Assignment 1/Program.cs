using System;

class Calculator
{
    static void GetExpression()
    {
        Console.WriteLine("Type your math expression >>");
        string expression = Console.ReadLine();
    }

    // here stuff with calculator
}
// get expression 
Console.WriteLine("Enter your math expression >>");
string expression = Console.ReadLine();

// implement stack 
public class Stack<T>
{
    private int top = 0;
    private int size = 15;
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
// implement quue 
public class Queue<T>
{
    public T item;
    public Node next;

    public Node(T item)
    {
        this.item = item;
        this.next = null;
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
            throw new Exception("Nothing to dequeue!")
        T item = head.Item;
        head = head.Next;
        if (head == null)
            tail = null;

        return item;
    }

    public bool IsEmpty() => head == null;
}
// to RPN 

// write result