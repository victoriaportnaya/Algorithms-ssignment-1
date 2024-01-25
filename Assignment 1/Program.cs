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
    private Node head;
    private Node tail;

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
            throw new Exception("Queue Is Empty")

        var temp = head.item;
        head = (Node)head.Next;

        return temp;
    }
}
// to RPN 

// write result