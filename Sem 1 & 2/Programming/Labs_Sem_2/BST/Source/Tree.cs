using System.Collections.Generic;

namespace BinaryTree
{
    public class Tree
    {
        private Item root;


        public Tree()
        {
            root = null;
        }


        public bool Find(int x)
        {
            Item bufferItem;

            return HiddenFind(x, out bufferItem);
        }

        public bool Insert(int x)
        {
            Item r, p;

            if (root == null)
            {
                r = new Item(x);
                root = r;
                return true;
            }

            if (HiddenFind(x, out r)) return false;

            p = new Item(x);

            p.father = r;

            if (r.data < x)
                r.son_right = p;
            else
                r.son_left = p;

            return true;
        }

        public bool Delete(int x)
        {
            Item r, p;

            if (!HiddenFind(x, out r)) return false;

            if ((r.son_left == null) || (r.son_right == null))
            {
                DeleteItem(r);
                return true;
            }

            p = r.son_left;

            while (p.son_right != null) p = p.son_right;

            r.data = p.data;

            DeleteItem(p);

            return true;
        }


        public List<int> GetLayer(int x)
        {
            List<int> Layer = new List<int>();

            Item
                bufferItem;
            int
                depth = 0,
                current_level = 0;

            if (!HiddenFind(x, out bufferItem)) return Layer;


            while (bufferItem != null)
            {
                bufferItem = bufferItem.father;
                ++depth;
            }

            Queue<Item> ItemQueue = new Queue<Item>();

            ItemQueue.Enqueue(root);
            current_level = 1;

            while (current_level != depth)
            {
                int CurrentLayerCount = ItemQueue.Count;

                for (int i = 1; i <= CurrentLayerCount; i++)
                {
                    var item = ItemQueue.Dequeue();

                    if (item.son_left != null) ItemQueue.Enqueue(item.son_left);
                    if (item.son_right != null) ItemQueue.Enqueue(item.son_right);
                }

                current_level++;
            }

            foreach (var item in ItemQueue)
            {
                Layer.Add(item.data);
            }

            return Layer;
        }



        private void DeleteItem(Item x)
        {
            if (x.father == null)
            {
                if (x.son_left != null)
                {
                    root = x.son_left;
                    x.son_left.father = null;
                }
                else
                {
                    root = x.son_right;
                    if (x.son_right != null)
                        x.son_right.father = null;
                }
            }

            else if (x.father.son_left == x)
            {
                if (x.son_left != null)
                {
                    x.father.son_left = x.son_left;
                    x.son_left.father = x.father;
                }
                else
                {
                    x.father.son_left = x.son_right;
                    if (x.son_right != null)
                        x.son_right.father = x.father;
                }
            }

            else if (x.son_left != null)
            {
                x.father.son_right = x.son_left;
                x.son_left.father = x.father;
            }

            else
            {
                x.father.son_right = x.son_right;
                if (x.son_right != null)
                    x.son_right.father = x.father;
            }

            x.father = x.son_left = x.son_right = null;
        }

        private bool HiddenFind(int x, out Item parameterItem)
        {
            parameterItem = root;

            Item bufferItem = parameterItem;

            while (bufferItem != null)
            {
                parameterItem = bufferItem;

                if (bufferItem.data == x)
                    return true;

                if (bufferItem.data < x)
                    bufferItem = bufferItem.son_right;

                else
                    bufferItem = bufferItem.son_left;
            }

            return false;
        }


        private class Item
        {
            public int data;
            public Item son_left, son_right, father;

            public Item(int x)
            {
                data = x;
                son_left = son_right = father = null;
            }
        }
    }
}
