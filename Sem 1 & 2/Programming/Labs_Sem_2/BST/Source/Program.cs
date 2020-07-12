using System;

namespace BinaryTree
{
    internal class Program
    {
        private static void Main()
        {
            Tree TestTree = new Tree();

            Console.WriteLine("Введите количество вершин: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите корень дерева: ");
            TestTree.Insert(Convert.ToInt32(Console.ReadLine()));
            n--;

            while (n != 0)
            {
                Console.WriteLine("Введите вершину: ");
                TestTree.Insert(Convert.ToInt32(Console.ReadLine()));
                n--;
            }

            //TestTree.Insert(8);
            //TestTree.Insert(4);
            //TestTree.Insert(12);
            //TestTree.Insert(2);
            //TestTree.Insert(6);
            //TestTree.Insert(10);
            //TestTree.Insert(14);
            //TestTree.Insert(1);
            //TestTree.Insert(3);
            //TestTree.Insert(5);
            //TestTree.Insert(7);
            //TestTree.Insert(9);
            //TestTree.Insert(11);
            //TestTree.Insert(13);
            //TestTree.Insert(15);

            Console.WriteLine();

            int value = 0;
            int.TryParse(Console.ReadLine(), out value);

            foreach (var val in TestTree.GetLayer(value))
            {
                Console.WriteLine(val);
            }

            Console.ReadKey();
        }
    }
}
