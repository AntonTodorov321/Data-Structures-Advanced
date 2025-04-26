namespace BitcoinWalletManager
{
    using System.Collections.Generic;

    public interface IWalletManager
    {
        void AddTransaction(Transaction transaction);

        Transaction CancelTransaction(string txHash);

        Transaction BroadcastTransaction(string txHash);
        
        bool Contains(string txHash);

        int GetEarliestNonceByUser(string user);

        IEnumerable<Transaction> GetPendingTransactionsByUser(string user);

        IEnumerable<Transaction> GetHistoryTransactionsByUser(string user);
    }
}
