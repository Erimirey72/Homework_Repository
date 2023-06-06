public class ProductManager
{
    public List<IProduct> Products { get; set; }

    public ProductManager()
    {
        Products = new List<IProduct>();
    }

    public void RegisterNewProduct()
    {
        Console.WriteLine("\nRegistering a new product...");

        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Enter product quantity: ");
        int quantity = int.Parse(Console.ReadLine());

        IProduct product = new Product
        {
            Name = name,
            Price = price,
            Quantity = quantity
        };

        Products.Add(product);

        Console.WriteLine("Product registered successfully.");
    }

    public void AddQuantityToProduct()
    {
        Console.WriteLine("\nAdding quantity to an existing product...");

        Console.Write("Enter product name: ");
        string name = Console.ReadLine();

        Console.Write("Enter quantity to add: ");
        int quantity = int.Parse(Console.ReadLine());

        IProduct product = Products.Find(p => p.Name.Equals(name));

        if (product != null)
        {
            if (product is IQuantity quantityProduct)
            {
                quantityProduct.Quantity += quantity;
                Console.WriteLine("Quantity added successfully.");
            }
            else
            {
                Console.WriteLine("The product does not support quantity management.");
            }
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void SellProduct(BuyerManager buyerManager, ReceiptManager receiptManager)
    {
        Console.WriteLine("\nSelling a product...");

        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();

        Console.Write("Enter buyer name: ");
        string buyerName = Console.ReadLine();

        IProduct product = Products.Find(p => p.Name.Equals(productName));
        IBuyer buyer = buyerManager.GetBuyerByName(buyerName);

        if (product != null && buyer != null)
        {
            if (product is IQuantity quantityProduct)
            {
                if (quantityProduct.Quantity > 0)
                {
                    quantityProduct.Quantity--;
                    IReceipt receipt = new Receipt
                    {
                        Product = product,
                        Buyer = buyer
                    };
                    receiptManager.AddReceipt(buyer, receipt);
                    Console.WriteLine("Product sold successfully.");
                }
                else
                {
                    Console.WriteLine("Product out of stock.");
                }
            }
            else
            {
                Console.WriteLine("The product cannot be sold as it does not support quantity management.");
            }
        }
        else
        {
            Console.WriteLine("Product or buyer not found.");
        }
    }

    public void ShowListOfProducts()
    {
        Console.WriteLine("\nList of Products:");

        foreach (var product in Products)
        {
            Console.WriteLine($"Name: {product.Name} | Price: {product.Price} | Quantity: {GetQuantity(product)}");
        }
    }

    private int GetQuantity(IProduct product)
    {
        if (product is IQuantity quantityProduct)
        {
            return quantityProduct.Quantity;
        }
        return 0;
    }
}
