namespace BitcoinWalletManager
{
    using System;

    public class Transaction : IComparable<Transaction>
    {
        public string Hash { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int Value { get; set; }

        public int Nonce { get; set; }

        public int CompareTo(Transaction other)
        {
            return this.Nonce.CompareTo(other.Nonce);
        }
    }
}
