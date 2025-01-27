namespace receipt_processor_challenge.Models
{
    public class ReceiptData
    {
        public Receipt Receipt { get; set; }
        public int Points { get; set; }

        public ReceiptData(Receipt receipt, int points)
        {
            Receipt = receipt;
            Points = points;
        }
    }
}