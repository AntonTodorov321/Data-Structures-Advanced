namespace BitcoinWalletManager
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class WalletManager : IWalletManager
    {
        private Dictionary<string, Transaction> pendingTransactionsHash;
        private Dictionary<string, Transaction> executedTransactions;
        private Dictionary<string, SortedSet<Transaction>> transactionsByUserNonce;

        public WalletManager()
        {
            this.pendingTransactionsHash = new Dictionary<string, Transaction>();
            this.executedTransactions = new Dictionary<string, Transaction>();
            this.transactionsByUserNonce = new Dictionary<string, SortedSet<Transaction>>();
        }

        public void AddTransaction(Transaction transaction)
        {
            this.pendingTransactionsHash.Add(transaction.Hash, transaction);

            if (!this.transactionsByUserNonce.ContainsKey(transaction.From))
            {
                this.transactionsByUserNonce[transaction.From] = new SortedSet<Transaction>();
            }

            this.transactionsByUserNonce[transaction.From].Add(transaction);
        }

        public Transaction BroadcastTransaction(string txHash)
        {
            if (!this.pendingTransactionsHash.ContainsKey(txHash))
            {
                throw new ArgumentException();
            }

            var transaction = this.pendingTransactionsHash[txHash];

            this.pendingTransactionsHash.Remove(txHash);
            this.transactionsByUserNonce[transaction.From].Remove(transaction);

            this.executedTransactions.Add(txHash, transaction);

            return transaction;
        }

        public Transaction CancelTransaction(string txHash)
        {
            if (!this.pendingTransactionsHash.ContainsKey(txHash))
            {
                throw new ArgumentException();
            }

            var transaction = this.pendingTransactionsHash[txHash];

            this.pendingTransactionsHash.Remove(txHash);
            this.transactionsByUserNonce[transaction.From].Remove(transaction);

            return transaction;
        }

        public bool Contains(string txHash)
        {
            return this.pendingTransactionsHash.ContainsKey(txHash);
        }

        public int GetEarliestNonceByUser(string user)
        {
            return this.transactionsByUserNonce[user].First().Nonce;
        }

        public IEnumerable<Transaction> GetHistoryTransactionsByUser(string user)
        {
            return this.executedTransactions.Values.Where(t => t.From == user);
        }

        public IEnumerable<Transaction> GetPendingTransactionsByUser(string user)
        {
            return this.transactionsByUserNonce[user];
        }
    }
}
