public class Receipt : IReceipt
{
    public IProduct Product { get; set; }
    public IBuyer Buyer { get; set; }
}
