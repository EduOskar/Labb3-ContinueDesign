namespace Labb3_ContinueDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var machine = new WarmDrinkMachine();
            IWarmDrink drink = machine.MakeDrink();
            drink.Consume();

            

        }
    }
}