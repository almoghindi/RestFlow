﻿namespace RestFlow.Common.DataStructures
{
    public class CustomTree<T>
    {
        public class TreeNode
        {
            public T Data;
            public List<TreeNode> Children;

            public TreeNode(T data)
            {
                Data = data;
                Children = new List<TreeNode>();
            }
        }

        public TreeNode Root { get; private set; }

        public CustomTree(T rootData)
        {
            Root = new TreeNode(rootData);
        }

        public TreeNode AddChild(TreeNode parent, T childData)
        {
            var childNode = new TreeNode(childData);
            parent.Children.Add(childNode);
            return childNode;
        }

        public void Traverse(TreeNode node, Action<T> action)
        {
            if (node == null) return;
            action(node.Data);
            foreach (var child in node.Children)
            {
                Traverse(child, action);
            }
        }
    }

}
