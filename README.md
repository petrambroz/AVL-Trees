# Programming II semester project

This is a semsester programming project for the Programming II (NPRG031) subject at Charles University.
The topic of this project is AVL tree library.
The project is developed and maintained by Petr Ambrož.

## Brief introduction

The goal of this project is to provide a library implementing an AVL tree class in C#, which will allow its user to
create generic AVL trees. Generic implementation means, that it's up to the user to decide what data type will the nodes
store, while the only requirement is that the data type must be derived from System.IComparable interface. Binary search
trees (which are AVL trees based on) rely on comparing elements to decide which is 'larger' and which is 'smaller'.
Suitable data types are therefore integers, chars, strings and many more. Find further information about the IComparable
interface [here](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable?view=net-8.0) on Microsoft's official
C#/.NET documentation website.

## What are AVL trees?

AVL tree is an advanced version of a binary search tree (shortened to BST). The biggest downside to regular BST is the
worst-case depth is O(N) (N is the number of nodes) A perfectly balanced BST has a height of log(N). AVL trees don't
seek perfect balance, but they maintain a very important property: for each node, the difference in height of both of
its subtrees is at most 1. This way, AVL trees are able to maintain O(log N) depth at all times.  
This is achieved using rotations – swapping nodes in a way which doesn't break the binary search tree property, but 
changes the balance of a node.

## What this library provides

This library can be added to any C# project which uses .NET 8 runtime. Other versions were not tested, use at your risk.
Each method and function has an in-code XML documentation explaining its purpose and use.
Algorithms used in this project are from knowledge gained during my studies at Charles University, or from the book
*Průvodce labyrintem algoritmů* written by Martin Mareš, Tomáš Valla, published in 2022 by CZ.NIC, available 
[here](http://pruvodce.ucw.cz).

## Usage

### Creating an AVL tree

`AVLTree<T> tree = new AVLTree<T>();` creates a new empty AVL tree. `T` indicates the class is generic and the `T`
should be replaced by any data type derived from System.IComparable, such as `int`.  
`AVLTree<int> tree = new AVLTree<int>();` creates a tree with nodes storing `int`.

### Overview of public methods and functions
* Count() -- returns an Int32 indicating the current number of nodes present in the tree
* Delete(value) -- 
* DFSInorder, DFSPreorder, DFSPostorder -- IEnumerable, which can be used in a foreach loop, traversing the tree in either preorder, postorder or inorder DFS traversal, yields node objects
* Find(value) -- returns a node with given value, null if no such node exists in the tree
* FindMax() -- returns the largest (rightmost) node in the tree, null if tree is empty
* FindMin() -- returns the smallest (leftmost) node in the tree, null if tree is empty
* GetBalance(node object) -- returns a balance of given node -- height of right subtree - height of left subtree
* InRange(low, high) --  Returns a number of nodes, which have a value in a given closed interval, low is the start of the interval, high is the end
* Insert(value) -- inserts a new node into tree, returns true if node was inserted, false if node with same value was already present
* Next() -- returns a successor (smallest larger node) to a node with given value, returns null if node with given value wasn't found or the node is the largest node in the tree
* RootValue() -- returns the value of root node

### Insert(value)
