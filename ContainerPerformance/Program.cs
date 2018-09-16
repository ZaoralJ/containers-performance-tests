using System;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;

namespace ContainerPerformance
{
    class Program
    {
        private static void Main()
        {
            // warm up
            Console.WriteLine("/* ignore");
            MsContainerResolveSingleton(100);
            CWContainerResolveSingleton(100);
            Console.WriteLine("*/");
            Console.WriteLine();

            Console.WriteLine("# Singleton");

            MsContainerResolveSingleton(100);
            MsContainerResolveSingleton(1_000);
            MsContainerResolveSingleton(100_000);
            MsContainerResolveSingleton(1_000_000);

            CWContainerResolveSingleton(100);
            CWContainerResolveSingleton(1_000);
            CWContainerResolveSingleton(100_000);
            CWContainerResolveSingleton(1_000_000);

            Console.WriteLine();
            Console.WriteLine("# Transient");

            MsContainerResolveTransient(100);
            MsContainerResolveTransient(1_000);
            MsContainerResolveTransient(100_000);
            MsContainerResolveTransient(1_000_000);

            CWContainerResolveTransient(100);
            CWContainerResolveTransient(1_000);
            CWContainerResolveTransient(100_000);
            CWContainerResolveTransient(1_000_000);

            Console.WriteLine();
            Console.WriteLine("# Complex transient");

            MsContainerResolveComplexTransient(100);
            MsContainerResolveComplexTransient(1_000);
            MsContainerResolveComplexTransient(100_000);
            MsContainerResolveComplexTransient(1_000_000);

            CWContainerResolveComplexTransient(100);
            CWContainerResolveComplexTransient(1_000);
            CWContainerResolveComplexTransient(100_000);
            CWContainerResolveComplexTransient(1_000_000);

            Console.WriteLine();
            Console.WriteLine("# Complex transient new container");
            
            MsContainerResolveComplexTransient(100, true);
            MsContainerResolveComplexTransient(1_000, true);
            MsContainerResolveComplexTransient(100_000, true);

            CWContainerResolveComplexTransient(100, true);
            CWContainerResolveComplexTransient(1_000, true);
            CWContainerResolveComplexTransient(100_000, true);

            Console.ReadLine();
        }

        private static void WarmUp()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.BuildServiceProvider();
            new WindsorContainer();
        }

        private static void MsContainerResolveSingleton(int times)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<SimpleClass>();
            var container = serviceCollection.BuildServiceProvider();

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                container.GetRequiredService<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- MS container static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");
        }

        private static void MsContainerResolveTransient(int times)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<SimpleClass>();
            var container = serviceCollection.BuildServiceProvider();

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                container.GetRequiredService<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- MS container static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");
        }

        private static void MsContainerResolveComplexTransient(int times, bool alwaysNewContainer = false)
        {
            var container = GetContainer();

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                if (alwaysNewContainer)
                {
                    container = GetContainer();
                }
                container.GetRequiredService<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- MS container static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");

            ServiceProvider GetContainer()
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient<SimpleClass>();
                serviceCollection.AddTransient<ComplexClass>();
                return serviceCollection.BuildServiceProvider();
            }
        }

        private static void CWContainerResolveSingleton(int times)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<SimpleClass>());

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                container.Resolve<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- Castle.Windsor static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");
        }

        private static void CWContainerResolveTransient(int times)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<SimpleClass>().LifestyleTransient());

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                container.Resolve<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- Castle.Windsor static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");
        }

        private static void CWContainerResolveComplexTransient(int times, bool alwaysNewContainer = false)
        {
            var container = GetContainer();

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < times; i++)
            {
                if (alwaysNewContainer)
                {
                    container = GetContainer();
                }
                container.Resolve<SimpleClass>();
            }
            sw.Stop();
            Console.WriteLine($"- Castle.Windsor static {times}x times in {GetMillisecondsFromTicks(sw.ElapsedTicks)} ms");

            WindsorContainer GetContainer()
            {
                var c = new WindsorContainer();
                c.Register(Component.For<SimpleClass>().LifestyleTransient());
                c.Register(Component.For<ComplexClass>().LifestyleTransient());
                return c;
            }
        }

        private static double GetMillisecondsFromTicks(long ticks)
        {
            return Math.Round(TimeSpan.FromTicks(ticks).TotalMilliseconds, 3);
        }
    }
}
