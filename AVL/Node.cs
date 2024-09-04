namespace AVL;


public class Node<T>
{
    public T Value;
    public Node<T>? Left;
    public Node<T>? Right;
    public int Height;

    public Node(T value)
    {
        Value = value;
        Height = 1;
    }
}
