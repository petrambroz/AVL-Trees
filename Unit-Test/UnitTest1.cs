namespace Unit_Test;

using AVL;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    // Many nullable warnings are intentionally supressed with the '!' mark, because only non-problematic values are
    // used in possibly null-returning functions.
    [Test]
    public void TestEmptyInsertion()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        Assert.That(tree.RootValue(), Is.EqualTo(5));
    }

    [Test]
    public void TestEmptyTree()
    //  test if an exception is thrown when trying to access a root value of an empty tree
    {
        AVLTree<int> tree = new AVLTree<int>();
        Assert.Throws<System.NullReferenceException>(MethodThatThrows);
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
        Assert.That(tree.Root!.Left!.Value, Is.EqualTo(4));
        Assert.That(tree.Root.Right!.Value, Is.EqualTo(9));
    }

    [Test]
    public void TestRepeatedInsert()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(8);
        bool inserted = tree.Insert(4);
        bool notInserted = tree.Insert(5);
        Assert.That(inserted, Is.True);
        Assert.That(notInserted, Is.False);
    }

    [Test]
    public void TestBalance()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.GetBalance(tree.Root!), Is.EqualTo(0));
    }

    [Test]
    public void TestBalance2()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(3);
        tree.Insert(-2);
        Assert.That(tree.GetBalance(tree.Root!), Is.EqualTo(1));
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
        Assert.That(tree.Find(-2)!.Value, Is.EqualTo(-2));
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

    [Test]
    public void TestFindMax()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(21);
        tree.Insert(-2);
        tree.Delete(2);
        Node<int>? maximum = tree.FindMax();
        Assert.That(maximum, Is.Not.Null);
        Assert.That(maximum.Value, Is.EqualTo(22));
    }

    [Test]
    public void TestFindMin()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Node<int>? maximum = tree.FindMin();
        Assert.That(maximum, Is.Not.Null);
        Assert.That(maximum.Value, Is.EqualTo(-5));
    }

    [Test]
    public void TestRotations()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.GetBalance(tree.Root!), Is.InRange(-1,1));
    }

    [Test]
    public void TestRotationsWithStrings()
    {
        AVLTree<string> tree = new AVLTree<string>();
        tree.Insert("ahoj");
        tree.Insert("cauky");
        tree.Insert("nazdarek");
        tree.Insert("kocka");
        tree.Insert("slepice");
        tree.Insert("linoleum");
        Assert.That(tree.GetBalance(tree.Root!), Is.InRange(-1,1));
    }

    [Test]
    public void TestCount()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(9);
        tree.Delete(3);
        tree.Insert(12);
        tree.Insert(-4);
        tree.Insert(0);
        tree.Delete(1);
        Assert.That(tree.Count, Is.EqualTo(5));
    }

    [Test]
    public void TestDelete()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        bool deleted = tree.Delete(1);
        bool notdeleted = tree.Delete(1);
        Assert.That(tree.Find(1), Is.Null);
        Assert.That(deleted, Is.True);
        Assert.That(notdeleted, Is.False);
    }

    [Test]
    public void TestDeleteBalance()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        tree.Delete(2);
        Assert.That(tree.GetBalance(tree.Root!), Is.InRange(-1, 1));
    }
    [Test]
    public void TestDeleteRoot()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        tree.Insert(15);
        Assert.That(tree.Root!.Value, Is.EqualTo(10));
        tree.Delete(10);
        Assert.That(tree.Root, Is.Not.Null);
        Assert.That(tree.GetBalance(tree.Root!), Is.InRange(-1, 1));
    }

    [Test]
    public void TestNext()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Assert.That(tree.Next(12)!.Value, Is.EqualTo(22));
    }

    [Test]
    public void TestNextOfMaximum()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(6);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(12);
        tree.Insert(-5);
        tree.Insert(22);
        tree.Insert(-2);
        tree.Delete(2);
        Assert.That(tree.Next(22), Is.Null);
    }

    [Test]
    public void TestInRange()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        tree.Delete(2);
        Assert.That(tree.InRange(1,3), Is.EqualTo(2));
    }

    [Test]
    public void TestInRange2()
    {
        AVLTree<int> tree = new AVLTree<int>();
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
        tree.Delete(2);
        tree.Delete(1);
        Assert.That(tree.InRange(0,12), Is.EqualTo(5));
    }

    [Test]
    public void TestInOrder()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>(); ;
        foreach(var value in tree.DFSInOrder())
            list.Add(value);
        Assert.That(list[0], Is.EqualTo(-5));
        Assert.That(list[1], Is.EqualTo(1));
    }

    [Test]
    public void TestToString()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        tree.Insert(9);
        Assert.That(tree.ToString(), Is.EqualTo("-5 1 2 3 4 9 "));
    }

    [Test]
    public void TestClone()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        AVLTree<int> clonedTree = tree.Clone();
        Assert.That(tree.ToString(), Is.EqualTo(clonedTree.ToString()));
    }

    [Test]
    public void TestMerge()
    {
        AVLTree<int> tree = new AVLTree<int>();
        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
        tree.Insert(-5);
        tree.Insert(4);
        AVLTree<int> tree2 = new AVLTree<int>();
        tree2.Insert(9);
        tree2.Insert(-1);
        tree.Merge(tree2);
        Assert.That(tree.ToString(), Is.EqualTo("-5 -1 1 2 3 4 9 "));
    }
}
