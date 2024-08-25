using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.Common.DataStructures
{
    public class CustomQueue<T>
    {
        private class Node
        {
            public T Data;
            public Node Next;
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public CustomQueue()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Enqueue(T item)
        {
            Node newNode = new Node(item);
            if (tail != null)
            {
                tail.Next = newNode;
            }
            tail = newNode;
            if (head == null)
            {
                head = tail;
            }
            count++;
        }

        public T Dequeue()
        {
            if (head == null) throw new InvalidOperationException("Queue is empty.");
            T value = head.Data;
            head = head.Next;
            if (head == null)
            {
                tail = null;
            }
            count--;
            return value;
        }

        public T Peek()
        {
            if (head == null) throw new InvalidOperationException("Queue is empty.");
            return head.Data;
        }

        public int Count => count;
    }

}
