using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();   
        }
        public async Task<bool> DeleteItemsAsync(string cartid)
        {
            return await _database.KeyDeleteAsync(cartid);
        }

        public async Task<CustomerCart?> GetCartAsync(string cartid)
        {
            var getcart = await _database.StringGetAsync(cartid);
            return getcart.IsNullOrEmpty ?null: JsonSerializer.Deserialize<CustomerCart>(getcart);
        }

        public async Task<CustomerCart?> UpdateCartAsync(CustomerCart customer)
        {
            var createorupdate = await _database.StringSetAsync(customer.Id, JsonSerializer.Serialize(customer), TimeSpan.FromDays(30));
            if (!createorupdate) return null;
                return await GetCartAsync(customer.Id); 
        }
    }
}
