public class ReceiptManager
{
    private Dictionary<IBuyer, List<IReceipt>> buyerReceipts;

    public ReceiptManager()
    {
        buyerReceipts = new Dictionary<IBuyer, List<IReceipt>>();
    }

    public void AddReceipt(IBuyer buyer, IReceipt receipt)
    {
        if (!buyerReceipts.ContainsKey(buyer))
        {
            buyerReceipts[buyer] = new List<IReceipt>();
        }

        buyerReceipts[buyer].Add(receipt);
    }

    public List<IReceipt> GetReceipts(IBuyer buyer)
    {
        if (buyerReceipts.ContainsKey(buyer))
        {
            return buyerReceipts[buyer];
        }

        return new List<IReceipt>();
    }
}
