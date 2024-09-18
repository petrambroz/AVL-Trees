namespace Unit_Test;

using AVL;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEmptyInsertion()
    {
        const int testVal = 5;
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(testVal);
        Assert.That(tree.RootValue(), Is.EqualTo(testVal));
    }
    [Test]
    public void TestEmptyTree()
    //  test if an exception is thrown when trying to access a root value of an empty tree
    {
        AVLTree<int> tree = new AVLTree<int>();
        Assert.Throws<System.Exception>(MethodThatThrows);

        void MethodThatThrows()
        {
            tree.RootValue();
        }
    }
    [Test]
    public void TestSimpleInsert()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.Root.Left.Value, Is.EqualTo(4));
        Assert.That(tree.Root.Right.Value, Is.EqualTo(9));

    }

    [Test]
    public void TestBalance()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.GetBalance(tree.Root), Is.EqualTo(0));
    }
    [Test]
    public void TestBalance2()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(3);
        tree.Insert(-2);
        Assert.That(tree.GetBalance(tree.Root), Is.EqualTo(1));
    }

    [Test]
    public void TestFind()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(7);
        tree.Insert(3);
        tree.Insert(-2);
        tree.Insert(9);
        Assert.That(tree.Find(-2).Value, Is.EqualTo(-2));
    }

    [Test]
    public void TestFindNull()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(7);
        tree.Insert(3);
        tree.Insert(-2);
        tree.Insert(9);
        Assert.That(tree.Find(-1), Is.Null);
    }
}
