
using System.Collections.Generic;

namespace CP380_B1_BlockList.Models
{
    public enum TransactionTypes
    {
        BUY, SELL, GRANT
    }

    public class Payload
    {
        // TODO        
        public string User { get; set; }
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public string Item { get; set; }

        public Payload(string user, TransactionTypes t, int amount, string item)
        {
            User = user;
            TransactionType = (int)t;
            Amount = amount;
            Item = item;
        }
    }
}
