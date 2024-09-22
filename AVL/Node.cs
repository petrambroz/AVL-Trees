namespace AVL;


public class Node<T> where T : System.IComparable<T>
{
    public T Value { get; private set; }
    public Node<T>? Left { get; internal set; }
    public Node<T>? Right { get; internal set; }
    public int Height { get; internal set; }

    public Node(T value)
    {
        Value = value;
        Height = 1;
    }

    public Node(T value, Node<T>? left, Node<T>? right)
    {
        Value = value;
        Height = 1;
        Left = left;
        Right = right;
    }
}
