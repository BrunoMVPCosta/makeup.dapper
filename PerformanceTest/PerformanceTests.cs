using PerformanceTest.MakeUpDapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PerformanceTest
{
    class PerformanceTests
    {
        class Test
        {
            public static Test Create(Action<int> iteration, string name)
            {
                return new Test { Iteration = iteration, Name = name };
            }

            public Action<int> Iteration { get; set; }
            public string Name { get; set; }
            public Stopwatch Watch { get; set; }
        }

        class Tests : List<Test>
        {
            public void Add(Action<int> iteration, string name)
            {
                Add(Test.Create(iteration, name));
            }

            public void Run(int iterations)
            {
                // warmup 
                foreach (var test in this)
                {
                    test.Iteration(iterations + 1);
                    test.Watch = new Stopwatch();
                    test.Watch.Reset();
                }

                var rand = new Random();
                for (int i = 1; i <= iterations; i++)
                {
                    foreach (var test in this.OrderBy(ignore => rand.Next()))
                    {
                        test.Watch.Start();
                        test.Iteration(i);
                        test.Watch.Stop();
                    }
                }

                foreach (var test in this.OrderBy(t => t.Watch.ElapsedMilliseconds))
                {
                    Console.WriteLine(test.Name + " took " + test.Watch.ElapsedMilliseconds + "ms");
                }
            }
        }

        public void Run(int iterations)
        {

            var tests = new Tests();

            var context = new MUContext("ConnectionString");
            tests.Add(id => context.Posts.Where(p => p.Id == 1).ToList(), "MakeUp dapper");

            tests.Run(iterations);

            Console.Read();
        }
    }
}
