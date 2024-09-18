namespace AVL;


public class Node<T>
{
    public T Value { get; private set; }
    public Node<T>? Left;
    public Node<T>? Right;
    internal int Height;

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
