using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_ContinueDesign
{
    public interface IWarmDrink
    {
        void Consume();
    }
    //Consume
    internal class Water : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Warm water is served.");
        }
    }
    internal class Coffee : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot coffee is served.");
        }
    }
    internal class Choclate : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot Choclate is served.");
        }
    }
    //EndofConsume
    public interface IWarmDrinkFactory
    {
        IWarmDrink Prepare(int total);
    }
    internal class HotWaterFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pour {total} ml hot water in your cup");
            return new Water();
        }
    }
    internal class HotCoffeeFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pour {total} ml hot Coffee in your cup");
            return new Coffee();
        }
    }
    internal class HotChoclateFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pour {total} ml hot Cohclate in your cup");
            return new Choclate();
        }
    }
    public class WarmDrinkMachine
    {
        public enum AvailableDrink // violates open-closed
        {
            Coffee, Tea
        }
        private Dictionary<AvailableDrink, IWarmDrinkFactory> factories =
          new Dictionary<AvailableDrink, IWarmDrinkFactory>();

        private List<Tuple<string, IWarmDrinkFactory>> namedFactories =
          new List<Tuple<string, IWarmDrinkFactory>>();

        public WarmDrinkMachine()
        {
            foreach (var t in typeof(WarmDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IWarmDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    namedFactories.Add(Tuple.Create(
                      t.Name.Replace("Factory", string.Empty), (IWarmDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }
        public IWarmDrink MakeDrink()
        {
            Console.WriteLine("This is what we serve today:");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                var tuple1 = namedFactories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
                
            }
            Console.WriteLine("Select a number to continue:");
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i) // c# 7
                    && i >= 0
                    && i < namedFactories.Count)
                {
                    Console.Write("How much: ");
                    s = Console.ReadLine();
                    if (s != null
                        && int.TryParse(s, out int total)
                        && total > 0)
                    {
                        return namedFactories[i].Item2.Prepare(total);
                    }
                }
                Console.WriteLine("Something went wrong with your input, try again.");
            }
        }

        public IWarmDrink MakeHotDrink()
        {
            Console.WriteLine("This is what we serve today:");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }
            Console.WriteLine("Select a number to continue:");
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i) // c# 7
                    && i >= 0
                    && i < namedFactories.Count)
                {
                    Console.Write("How much: ");
                    s = Console.ReadLine();
                    if (s != null
                        && int.TryParse(s, out int total)
                        && total > 0)
                    {
                        return namedFactories[i].Item2.Prepare(total);
                    }
                }
                Console.WriteLine("Something went wrong with your input, try again.");

            }
        }
    }
}
