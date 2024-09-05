namespace AVL;

public class AVLTree<T>() where T : IComparable
{
    public Node<T>? Root { get; private set;  }

    public T RootValue()
    {
        if (Root is not null)
            return Root.Value;
        throw new Exception("tree is empty, cannot access root value");
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private Node<T> Insert(Node<T>? node, T value)
    // recursively insert nodes while mainaining balance
    {
        if (node is null)
            return new Node<T>(value);
        if (value.CompareTo(node.Value) < 0)
            node.Left = Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);
        else return node;

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        int balance = GetBalance(node);


        return node;
    }

    public int GetBalance(Node<T> node)
    {
        return Height(node.Left) - Height(node.Right);
    }

    private int Height(Node<T>? node)
    {
        // null node has height of 0
        return node?.Height ?? 0;
    }

}
