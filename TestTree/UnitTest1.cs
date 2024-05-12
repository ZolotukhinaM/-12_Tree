using ClassLibrary2;
using ЛР_12_Дерево;
using System;
using System.Linq;
using Moq;
using System.IO;


namespace TestTree
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToString_EmptyData_ReturnsEmptyString()
        {
            var tree = new PointTree<string>(null);

            Assert.AreEqual("", tree.ToString());
        }
        [TestMethod]
        public void ToString_NonEmptyData_ReturnsDataToString()
        {
            var tree = new PointTree<int>(42);

            Assert.AreEqual("42", tree.ToString());
        }
        [TestMethod]

        public void TestGetHashCode_NullData_ReturnsZero()
        {
            var tree = new PointTree<object> { Data = null };
            Assert.AreEqual(0, tree.GetHashCode());
        }
        [TestMethod]
        public void TestEquals_ObjectsWithData_ReturnsTrue()
        {
            var data1 = 10;
            var data2 = 20;
            var tree1 = new PointTree<int> { Data = data1, Left = null, Right = null };
            var tree2 = new PointTree<int> { Data = data1, Left = null, Right = null };
            var tree3 = new PointTree<int> { Data = data2, Left = null, Right = null };

            Assert.IsTrue(tree1.Equals(tree2));
            Assert.IsFalse(tree1.Equals(tree3));
        }
        [TestMethod]
        public void TestEquals_ObjectsWithDifferentStructures_ReturnsFalse()
        {
            var data = 10;
            var tree1 = new PointTree<int>
            {
                Data = data,
                Left = new PointTree<int> { Data = 5, Left = null, Right = null },
                Right = new PointTree<int> { Data = 15, Left = null, Right = null }
            };

            var tree2 = new PointTree<int>
            {
                Data = data,
                Left = new PointTree<int> { Data = 5, Left = null, Right = null },
                Right = null
            };

            Assert.IsFalse(tree1.Equals(tree2));
        }

        [TestMethod]
        public void GetKey_ReturnsDataToString()
        {
            var tree = new PointTree<double>(3.14);

            Assert.AreEqual("3,14", tree.GetKey());
        }



        [TestMethod]
        public void Count_LengthInitialized_ReturnsLength()
        {
            Tree<CelestialBody> tree = new Tree<CelestialBody>(5);
            Assert.AreEqual(5, tree.Count);
        }

        [TestMethod]
        public void PrintTree_EmptyTree_PrintsNothing()
        {
            Tree<CelestialBody> tree = new Tree<CelestialBody>(0);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.PrintTree();
                Assert.AreEqual("", sw.ToString().Trim());
            }
        }
        [TestMethod]
        public void TestShowWithEmptyTree()
        {
            var tree = new Tree<CelestialBody>(0);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.Show(tree.root);

                string result = sw.ToString();
                // Ожидаем пустой вывод, так как дерево без узлов
                Assert.AreEqual("", result);
            }
        }
        [TestMethod]
        public void AddPointIncreasesCount()
        {
            var tree = new Tree<CelestialBody>(0);
            var body = new CelestialBody("Earth", 123, 123);
            tree.AddPoint(body);
            Assert.AreEqual(1, tree.Count);
        }
        [TestMethod]
        public void AddExistingPointDoesNotIncreaseCount()
        {
            var tree = new Tree<CelestialBody>(0);
            var body = new CelestialBody("Earth", 123, 123);
            tree.AddPoint(body);
            tree.AddPoint(body); // Adding the same point
            Assert.AreEqual(1, tree.Count);
        }
        [TestMethod]
        public void MakeTree_PositiveLength_ReturnsNotNullRoot()
        {
            Tree<CelestialBody> tree = new Tree<CelestialBody>(3);
            Assert.IsNotNull(tree.root);
        }
        [TestMethod]
        public void TestTreeHeight_Empty()
        {
            var tree = new Tree<CelestialBody>(0);
            tree.root = null;  // Предположим, что дерево пустое
            Assert.AreEqual(0, tree.TreeHeight());
        }
        [TestMethod]
        public void TestTreeHeight_OneElement()
        {
            var tree = new Tree<CelestialBody>(10);
            Assert.AreEqual(4, tree.TreeHeight());
        }
        [TestMethod]
        public void TestTransformToArray()
        {
            var tree = new Tree<CelestialBody>(2);
            tree.root.Left = new PointTree<CelestialBody>(new CelestialBody("Mercury", 2, 2));
            tree.root.Right = new PointTree<CelestialBody>(new CelestialBody("Venus", 2, 2));

            CelestialBody[] array = new CelestialBody[3];
            int currentIndex = 0;
            tree.TransformToArray(tree.root, array, ref currentIndex);

            CelestialBody[] expected = new CelestialBody[]{
            new CelestialBody("Mercury", 2,2),
            new CelestialBody("Venus",2,2)
        };
        }
        [TestMethod]
        public void TransformToFindTree_ShouldHandleEmptyTree()
        {
            // Подготовка данных
            var root = new Tree<CelestialBody>(0);
            root.count = 0;

            // Вызов тестируемой функции
            root.TransformToFindTree();

            // Проверки
            Assert.IsNull(root.root, "Root should be null for an empty tree.");
        }
        [TestMethod]
        public void Delete_NullNode_ReturnsNull()
        {
            // Arrange: создание экземпляра дерева типа int
            Tree<CelestialBody> tree = new Tree<CelestialBody>(0); // размер дерева 0, так как он не используется

            // Act: Пытаемся удалить элемент из "пустого" дерева
            var result = tree.Delete(null, null);

            // Assert: проверяем, что результат тоже null, так как нет элементов для удаления
            Assert.IsNull(result);
        }
        [TestMethod]
        public void DeleteByKey_TreeWithSingleElement_BecomesEmpty()
        {
            CelestialBody item = new CelestialBody("null", 1, 1);
            Tree<CelestialBody> localTree = new Tree<CelestialBody>(1);
            localTree.root.Data = item;

            localTree.DeleteByKey(item);

            Assert.IsNull(localTree.root);
        }
        [TestMethod]
        public void DeleteByKey_NodeInRightSubtree_DeletesProperly()
        {
            CelestialBody rootItem = new CelestialBody("Earth", 5972, 6371);
            CelestialBody leftItem = new CelestialBody("Venus", 4867, 6052);
            CelestialBody rightItem = new CelestialBody("Mars", 641, 3390);
            CelestialBody rightLeftItem = new CelestialBody("Jupiter", 1898600, 69911);
            CelestialBody rightRightItem = new CelestialBody("Saturn", 568346, 58232);

            var tree = new Tree<CelestialBody>(5); // Создаём дерево с 5 элементами
            tree.root = new PointTree<CelestialBody>(rootItem)
            {
                Left = new PointTree<CelestialBody>(leftItem),
                Right = new PointTree<CelestialBody>(rightItem)
                {
                    Left = new PointTree<CelestialBody>(rightLeftItem),
                    Right = new PointTree<CelestialBody>(rightRightItem)
                }
            };

            // Удаляем узел "Saturn", который находится глубоко в правом поддереве
            tree.DeleteByKey(rightRightItem);

            Assert.IsNull(tree.root.Right.Right); // Убедимся, что у Mars нет правого ребенка после удаления
            Assert.AreEqual("Mars", tree.root.Right.Data.Name); // Mars все еще должен быть на своем месте
            Assert.AreEqual("Jupiter", tree.root.Right.Left.Data.Name); // Jupiter останется неизменным слева от Mars
        }

        [TestMethod]
        public void DeleteByKey_NodeWithTwoChildren_ReplacesWithMinValue()
        {
            CelestialBody rootItem = new CelestialBody("Earth", 5972, 6371);
            CelestialBody leftItem = new CelestialBody("Venus", 4867, 6052);
            CelestialBody rightItem = new CelestialBody("Mars", 641, 3390);
            CelestialBody rightLeftItem = new CelestialBody("Jupiter", 1898600, 69911);

            var tree = new Tree<CelestialBody>(4); // Создаём дерево с 4 элементами
            tree.root = new PointTree<CelestialBody>(rootItem)
            {
                Left = new PointTree<CelestialBody>(leftItem),
                Right = new PointTree<CelestialBody>(rightItem)
                {
                    Left = new PointTree<CelestialBody>(rightLeftItem)
                }
            };

            tree.DeleteByKey(rootItem);

            Assert.AreEqual("Jupiter", tree.root.Data.Name); // Min значение Jupiter должно заменить корень
        }
        [TestMethod]
        public void RemoveTree_EmptiesTree()
        {
            Tree<CelestialBody> localTree = new Tree<CelestialBody>(1);
            localTree.root.Data = new CelestialBody("null",1,1);

            localTree.RemoveTree();

            Assert.IsNull(localTree.root);
            Assert.AreEqual(0, localTree.Count);
        }
    }
}