public class Program
{
    static ProductManager productManager = new ProductManager();
    static BuyerManager buyerManager = new BuyerManager();
    static ReceiptManager receiptManager = new ReceiptManager();

    static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n--- Internet Shop ---");
            Console.WriteLine("1. Register New Product");
            Console.WriteLine("2. Add Quantity to Existing Product");
            Console.WriteLine("3. Sell Product");
            Console.WriteLine("4. Register New Buyer");
            Console.WriteLine("5. Show List of Products");
            Console.WriteLine("6. Show List of Buyers");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    productManager.RegisterNewProduct();
                    break;
                case 2:
                    productManager.AddQuantityToProduct();
                    break;
                case 3:
                    productManager.SellProduct(buyerManager, receiptManager);
                    break;
                case 4:
                    buyerManager.RegisterNewBuyer();
                    break;
                case 5:
                    productManager.ShowListOfProducts();
                    break;
                case 6:
                    buyerManager.ShowListOfBuyers();
                    break;
                case 0:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
