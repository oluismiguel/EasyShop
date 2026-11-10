using EasyShop.Models;
using LiteDB;

namespace EasyShop.Data;

public interface IProductRepository
{
    void Add(Product product);
    List<Product> GetAll();
    bool Delete(int id);
}

/// <summary>Camada de dados NoSQL: LiteDB (documentos BSON em arquivo único).</summary>
public class ProductRepository : IProductRepository, IDisposable
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<Product> _collection;

    public ProductRepository()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "products.db");
        // Connection=shared permite múltiplas aberturas seguras no mesmo processo.
        _db = new LiteDatabase($"Filename={path};Connection=shared");
        _collection = _db.GetCollection<Product>("products");
        _collection.EnsureIndex(p => p.Name);
    }

    public void Add(Product product) => _collection.Insert(product);

    public List<Product> GetAll() => _collection.FindAll().OrderBy(p => p.Name).ToList();

    public bool Delete(int id) => _collection.Delete(id);

    public void Dispose() => _db.Dispose();
}
