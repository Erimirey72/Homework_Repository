public interface IReceipt
{
    IProduct Product { get; set; }
    IBuyer Buyer { get; set; }
}
