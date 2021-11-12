using NUnit.Framework;
using list;

namespace tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void set()
        {
            List<int> l = new List<int>(3);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 3);
            Assert.IsTrue(l.get(0) == 1 && l.get(1) == 2 && l.get(2) == 3, "set or get not working");
        }
        [Test]
        public void insert()
        {
            List<int> l = new List<int>(2);
            l.set(0, 1);
            l.set(1, 2);
            l.insert(1, 3);
            Assert.IsTrue(l.get(0) == 1 && l.get(1) == 3 && l.get(2) == 2, "Insert not working");
        }
        [Test]
        public void del_index()
        {
            List<int> l = new List<int>(4);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 3);
            l.set(3, 4);
            l.del_index(1);
            l.del_index(2);
            l.del_index(0);
            
            Assert.IsTrue(l.get(0) == 3 && l.length() == 1, "del_index not working");
        }
        [Test]
        public void del_val()
        {
            List<int> l = new List<int>(4);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 2);
            l.set(3, 2);
            l.del_val(2);
            Assert.IsTrue(l.get(0) == 1 && l.length() == 1, "del_val not working");
        }
        [Test]
        public void find()
        {
            List<int> l = new List<int>(4);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 2);
            l.set(3, 2);
            Assert.IsTrue(l.find(2) == 1, "find: not first match or not found");
            Assert.IsTrue(l.find(3) == -1, "find: wrong result");
        }
        [Test]
        public void find_all()
        {
            List<int> l = new List<int>(4);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 2);
            l.set(3, 2);
            Assert.IsTrue(l.find_all(2).length() == 3 && l.find_all(3).length() == 0, "find_all not working");
        }
        [Test]
        public void is_symetric()
        {
            List<int> l = new List<int>(4);
            l.set(0, 1);
            l.set(1, 2);
            l.set(2, 2);
            l.set(3, 1);

            List<int> l1 = new List<int>(4);
            l1.set(0, 1);
            l1.set(1, 2);
            l1.set(2, 3);
            l1.set(3, 1);
            Assert.IsTrue(l.is_symetric() && !l1.is_symetric(), "is_symetric not working");
        }
        [Test]
        public void sort()
        {
            List<int> l = new List<int>(5);
            l.set(0, 1);
            l.set(1, 3);
            l.set(2, 2);
            l.set(3, 1);
            l.set(4, 6);
            l.sort();
            Console.WriteLine(l);

            Assert.IsTrue(l.get(0) == 1 && l.get(1) == 1 && l.get(2) == 2 && l.get(3) == 3 && l.get(4) == 6, "sort not working");
        }
        [Test]
        public void is_sorted()
        {
            List<int> l = new List<int>(5);
            l.set(0, 1);
            l.set(1, 3);
            l.set(2, 2);
            l.set(3, 1);
            l.set(4, 6);
            bool before = l.is_sorted();
            l.sort();
            Console.WriteLine(l);

            Assert.IsTrue(l.is_sorted() && !before, "is_sorted not working");
        }
        [Test]
        public void concat()
        {
            List<int> l = new List<int>(2);
            l.set(0, 0);
            l.set(1, 3);

            List<int> l1 = new List<int>(2);
            l1.set(0, 1);
            l1.set(1, 2);

            l = l.concat(l1);
            Assert.IsTrue(l.get(0) == 0 && l.get(1) == 3 && l.get(2) == 1 && l.get(3) == 2, "concat not working");
        }
        [Test]
        public void sorted_fuse()
        {
            List<int> l = new List<int>(3);
            l.set(0, 0);
            l.set(1, 3);
            l.set(2, 2);

            List<int> l1 = new List<int>(3);
            l1.set(0, 1);
            l1.set(1, 8);
            l1.set(2, 2);

            List<int> l2 = l.sorted_fuse(l1);
            
            List<int> l3 = new List<int>(6);
            l3.set(0, 0);
            l3.set(1, 1);
            l3.set(2, 2);
            l3.set(3, 2);
            l3.set(4, 3);
            l3.set(5, 8);
            bool match  = l2.CompareTo(l3) == 0;
            l3.set(5, 3);
            bool match1 = l2.CompareTo(l3) == 0;

            Assert.IsTrue(match && !match1, "sorted_fuse not working");
        }
    }
}
