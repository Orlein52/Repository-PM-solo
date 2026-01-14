using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class Node
{
    public int data;
    public Node next;
    public Node prev;
    public Node maybe;
    public int nodeNum;
    bool top;
    public Node head;
    public int length;
    public int loops = 0;
    bool last;
    public Node(int num, int node)
    {
        data = num;
        next = null;
        maybe = null;
        nodeNum = node;
        last = true;
        if (nodeNum == 0)
        {
            top = true;
            length = 1;
            head = this;
        }
        else
        {
            top = false;
        }
    }
    public int Get(int node)
    {
        if (top)
        {
            loops++;
        }
        if (head.loops > 1)
        {
            if (node == nodeNum)
            {
                return data;
            }
            else if (last)
            {
                return 0;
            }
            else
            {
                return next.Get(node);
            }
        }
        else
        {
            return 0;
        }
    }
    public void Add(int num)
    {
        if (next == null)
        {
            next = new Node(num, (nodeNum + 1));
            next.prev = this;
            if (top)
            {
                next.head = this;
            }
            else
            {
                next.head = head;
            }
            head.length++;
            last = false;
        }
        else
        {
            next.Add(num);
        }
    }
    public void AddAt(int num, int node)
    {
        if (next.nodeNum == node)
        {
            maybe = next;
            next = new Node(num, (nodeNum + 1));
            next.prev = this;
            next.next = maybe;
            maybe.NodeUp();
            maybe = null;
            if (top)
            {
                next.head = this;
            }
            else
            {
                next.head = head;
            }
            head.length++;
        }
        else
        {
            next.AddAt(num, node);
        }
    }
    public void NodeUp()
    {
        nodeNum++;
        if (next != null)
        {
            next.NodeUp();
        }
    }
    public void Remove()
    {
        if (next.next == null)
        {
            next.maybe = null;
            next.head = null;
            next.prev = null;
            maybe = null;
            head.length--;
        }
        else
        {
            next.Remove();
        }
    }
    public void RemoveAt(int node)
    {
        if (next.nodeNum == node)
        {
            maybe = next.next;
            next.next = null;
            next.maybe = null;
            next = maybe;
            maybe = null;
            head.length--;
            next.NodeDown();
        }
        else
        {
            next.RemoveAt(node);
        }
    }
    public void NodeDown()
    {
        nodeNum--;
        if (next != null)
        {
            next.NodeDown();
        }
    }
}
