namespace AVL;
/// <summary>
/// This is a library providing a generic-type implementation of AVL trees.
/// </summary>
/// <typeparam name="T">
/// Specifies the data type that the nodes save. Must be derived from System.IComparable
/// </typeparam>
public class AVLTree<T> where T : System.IComparable<T>
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

    private enum TraversalOrder
    {
        InOrder,
        PreOrder,
        PostOrder
    }

    /// <summary>
    /// Performs a depth-first in-order traversal of the AVL tree. This traversal returns nodes in ascending order.
    /// </summary>
    /// <returns>
    /// An enumerable collection of nodes in in-order sequence.
    /// </returns>
    public System.Collections.Generic.IEnumerable<T> DFSInOrder()
    {
        return DFS(TraversalOrder.InOrder);
    }

    /// <summary>
    /// Performs a depth-first pre-order traversal of the AVL tree.
    /// In pre-order traversal, the nodes are recursively visited in the following order:
    /// </summary>
    /// <returns>
    /// An enumerable collection of nodes in pre-order sequence.
    /// </returns>
    public System.Collections.Generic.IEnumerable<T> DFSPreOrder()
    {
        return DFS(TraversalOrder.PreOrder);
    }

    /// <summary>
    /// Performs a depth-first post-order traversal of the AVL tree.
    /// In post-order traversal, the nodes are recursively visited in the following order:
    /// </summary>
    /// <returns>
    /// An enumerable collection of nodes in post-order sequence.
    /// </returns>
    public System.Collections.Generic.IEnumerable<T> DFSPostOrder()
    {
        return DFS(TraversalOrder.PostOrder);
    }

    /// <summary>
    /// Performs a Breadth-First Search (BFS) traversal of the tree.
    /// </summary>
    /// <returns>Enumerable containing values of the nodes in BFS order.</returns>
    public System.Collections.Generic.IEnumerable<T> BFS()
    {
        if (Root is null)
            yield break;

        System.Collections.Generic.Queue<Node<T>> queue = new System.Collections.Generic.Queue<Node<T>>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            Node<T> current = queue.Dequeue();
            yield return current.Value;

            if (current.Left is not null)
                queue.Enqueue(current.Left);
            if (current.Right is not null)
                queue.Enqueue(current.Right);
        }
    }

    private System.Collections.Generic.IEnumerable<T> DFS(TraversalOrder order)
    {
        return DFS(Root, order);
    }

    private System.Collections.Generic.IEnumerable<T> DFS(Node<T>? node, TraversalOrder order)
    {
        if (node is null)
            yield break;

        if (order == TraversalOrder.PreOrder)
            yield return node.Value;

        foreach (var value in DFS(node.Left, order))
            yield return value;

        if (order == TraversalOrder.InOrder)
            yield return node.Value;

        foreach (var value in DFS(node.Right, order))
            yield return value;

        if (order == TraversalOrder.PostOrder)
            yield return node.Value;
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
    {
        // recursively insert nodes while mainaining balance
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
        return BalanceNode(node);
    }

    private Node<T> BalanceNode(Node<T> node)
    {
        int balance = GetBalance(node);
        if (balance > 1)
        {
            if (node.Left is not null &&  GetBalance(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        if (balance < -1)
        {
            if (node.Right is not null && GetBalance(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private Node<T> RotateLeft(Node<T> nodeX)
    {
        if (nodeX.Right is null)
            throw new System.InvalidOperationException("Can't perform left rotation on a node without right child");
        Node<T> nodeY = nodeX.Right;
        Node<T>? alpha = nodeY.Left;

        nodeY.Left = nodeX;
        nodeX.Right = alpha;

        nodeX.Height = System.Math.Max(Height(nodeX.Left), Height(nodeX.Right)) + 1;
        nodeY.Height = System.Math.Max(Height(nodeY.Left), Height(nodeY.Right)) + 1;

        return nodeY;
    }

    private Node<T> RotateRight(Node<T> nodeY)
    {
        if (nodeY.Left is null)
            throw new System.InvalidOperationException("Can't perform right rotation on a node without left child");
        Node<T> nodeX = nodeY.Left;
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
    /// takes O(H) time where H is the height of the tree. Returns null if node with given value is not found.
    /// </summary>
    /// <param name="value">Value of the node function will try to find.</param>
    /// <returns>Node with given value, null if no such node found.</returns>
    public Node<T>? Find(T value)
    {
        return Find(Root, value);
    }

    private Node<T>? Find(Node<T>? node, T value)
    {
        // Travesrses down the tree using binary search until a node with correct value is found. Can start in any node.
        // Returns null if node is not found.
        while (node is not null && value.CompareTo(node.Value) != 0)
        {
            node = value.CompareTo(node.Value) < 0 ? node.Left : node.Right;
        }

        return node;
    }

    /// <summary>
    /// Finds and returns the largest (rightmost) node in the tree.
    /// </summary>
    /// <returns>The largest node in tree. Null if tree is empty.</returns>
    public Node<T>? FindMax()
    {
        Node<T>? node = Root;
        if (node is null)
            return null;
        return FindMax(node);
    }

    /// <summary>
    /// Finds and returns the smallest (leftmost) node in the tree.
    /// </summary>
    /// <returns>The smallest node in tree. Null if tree is empty.</returns>
    public Node<T>? FindMin()
    {
        Node<T>? node = Root;
        if (node is null)
            return null;
        return FindMin(node);
    }

    private Node<T> FindMin(Node<T> node)
    {
        // find the smallest node in given subtree
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

    /// <summary>
    /// Deletes a node with given value from the tree. If there's no such node in the tree, nothing is deleted.
    /// </summary>
    /// <param name="value">Value of the node that should be deleted.</param>
    /// <returns>True if a node was deleted, false otherwise. </returns>
    public bool Delete(T value)
    {
        bool deleted = false;

        if (Root is not null)
            Root = Delete(Root, value, ref deleted);
        return deleted;
    }

    private Node<T>? Delete(Node<T>? node, T value, ref bool deleted)
    {
        if (node is null)
            return null;
        if (value.CompareTo(node.Value) < 0)
            node.Left = Delete(node.Left, value, ref deleted);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Delete(node.Right, value, ref deleted);
        else
        {
            if (node.Left is null || node.Right is null)
            {
                Node<T>? next = (node.Left ?? node.Right)!;
                node = next ?? null;
                Count--;
                deleted = true;
            }
            else
            {
                Node<T> temp = FindMin(node.Right);
                node = new Node<T>(temp.Value, node.Left, node.Right);
                node.Right = Delete(node.Right, temp.Value, ref deleted);
            }
        }

        if (node is not null)
        {
            node.Height = 1 + System.Math.Max(Height(node.Left), Height(node.Right));
            return BalanceNode(node);
        }

        return null;
    }

    /// <summary>
    /// Returns an in-order successor of a node with given value, i.e. the smallest larger node.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>An in-order successor node. Null if no larger node exists or initial value not found in tree</returns>
    public Node<T>? Next(T value)
    {
        // finds a succesor to a node with given value, return null if doesn't exist
        Node<T>? node = Find(value);
        if (node is null)
            // value not found, can't have a successor
            return null;
        if (node.Right is not null)
            // the successor is certainly in the right subtree
        {
            return FindMin(node.Right);
        }

        if (Root is null)
            return null;
        Node<T>? successor = null;
        Node<T>? ancestor = Root;

        while (ancestor is not null)
        {
            if (value.CompareTo(ancestor.Value) < 0)
            {
                successor = ancestor;
                ancestor = ancestor.Left;
            }
            else if (value.CompareTo(ancestor.Value) > 0)
                ancestor = ancestor.Right;
            else break;
        }

        return successor;
    }

    /// <summary>
    /// Returns a number of nodes, which have a value in a given closed interval.
    /// </summary>
    /// <param name="low">Lower endpoint of the interval.</param>
    /// <param name="high">Upper endpoint of the interval.</param>
    /// <returns>Number of nodes in the given interval.</returns>
    public int InRange(T low, T high)
    {
        if (Root is not null)
            return InRange(Root, low, high);
        return 0;
    }

    private int InRange(Node<T>? node, T low, T high)
    {
        // returns a count of nodes in both subtrees that have a value inside given interval
        if (node is null)
            return 0;
        if (node.Value.CompareTo(low) >= 0 && node.Value.CompareTo(high) <= 0)
            return 1 + InRange(node.Left, low, high) + InRange(node.Right, low, high);
        if (node.Value.CompareTo(low) < 0)
            return InRange(node.Right, low, high);
        return InRange(node.Left, low, high);
    }

    /// <summary>
    /// Converts the tree to a string representation.
    /// </summary>
    /// <returns>String representation of the tree in in-order traversal.</returns>
    public override string ToString()
    {
        var result = new System.Text.StringBuilder();
        foreach (var value in DFSInOrder())
        {
            result.Append(value);
            result.Append(' ');
        }
        return result.ToString();
    }

    /// <summary>
    /// Validates whether the tree is correctly balanced and adheres to AVL properties.
    /// </summary>
    /// <returns>True if the tree is valid, false otherwise.</returns>
    public bool Validate()
    {
        return Validate(Root);
    }

    private bool Validate(Node<T>? node)
    {
        if (node is null)
            return true;

        int balance = GetBalance(node);
        if (balance < -1 || balance > 1)
            return false;
        if (node.Left?.Value.CompareTo(node.Value) >= 0 || node.Right?.Value.CompareTo(node.Value) <= 0)
            return false;

        return Validate(node.Left) && Validate(node.Right);
    }

    /// <summary>
    /// Creates a deep 1:1 copy of the tree.
    /// </summary>
    /// <returns>An AVLTree object.</returns>
    public AVLTree<T> Clone()
    {
        AVLTree<T> clonedTree = new AVLTree<T>();
        clonedTree.Root = CloneNode(Root);
        clonedTree.Count = Count;
        return clonedTree;
    }

    private Node<T>? CloneNode(Node<T>? node)
    {
        if (node is null)
            return null;

        Node<T> clonedNode = new Node<T>(node.Value)
        {
            Left = CloneNode(node.Left),
            Right = CloneNode(node.Right),
            Height = node.Height
        };
        return clonedNode;
    }

    /// <summary>
    /// Merges another tree into this one.
    /// </summary>
    /// <param name="otherTree">Another AVLTree object. Must be using same data type.</param>
    public void Merge(AVLTree<T> otherTree)
    {
        if (otherTree.Root is null)
            return;

        foreach (var value in otherTree.DFSInOrder())
            Insert(value);
    }

    /// <summary>
    /// Creates a list of nodes with values in given interval.
    /// </summary>
    /// <param name="low">Lower endpoint of the interval.</param>
    /// <param name="high">Upper endpoint of the interval.</param>
    /// <returns>List of nodes with matching values.</returns>
    public System.Collections.Generic.List<T> GetNodesInRange(T low, T high)
    {
        System.Collections.Generic.List<T> nodes = new System.Collections.Generic.List<T>();
        GetNodesInRange(Root, low, high, nodes);
        return nodes;
    }

    private void GetNodesInRange(Node<T>? node, T low, T high, System.Collections.Generic.List<T> result)
    {
        if (node is null)
            return;

        if (node.Value.CompareTo(low) > 0)
            GetNodesInRange(node.Left, low, high, result);

        if (node.Value.CompareTo(low) >= 0 && node.Value.CompareTo(high) <= 0)
            result.Add(node.Value);

        if (node.Value.CompareTo(high) < 0)
            GetNodesInRange(node.Right, low, high, result);
    }

    /// <summary>
    /// Counts the number of nodes in the AVL tree that have a value larger than the specified value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The number of nodes with a value larger than the this value.</returns>
    public int CountLargerThan(T value)
    {
        return CountLargerThan(Root, value);
    }
    private int CountLargerThan(Node<T>? node, T value)
    {
        while (node is not null)
        {
            if (node.Value.CompareTo(value) > 0)
                return 1 + CountLargerThan(node.Left, value) + CountLargerThan(node.Right, value);
            node = node.Right;
        }
        return 0;
    }

    /// <summary>
    /// Counts the number of nodes in the AVL tree that have a value smaller than the specified value.
    /// </summary>
    /// <param name="value">The value to compare against.</param>
    /// <returns>The number of nodes with a value smaller than the this value.</returns>
    public int CountSmallerThan(T value)
    {
        return CountSmallerThan(Root, value);
    }

    private int CountSmallerThan(Node<T>? node, T value)
    {
        while (node is not null)
        {
            if (node.Value.CompareTo(value) < 0)
                return 1 + CountSmallerThan(node.Left, value) + CountSmallerThan(node.Right, value);
            node = node.Left;
        }
        return 0;
    }
}
