namespace Example;
using AVL;
class Program
{
    static void Main(string[] args)
    {
        AVLTree<int> tree = new AVLTree<int>();

        // Insert nodes into the AVL tree
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(12);
        tree.Insert(0);
        tree.Insert(56);
        tree.Insert(-3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);

        // DFS in-order traversal
        Console.WriteLine("\nIn-order traversal of the AVL tree:");
        foreach (var value in tree.DFSInOrder())
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();

        // Count nodes larger than 5
        int largerThan5 = tree.CountLargerThan(5);
        Console.WriteLine($"\nThere are {largerThan5} nodes larger than 5.");

        // Find the maximum and minimum nodes
        Node<int>? maxNode = tree.FindMax();
        Node<int>? minNode = tree.FindMin();
        Console.WriteLine($"\nLargest node: {maxNode?.Value}");
        Console.WriteLine($"Smallest node: {minNode?.Value}");

        // Delete a node
        Console.WriteLine("\nDeleting node with value 2:");
        bool deleted = tree.Delete(2);
        Console.WriteLine(deleted ? "Node with value 2 deleted." : "Node with value 2 not found.");

        // DFS in-order traversal after deleting
        Console.WriteLine("\nIn-order traversal of the AVL tree:");
        foreach (var value in tree.DFSInOrder())
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();

        // Find number of nodes between 1 and 15
        int nodesFrom1To15 = tree.InRange(1, 15);
        Console.WriteLine($"Number of nodes between 1 and 15: {nodesFrom1To15}");
    }
}
