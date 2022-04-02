using AwesomeAssebly;
using AwesomeAssebly.Awesome.Abstractions;
using AwesomeAssebly.Awesome.Implementation;

namespace TestTypeConsoleSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ExecTests(
                ("Operations", TestOne),
                ("Exceptions", TestTwo),
                ("Events", TestThree)
                );
        }

        private static void TestOne()
        {
            var s = new AwesomeLinkedList<int>();

            s.Add(2);
            s.Print();
            s.Add(3);
            s.Print();
            s.Add(5);
            s.Print();

            s.Remove(6);
            s.Print();
            s.Remove(2);
            s.Print();
            s.Remove(5);
            s.Print();
            s.Remove(3);
            s.Print();

            s.Add(6);
            s.Print();
            s.Add(10);
            s.Print();
            s.Remove(6);
            s.Print();
            s.Add(11);
            s.Print();

            Console.WriteLine("*****************************");

            for (int i = 0; i < 20; i += 2)
            {
                s.Add(i);
            }
            s.Print();

            Console.WriteLine("Contains 1 => {0} ", s.Contains(1));
            Console.WriteLine("Contains 2 => {0} ", s.Contains(2));

            Console.WriteLine("Find 1 => {0} ", s.Find(1));
            Console.WriteLine("Find 2 => {0} ", s.Find(2));
        }
        private static void TestTwo()
        {
            int n = 10;

            IAwesomeLinkedList<Test> s = new AwesomeLinkedList<Test>();

            for (int i = 0; i < n; i++)
                s.Add(Test.Default);

            ExecInTry(
                () =>
                {
                    var array = new Test[n];
                    s.CopyTo(array, -1);
                },
                () =>
                {
                    var array = new Test[n / 2];
                    s.CopyTo(array, 0);
                },
                () =>
                {
                    var array = new Test[n];
                    s.CopyTo(array, n);
                },
                () =>
                {
                    s.GetObjectData(default, default);
                },

                () => { }
            );
        }
        private static void TestThree()
        {
            int n = 10;
            IAwesomeLinkedList<Test> s = new AwesomeLinkedList<Test>();

            for (int i = 0; i < n; i++)
                s.Add(Test.Default);

            s.OnAdd += (data) => { Console.WriteLine("-------{0} ADDED", data); s.Print(); };
            s.OnRemove += (data) => { Console.WriteLine("-------{0} REMOVED", data); s.Print(); };

            var t1 = new Test
            {
                Name = "Name 1",
                Date = DateTime.Now,
            };
            var t2 = new Test
            {
                Name = "Name 2",
                Date = DateTime.Now,
            };
            var t3 = new Test
            {
                Name = "Name 3",
                Date = DateTime.Now,
            };

            s.Add(t1);
            s.Add(t2);
            s.Remove(t1);
            s.Add(t3);
        }

        private static void ExecTests(params (string, Action)[] tests)
        {
            foreach (var test in tests)
            {
                Console.Clear();
                Console.WriteLine("Test '{0}'", test.Item1);

                test.Item2();

                Console.WriteLine("Press enter to continue...");
                Console.ReadKey();
            }
        }
        private static void ExecInTry(params Action[] actions)
        {
            int ind = 0;
            foreach (var action in actions)
            {
                try
                {
                    Console.Write("SubTest {0} => ", ind++);
                    action();
                    Console.WriteLine("OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
