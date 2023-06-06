public class Program
{
    static void Main()
    {
        Stack<int> stack = new Stack<int>();

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        Console.WriteLine("Stack Count: " + stack.Count);

        int poppedItem1 = stack.Pop();
        Console.WriteLine("Popped Item: " + poppedItem1);
        int poppedItem2 = stack.Pop();
        Console.WriteLine("Popped Item: " + poppedItem2);

        Console.WriteLine("Stack Count: " + stack.Count);

        int topItem = stack.Peek();
        Console.WriteLine("Top Item: " + topItem);

        stack.Clear();

        Console.WriteLine("Stack Count: " + stack.Count);
    }
}