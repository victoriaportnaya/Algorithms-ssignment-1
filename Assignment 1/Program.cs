// get expression 

// implement stack 
public class Stack 
{
    private int top = 0;
    private int size;
    private int[] stack;

    public Stack(int size = 15)
    {
        this.size = size;
        stack = new int[size];
    }

    public bool IsEmpty()
    {
        if (top == 0)
            return true;
        else
            return false;
    }

    public void Push(int element)
    {
        if (top > size)
            throw new Exception("Stack Overflow");
        stack[top] = element;
        top++;
    }

    public int Pop()
    {
        if (IsEmpty())
            throw new Exception("Stack Overflow");
        else
        {
            top--;
            return stack[top];
        }
    }
}
// implement quue 
public class Queue<T>
{
    private Node head;
    private Node tail;

    public void Enqueue(T element)
    {
        var temp = new Node(element);

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

        var temp = head.Element;
        head = (Node)head.Next;

        return temp;
    }
}
// to RPN 

// write result