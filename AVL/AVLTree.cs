namespace AVL;

public class AVLTree<T> where T : System.IComparable
{
    public Node<T>? Root { get; private set; }
    public int Count { get; private set; }


    /// <summary>
    /// Returns the value of root node.
    /// </summary>
    /// <returns>
    /// Generic T value of root node, null if root doesn't exist.
    /// </returns>
    /// <exception cref="RootValue">
    /// Throws NullReferenceException if root is null.
    /// </exception>
    public T RootValue()
    {
        if (Root is not null)
            return Root.Value;
        throw new System.NullReferenceException("tree is empty, cannot access root value");
    }

    /// <summary>
    /// Inserts a new node into the AVL tree while keeping it balanced. Returns a boolean indicating whether the node
    /// was actually inserted.
    /// </summary>
    /// <returns>Bool: True if node was inserted. False if node with same value was already present.</returns>
    public bool Insert(T value)
    {
        int originalCount = Count;
        Root = Insert(Root, value);
        if (originalCount < Count)
            return true;
        return false;
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
    /// <summary>
    /// Calculates the balance of given node = Height(left successor) - Height(right successor).
    /// </summary>
    /// <param name="node"> Node in the tree of which the balance will be calculated. </param>
    /// <returns>
    /// Integer = Height(left successor) - Height(right successor). If the tree is balanced, it should
    /// fall between -1 and 1.
    /// </returns>
    public int GetBalance(Node<T> node)
    {
        return Height(node.Left) - Height(node.Right);
    }

    private int Height(Node<T>? node)
    {
        // null node has height of 0
        return node?.Height ?? 0;
    }
    /// <summary>
    /// Finds and returns a node in the tree using binary search. Thanks to the fact that the tree is balanced, function
    /// takes O(log(N)) time where N is the count of nodes in tree. Returns null if node with given value is not found.
    /// </summary>
    /// <param name="value">Value of the node function will try to find.</param>
    /// <returns>Node with given value, null if no such node found.</returns>
    public Node<T>? Find(T value)
    // return a node with given value, return null if not found
    {
        return Find(Root, value);
    }

    private Node<T>? Find(Node<T>? node, T value)
    // Travesrses down the tree using binary search until a node with correct value is found. Can start in any node.
    // Returns null if node is not found.
    {
        while (node is not null && value.CompareTo(node.Value) != 0)
        {
            node = value.CompareTo(node.Value) < 0 ? node.Left : node.Right;
        }
        return node;
    }

    public Node<T>? FindMax()
    {
        //
        Node<T>? node = Root;
        if (node is null)
            return null;
        return FindMax(node);
    }

    public Node<T>? FindMin()
    {
        Node<T>? node = Root;
        if (node is null)
            return null;
        return FindMin(node);
    }

    private Node<T> FindMin(Node<T> node)
        // find the smallest node in given subtree
    {
        while (node.Left is not null)
            node = node.Left;
        return node;
    }

    private Node<T> FindMax(Node<T> node)
        // find the largest node in given subtree
    {
        while (node.Right is not null)
            node = node.Right;
        return node;
    }

}
