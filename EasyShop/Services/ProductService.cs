using System.Globalization;
using EasyShop.Data;
using EasyShop.Models;

namespace EasyShop.Services;

public interface IProductService
{
    (bool Success, string Message) Register(string name, string priceText, string? category, string? attributesText);
    List<Product> List();
    void Remove(int id);
}

/// <summary>Camada de negócio: valida e converte os dados para o documento NoSQL.</summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) => _repository = repository;

    public (bool Success, string Message) Register(string name, string priceText, string? category, string? attributesText)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0)
            return (false, "Informe o nome do produto.");

        var normalized = (priceText ?? string.Empty).Trim().Replace("R$", "").Trim();
        if (!decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out var price)
            && !decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out price))
            return (false, "Preço inválido.");

        if (price <= 0)
            return (false, "O preço deve ser maior que zero.");

        var product = new Product
        {
            Name = name,
            Price = price,
            Category = string.IsNullOrWhiteSpace(category) ? null : category.Trim(),
            Attributes = ParseAttributes(attributesText)
        };

        _repository.Add(product);
        return (true, "Produto cadastrado com sucesso!");
    }

    public List<Product> List() => _repository.GetAll();

    public void Remove(int id) => _repository.Delete(id);

    /// <summary>Converte "Cor=Azul; Tamanho=M" em dicionário (atributos dinâmicos).</summary>
    private static Dictionary<string, string> ParseAttributes(string? text)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(text)) return result;

        foreach (var pair in text.Split(new[] { ';', '\n' }, StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = pair.Split('=', 2);
            if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]))
                result[parts[0].Trim()] = parts[1].Trim();
        }
        return result;
    }
}
