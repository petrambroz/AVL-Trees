namespace AVL;

public class AVLTree<T>() where T : System.IComparable
{
    public Node<T>? Root { get; private set; }
    public int Count { get; private set; } = 0;


    public T RootValue()
    {
        if (Root is not null)
            return Root.Value;
        throw new System.Exception("tree is empty, cannot access root value");
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private Node<T> Insert(Node<T>? node, T value)
    // recursively insert nodes while mainaining balance
    {
        if (node is null)
        {
            Count++;
            return new Node<T>(value);
        }
        if (value.CompareTo(node.Value) < 0)
            node.Left = Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);
        else return node;

        node.Height = 1 + System.Math.Max(Height(node.Left), Height(node.Right));
        return BalanceNode(node, value);
    }

    private Node<T> BalanceNode(Node<T> node, T value)
    {
        int balance = GetBalance(node);
        if (balance > 1 && value.CompareTo(node.Left.Value) < 0)
            return RotateRight(node);

        if (balance < -1 && value.CompareTo(node.Right.Value) > 0)
            return RotateLeft(node);

        if (balance > 1 && value.CompareTo(node.Left.Value) > 0)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balance < -1 && value.CompareTo(node.Right.Value) < 0)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }
    private Node<T> RotateLeft(Node<T> nodeX)
    {
        Node<T> nodeY = nodeX.Right!;
        Node<T>? alpha = nodeY.Left;

        nodeY.Left = nodeX;
        nodeX.Right = alpha;

        nodeX.Height = System.Math.Max(Height(nodeX.Left), Height(nodeX.Right)) + 1;
        nodeY.Height = System.Math.Max(Height(nodeY.Left), Height(nodeY.Right)) + 1;

        return nodeY;
    }

    private Node<T> RotateRight(Node<T> nodeY)
    {
        Node<T> nodeX = nodeY.Left!;
        Node<T>? alpha = nodeX.Right;

        nodeX.Right = nodeY;
        nodeY.Left = alpha;

        nodeY.Height = System.Math.Max(Height(nodeY.Left), Height(nodeY.Right)) + 1;
        nodeX.Height = System.Math.Max(Height(nodeX.Left), Height(nodeX.Right)) + 1;

        return nodeX;
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

    public Node<T>? Find(T value)
    // return a node with given value, return null if not found
    {
        return Find(Root, value);
    }

    private Node<T>? Find(Node<T>? node, T value)
    {
        while (node is not null && value.CompareTo(node.Value) != 0)
        {
            node = value.CompareTo(node.Value) < 0 ? node.Left : node.Right;
        }
        return node;
    }

}
