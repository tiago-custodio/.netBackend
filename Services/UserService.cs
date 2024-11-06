using MongoDB.Driver;
using DotnetBackend.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetBackend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration["DatabaseSettings:connectionString"]); // Corrigido
            var mongoDatabase = mongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            _usersCollection = mongoDatabase.GetCollection<User>(configuration["DatabaseSettings:UserCollectionName"]); // Corrigido
        }

        public async Task<List<User>> GetUsersAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetUserByIdAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateUserAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateUserAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task DeleteUserAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}