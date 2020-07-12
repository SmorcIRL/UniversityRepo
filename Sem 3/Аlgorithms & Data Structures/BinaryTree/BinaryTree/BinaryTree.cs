using System;

namespace BinaryTree
{
    public class BinaryTree<T> where T : IComparable
    {
        public BinaryNode<T> Root { get; private set; }

        public BinaryTree(T rootValue)
        {
            Root = new BinaryNode<T>(rootValue);
        }
        public BinaryTree(T[] array)
        {
            if (array != null && array.Length != 0)
            {
                Root = new BinaryNode<T>(array[0]);

                for (int i = 1; i < array.Length; ++i)
                {
                    Insert(array[i]);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public bool Insert(T value)
        {
            if (Root == null)
            {
                Root = new BinaryNode<T>(value);
                return true;
            }

            if (Find(value, out BinaryNode<T> parent))
            {
                return false;
            }

            BinaryNode<T> newNode = new BinaryNode<T>(value, parent);

            if (parent.Value.CompareTo(value) < 0)
            {
                parent.RightSon = newNode;
            }
            else
            {
                parent.LeftSon = newNode;
            }

            return true;
        }
        public bool Find(T value)
        {
            return Find(value, out BinaryNode<T> parent);
        }
        private bool Find(T value, out BinaryNode<T> parent)
        {
            parent = Root;

            BinaryNode<T> buff = parent;

            while (buff != null)
            {
                parent = buff;

                int compareResult = buff.Value.CompareTo(value);

                if (compareResult == 0)
                    return true;
                if (compareResult < 0)
                    buff = buff.RightSon;
                else
                    buff = buff.LeftSon;
            }

            return false;
        }
        public bool Delete(T value, DirectionMode direction, out BinaryNode<T> node)
        {
            if (!Find(value, out node))
            {
                return false;
            }

            DeleteNode(node, direction);

            return true;
        }
        private void DeleteNode(BinaryNode<T> node, DirectionMode direction)
        {
            if (node.IsLeaf)
            {
                if (!node.HasParent)
                {
                    Root = null;
                }
                else
                {
                    if (node.Parent.LeftSon == node)
                    {
                        node.Parent.LeftSon = null;
                    }
                    else if (node.Parent.RightSon == node)
                    {
                        node.Parent.RightSon = null;
                    }
                }
            }
            else if (node.OnlyOneSon)
            {
                if (!node.HasParent)
                {
                    if (node.HasLeftSon)
                    {
                        Root = node.LeftSon;
                        node.LeftSon.Parent = null;
                    }
                    else if (node.HasRightSon)
                    {
                        Root = node.RightSon;
                        node.RightSon.Parent = null;
                    }
                }
                else
                {
                    if (node.HasLeftSon)
                    {
                        if (node.Parent.LeftSon == node)
                        {
                            node.LeftSon.Parent = node.Parent;
                            node.Parent.LeftSon = node.LeftSon;
                        }
                        else if (node.Parent.RightSon == node)
                        {
                            node.LeftSon.Parent = node.Parent;
                            node.Parent.RightSon = node.LeftSon;
                        }
                    }
                    else if (node.HasRightSon)
                    {
                        if (node.Parent.LeftSon == node)
                        {
                            node.RightSon.Parent = node.Parent;
                            node.Parent.LeftSon = node.RightSon;
                        }
                        else if (node.Parent.RightSon == node)
                        {
                            node.RightSon.Parent = node.Parent;
                            node.Parent.RightSon = node.RightSon;
                        }
                    }
                }
            }

            else if (node.BothSons)
            {
                if (direction == DirectionMode.Left)
                {
                    var buffNode = FindMax(node.LeftSon);
                    node.Value = buffNode.Value;
                    DeleteNode(buffNode, direction);
                }

                else if (direction == DirectionMode.Right)
                {
                    var buffNode = FindMin(node.RightSon);
                    node.Value = buffNode.Value;
                    DeleteNode(buffNode, direction);
                }
            }
        }
        public void ProcessTree(Action<BinaryNode<T>> action, RoutingMode routing, DirectionMode direction)
        {
            ProcessNode(Root, action, routing, direction);
        }
        private void ProcessNode(BinaryNode<T> node, Action<BinaryNode<T>> action, RoutingMode routing, DirectionMode direction)
        {
            if (node == null)
            {
                return;
            }

            switch (routing)
            {
                case RoutingMode.Direct:
                {
                    action?.Invoke(node);

                    if (direction == DirectionMode.Left)
                    {
                        ProcessNode(node.LeftSon, action, routing, direction);
                        ProcessNode(node.RightSon, action, routing, direction);
                    }
                    else if (direction == DirectionMode.Right)
                    {
                        ProcessNode(node.RightSon, action, routing, direction);
                        ProcessNode(node.LeftSon, action, routing, direction);
                    }

                    break;
                }
                case RoutingMode.Internal:
                {
                    if (direction == DirectionMode.Left)
                    {
                        ProcessNode(node.LeftSon, action, routing, direction);
                        action?.Invoke(node);
                        ProcessNode(node.RightSon, action, routing, direction);
                    }
                    else if (direction == DirectionMode.Right)
                    {
                        ProcessNode(node.RightSon, action, routing, direction);
                        action?.Invoke(node);
                        ProcessNode(node.LeftSon, action, routing, direction);
                    }

                    break;
                }
                case RoutingMode.Inverted:
                {
                    if (direction == DirectionMode.Left)
                    {
                        ProcessNode(node.LeftSon, action, routing, direction);
                        ProcessNode(node.RightSon, action, routing, direction);
                    }
                    else if (direction == DirectionMode.Right)
                    {
                        ProcessNode(node.RightSon, action, routing, direction);
                        ProcessNode(node.LeftSon, action, routing, direction);
                    }

                    action?.Invoke(node);
                    break;
                }
            }
        }

        public BinaryNode<T> FindMin(BinaryNode<T> localRoot)
        {
            if (localRoot == null)
                return null;

            var node = localRoot;

            while (node.LeftSon != null)
            {
                node = node.LeftSon;
            }

            return node;
        }
        public BinaryNode<T> FindMax(BinaryNode<T> localRoot)
        {
            if (localRoot == null)
                return null;

            var node = localRoot;

            while (node.RightSon != null)
            {
                node = node.RightSon;
            }

            return node;
        }

        public enum RoutingMode
        {
            Direct = 0,
            Internal = 1,
            Inverted = 2
        }
        public enum DirectionMode
        {
            Left = 0,
            Right = 1
        }

        public class BinaryNode<M> where M : T
        {
            public M Value { get; set; }

            public int[] Ints { get; set; }
            public bool[] Flags { get; set; }

            public BinaryNode<M> Parent { get; set; }
            public BinaryNode<M> LeftSon { get; set; }
            public BinaryNode<M> RightSon { get; set; }

            public bool HasParent => Parent != null;
            public bool HasLeftSon => LeftSon != null;
            public bool HasRightSon => RightSon != null;
            public bool OnlyOneSon => HasLeftSon != HasRightSon;
            public bool IsLeaf => !HasLeftSon && !HasRightSon;
            public bool BothSons => !IsLeaf;

            public BinaryNode(M value, BinaryNode<M> parent = null)
            {
                Value = value;
                Parent = parent;
            }
        }
    }
}