using EasyShop.Models;
using SQLite;

namespace EasyShop.Data;

public interface ICustomerRepository
{
    Task<int> AddAsync(Customer customer);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByEmailAsync(string email);
    Task<int> DeleteAsync(int id);
}

/// <summary>Camada de dados SQL: SQLite via sqlite-net-pcl.</summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly SQLiteAsyncConnection _db;
    private bool _initialized;

    public CustomerRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "customers.db3");
        _db = new SQLiteAsyncConnection(path);
    }

    private async Task InitAsync()
    {
        if (_initialized) return;
        await _db.CreateTableAsync<Customer>();
        _initialized = true;
    }

    public async Task<int> AddAsync(Customer customer)
    {
        await InitAsync();
        return await _db.InsertAsync(customer);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        await InitAsync();
        return await _db.Table<Customer>().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        await InitAsync();
        return await _db.Table<Customer>().Where(c => c.Email == email).FirstOrDefaultAsync();
    }

    public async Task<int> DeleteAsync(int id)
    {
        await InitAsync();
        return await _db.DeleteAsync<Customer>(id);
    }
}
