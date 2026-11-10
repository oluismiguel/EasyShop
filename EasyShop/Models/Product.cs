using LiteDB;

namespace EasyShop.Models;

/// <summary>
/// Documento NoSQL (LiteDB): atributos dinâmicos sem schema fixo.
/// Ex.: camisa => {Tamanho: M, Cor: Azul}; notebook => {Processador: i7, RAM: 16GB}.
/// </summary>
public class Product
{
    [BsonId]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? Category { get; set; }

    public Dictionary<string, string> Attributes { get; set; } = new();

    [BsonIgnore]
    public string AttributesText =>
        Attributes.Count == 0
            ? "Sem atributos adicionais"
            : string.Join(" | ", Attributes.Select(a => $"{a.Key}: {a.Value}"));

    [BsonIgnore]
    public string PriceText => Price.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
}
