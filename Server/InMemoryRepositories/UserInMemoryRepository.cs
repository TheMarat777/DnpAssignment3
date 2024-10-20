using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        _ = AddAsync(new User("marat", "1234")).Result;
        _ = AddAsync(new User("patrik", "4321")).Result;
        _ = AddAsync(new User("tomas", "1243")).Result;
        _ = AddAsync(new User("sebo", "2143")).Result;
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public async Task<User> UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        
        await Task.CompletedTask;
        return user;
    }

    public Task DeleteAsync(int id)
    {
        var userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return Task.FromResult(user);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        await Task.CompletedTask;
        return users.SingleOrDefault(u => u.Username == username);
    }

    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable(); 
    }
}