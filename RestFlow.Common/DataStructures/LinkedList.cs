using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow.Common.DataStructures
{
    public class CustomLinkedList<T>
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

        public CustomLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void AddLast(T data)
        {
            Node newNode = new Node(data);
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

        public void AddFirst(T data)
        {
            Node newNode = new Node(data);
            newNode.Next = head;
            head = newNode;
            if (tail == null)
            {
                tail = head;
            }
            count++;
        }

        public T RemoveFirst()
        {
            if (head == null) throw new InvalidOperationException("List is empty.");
            T value = head.Data;
            head = head.Next;
            if (head == null)
            {
                tail = null;
            }
            count--;
            return value;
        }

        public T RemoveLast()
        {
            if (tail == null) throw new InvalidOperationException("List is empty.");
            if (head == tail)
            {
                T value = head.Data;
                head = tail = null;
                count--;
                return value;
            }

            Node current = head;
            while (current.Next != tail)
            {
                current = current.Next;
            }

            T returnValue = tail.Data;
            tail = current;
            tail.Next = null;
            count--;
            return returnValue;
        }

        public int Count => count;
    }

}
