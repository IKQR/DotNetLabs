using AwesomeAssebly;
using AwesomeAssebly.Awesome.Abstractions;
using AwesomeAssebly.Awesome.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AwesomeLinkedList
{
    public class UnitTest
    {
        [Fact]
        public void TestCRUD()
        {
            var s = new AwesomeLinkedList<int>();

            s.Add(2);
            Assert.Equal(s, new[] { 2 });
            s.Add(3);
            Assert.Equal(s, new[] { 2, 3 });
            s.Add(5);
            Assert.Equal(s, new[] { 2, 3, 5 });

            s.Remove(6);
            Assert.Equal(s, new[] { 2, 3, 5 });
            s.Remove(2);
            Assert.Equal(s, new[] { 3, 5 });
            s.Remove(5);
            Assert.Equal(s, new[] { 3 });
            s.Remove(3);
            Assert.Empty(s);

            s.Add(6);
            s.Add(10);
            Assert.Equal(s, new[] { 6, 10 });
            s.Remove(6);
            s.Add(11);
            Assert.Equal(s, new[] { 10, 11 });

            for (int i = 0; i < 20; i += 2)
            {
                s.Add(i);
            }

            Assert.Equal(s, "10 11 0 2 4 6 8 10 12 14 16 18".Split(" ").Select(int.Parse));

            Assert.DoesNotContain(1, s);
            Assert.Contains(2, s);

            Assert.Null(s.Find(1));
            Assert.NotNull(s.Find(2));

            Assert.Equal("'Node with data 2 typeof Int32'", s.Find(2).ToString());

            s.Clear();
            Assert.Empty(s);
            s.Clear();
            Assert.Empty(s);
        }
        [Fact]
        public void TestExceptions()
        {
            int n = 10;

            IAwesomeLinkedList<TestModel> s = new AwesomeLinkedList<TestModel>();

            for (int i = 0; i < n; i++)
                s.Add(TestModel.Default);

            Assert.Throws<ArgumentNullException>(() =>
            {
                s.GetObjectData(default, default);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                var array = new TestModel[n / 2];
                s.CopyTo(array, 0);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var array = new TestModel[n];
                s.CopyTo(array, n);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var array = new TestModel[n];
                s.CopyTo(array, -1);
            });
        }
        [Fact]
        public void TestEvents()
        {
            int n = 10;
            IAwesomeLinkedList<TestModel> s = new AwesomeLinkedList<TestModel>();

            for (int i = 0; i < n; i++)
                s.Add(TestModel.Default);

            var duplicate = s.ToList();

            s.OnAdd += (data) => { duplicate.Add(data); };
            s.OnRemove += (data) => { duplicate.Remove(data); };

            var t1 = new TestModel
            {
                Name = "Name 1",
                Date = DateTime.Now,
            };
            var t2 = new TestModel
            {
                Name = "Name 2",
                Date = DateTime.Now,
            };
            var t3 = new TestModel
            {
                Name = "Name 3",
                Date = DateTime.Now,
            };

            s.Add(t1);
            s.Add(t2);
            s.Remove(t1);
            s.Add(t3);

            Assert.Equal(s, duplicate);

        }
    }
}