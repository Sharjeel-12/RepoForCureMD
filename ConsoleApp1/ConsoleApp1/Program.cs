// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

// Code#1 for checking balanced Paranthesis

public class Program
{
    public static bool Isbalanced(string str)
    {

        Stack<char> myStack = new Stack<char>();
        foreach (char c in str)
        {
            if (c == '(')
            {
                myStack.Push(c);

            }
            else if (c == ')')
            {
                if (myStack.Count == 0)
                {
                    return false;
                }
                char top = myStack.Pop();
                if (c == ')' && top != '(')
                {
                    return false;
                }
            }

        }
        return myStack.Count == 0;
    }




    // Code#2  for reversing a Queue
    public static Queue<int> ReverseQueue(Queue<int> queue)
    {
        Stack<int> myStack = new Stack<int>();
        while (queue.Count > 0)
        {
            myStack.Push(queue.Dequeue());
        }
        while (myStack.Count > 0)
        {
            queue.Enqueue(myStack.Pop());
        }
        return queue;
    }

    public static int[] RotateLeft(int[] arr, int k)
    {
        List<int> myList_1 = new List<int>();
        List<int> myList_2 = arr.ToList();
        if (k <= arr.Length)
        {
            
            for (int i = 0; i<k; i++)
            {
                myList_1.Add(myList_2[i]);

            }
            myList_2.RemoveRange(0, k);
            myList_2.AddRange(myList_1);
        }
        else
        {
            Console.WriteLine("k is greater than the length of array which cannot be the case");
        }
        return myList_2.ToArray();
    }



}
// Code #3 
public class Node
{
    public int data;
    public Node Next;
    public Node(int value)
    {
        data = value;
        Next = null;
    }
}
public class MainClass
{
    static void Main()
    {
        // Problem No.1 Balanced Parenthesis
        Console.WriteLine("Problem 1 Solution: Checkinh Parenthesis\n");
        string str = "(a+b)*(a";
        Console.WriteLine(Program.Isbalanced(str));// retuen False
        string str_2 = "(a+b)*(a+c)";
        Console.WriteLine(Program.Isbalanced(str_2));// retuen True


        // Problem No.2 Reversing a Queue: -
        // Example: -
        Console.WriteLine("Problem 2 Solution: Reversing the Queue using stack\n");

        int[] arr = { 1, 6, 7, 8, 10, 12 };
        Queue<int> myqueue = new Queue<int>(arr);
        Queue<int> reversed = Program.ReverseQueue(myqueue);
        Console.WriteLine("Before Reversing the queue");
        foreach (int item in arr)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("After Reversing the queue");
        foreach (int item in reversed)
        {
            Console.WriteLine(item);
        }


        // Problem 3: Linked List
        // Manually Creating 3 Nodes
        Console.WriteLine("Problem 3 Solution: Manually Creating 3 Nodes\n");
        Node node_1 = new Node(10);
        Node node_2= new Node(20);
        Node node_3 = new Node(30);

        node_1.Next = node_2;
        node_2.Next = node_3;
        node_3.Next = null;

        // Looping over the linked list: -
        Console.WriteLine("Looping over the Linked List");
        Node current = node_1;
        while (current != null)
        {
            Console.WriteLine(current.data);
            current = current.Next;
        }


        // Problem No.4
        int k = 4;
        Console.WriteLine("Problem 4 solution: Left Rotating an array");
        int[] myArray = { 1, 6, 8, 9, 10, 12 };
        int[] rotated = Program.RotateLeft(myArray, k);
        
        Console.WriteLine("Before Rotation: -");
        foreach (int item in myArray)
        {
            Console.WriteLine(item);
        }
        
        Console.WriteLine($"After Rotation (with k= {k}): -");
        foreach(int item in rotated)
        {
            Console.WriteLine(item);
        }




    }
}
