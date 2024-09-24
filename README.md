# Programming II semester project

This is a semester programming project for the Programming II (NPRG031) course at Charles University.
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

* `Count` - returns an Int32 indicating the current number of nodes present in the tree
  * example use: `int countOfNodes = tree.Count`
* `Delete(value)` - deletes a node with given value from the tree, return true if successful, false if no node with given value exists
  * example use: `tree.Delete(8)`
* `DFSInorder()`, `DFSPreorder()`, `DFSPostorder()` - IEnumerable, which can be used in a e.g. in a foreach loop, traversing the tree in either pre-order, post-order or in-order depth first search traversal, yields node objects
  * example use: `foreach (var value in tree.DFSPreOrder()) ...`
* `BFS()` - same as DFS functions, uses breadth first search
  * example use: `foreach (var value in tree.BFS) ...`
* `Clone()` - Creates a deep 1:1 copy of the tree.
  * example use: `AVLTree<int> treeCopy = tree.Clone()`
* `Find(value)` - returns a node with given value, null if no such node exists in the tree
  * example use: `Node<int> node = tree.Find(5)`
* `FindMax()` - returns the largest (rightmost) node in the tree, null if tree is empty
* `FindMin()` - returns the smallest (leftmost) node in the tree, null if tree is empty
* `GetBalance(node object)` - returns a balance of given node - height of right subtree - height of left subtree
  * example use: `int balance = tree.GetBalance(tree.find(3))`
* `GetNodesInRange(low, high)` - returns a System.Collections.Generic.List<T> list of nodes with values in given interval, low is the start of the interval, high is the end
  * example use: `var listOfNodes = tree.GetNodesInRange(3,13)`
* `InRange(low, high)` -  returns a number of nodes, which have a value in a given closed interval, low and high same as in `GetNodesInRange`
  * example use: `int 4to12 = tree.InRange(4,12)`
* `Insert(value)` - inserts a new node into tree, returns true if node was inserted, false if node with same value was already present
  * example use: `tree.Insert(12)`
* `Merge(tree object)` - merges another tree into this one
  * example use: `tree.Merge(anotherTree)`
* `Next()` - returns a successor (smallest larger node) to a node with given value, returns null if node with given value wasn't found or the node is the largest node in the tree
  * example use: `Node<int> next = tree.Next(4)`
* `RootValue()` - returns the value of root node
  * example use: `int rootVal = tree.RootValue()`
* `ToString()` - converts the tree to a string representation
  * example use: `string treeString = tree.ToString()`
* `Validate()` - checks the validity of the tree by going through all nodes and verifying they adhere to AVL properties. Useful for testing and debugging.
  * example use: `bool valid = Tree.Validate()`

## Information for programmers

If you wish to modify or extend anything in this library, I'll try to provide some useful information to make it easier.

## Object design

The library consists of two classes: a `Node<T>` class and a `AVLTree<T>` class. The Node class is responsible for saving
data and links to successors. It has two constructors, one for creating a new node with a value and no children and one 
for creating node while setting its value and both successors. 
The AVLTree class implements all the tree operations. 

### Encapsulation

The AVLTree class provides public functions that serve as an interface to the user. Those functions usually check if
the tree is empty – no work has to be done in that case, an only if it isn't, they call same-named overloaded functions,
which do the actual job. They usually take in a Node parameter indicating where the function should start – if a user
calls it, it's obvious that it should be the root, but often it's necessary to start in a certain subtree. These
functions are kept private, so the user can't interfere with the internal processes of AVLTree. 

For the Node class, value of a Node object is not changeable – has a private setter. Changing a Node's value could lead
to breaking the BST condition. Height and child nodes can only be changed by this library, never by user.

### Nodes

As long as the Node class contains the original properties, it's possible to add more 'data' properties to it so it can hold more
useful data. The node is always sorted based on the `Value` property.

### Validity

When implementing any structure changing functions, make use of the `Validate()` function. It checks the whole tree for
balance and that it is a proper binary search tree. This function should ideally return true 100% of time. If it doesn't,
start debugging.

### Unit tests

The library provides many unit tests that check individual functions and if they return expected results. Tests are also
run with each git push to the repository.

#### Tests

* `TestBalance` - test if inserting into the tree maintains balance
* `TestBalance2` - same as previous
* `TestClone` - test if cloned tree is equal to the original tree
* `TestCount` - test if nodes are counted correctly
* `TestDelete` - test if the Delete function really deletes the nodes, and doesn't delete anything when value not present in tree
* `TestDeleteBalance` - test if deleting nodes doesn't mess up tree balance
* `TestDeleteRoot` - test if trying to delete the root doesn't cause problems
* `TestEmptyInsertion` - test if inserting into empty tree is handled correctly
* `TestEmptyTree` - test if an exception is thrown when trying to access a root value of an empty tree
* `TestFind` - test if the Find functions returns a correct value
* `TestFindMax` - test if the FindMax functions returns a correct value 
* `TestFindMin` - test if the FindMin functions returns a correct value
* `TestFindNull` - test if trying to find a value not in tree returns false
* `TestInOrder` - test DFS in-order traversal
* `TestInRange` - tests the InRange(low, high) function
* `TestInRange2` - test the InRange function after multiple insertions and deletions
* `TestLargerThanCount` - test if the CountLargerThan function returns a correct value
  * `TestLargerThanCountMaximumNode - test if the CountLargerThan function returns a correct value when asked to find the
  max value in the tree
* `TestMerge` - tests if merging two trees results in a correct tree
* `TestNext` - test if the Next function returns a correct value
* `TestNextOfMaximum` - tests if trying to find a successor to a maximum value returns null
* `TestRepeatedInsert` - tests if repeated insertion doesn't insert the value twice
* `TestRotations` - test if repeated insertions maintain balance of the tree
* `TestRotationsWithStrings` - test if repeated string insertions maintain balance of the tree
* `TestSimpleInsert` - test if a very simple insertion is handled correctly
* `TestSmallerThanCount` -- test is the CountSmallerThan(low, high) function returns a correct result
* `TestToString` - test if the tree is converted to a correct string
