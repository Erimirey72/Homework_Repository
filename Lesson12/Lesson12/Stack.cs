public class Stack<T>
{
    private T[] items;
    private int lastElement;

    public Stack()
    {
        items = new T[10];
        lastElement = -1;
    }

    public void Push(T obj)
    {
        if (lastElement == items.Length - 1)
        {
            Array.Resize(ref items, items.Length * 2);
        }

        items[++lastElement] = obj;
    }

    public T Pop()
    {
        if (lastElement == -1)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        T item = items[lastElement];
        items[lastElement--] = default(T);
        return item;
    }

    public void Clear()
    {
        Array.Clear(items, 0, lastElement + 1);
        lastElement = -1;
    }

    public int Count
    {
        get { return lastElement + 1; }
    }

    public T Peek()
    {
        if (lastElement == -1)
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        return items[lastElement];
    }

    public void CopyTo(T[] array)
    {
        Array.Copy(items, 0, array, 0, lastElement + 1);
    }
}
