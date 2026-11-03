using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// Atomic transaction service (collect/return) per CTO MVP spec.
    /// All operations in single DB transaction.
    /// </summary>
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateTransactionAsync(string userId, TransactionRequest request);
        Task<TransactionResponse> CancelTransactionAsync(string userId, string transactionId);
    }
}
