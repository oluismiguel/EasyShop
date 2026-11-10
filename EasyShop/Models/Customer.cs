using SQLite;

namespace EasyShop.Models;

/// <summary>Entidade relacional (SQLite): cliente com schema fixo.</summary>
public class Customer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [NotNull, Unique, MaxLength(150)]
    public string Email { get; set; } = string.Empty;
}
