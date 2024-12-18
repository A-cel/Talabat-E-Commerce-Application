using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface ICartRepository
    {
        Task<bool> DeleteItemsAsync(string cartid);
        Task<CustomerCart?> GetCartAsync(string cartid);
        Task<CustomerCart?> UpdateCartAsync(CustomerCart customer);
    }
}
