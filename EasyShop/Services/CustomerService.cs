using System.Net.Mail;
using EasyShop.Data;
using EasyShop.Models;

namespace EasyShop.Services;

public interface ICustomerService
{
    Task<(bool Success, string Message)> RegisterAsync(string name, string email);
    Task<List<Customer>> ListAsync();
    Task RemoveAsync(int id);
}

/// <summary>Camada de negócio: valida regras e orquestra o repositório SQL.</summary>
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository) => _repository = repository;

    public async Task<(bool Success, string Message)> RegisterAsync(string name, string email)
    {
        name = (name ?? string.Empty).Trim();
        email = (email ?? string.Empty).Trim().ToLowerInvariant();

        if (name.Length < 3 || !name.Contains(' '))
            return (false, "Informe o nome completo (nome e sobrenome).");

        if (!IsValidEmail(email))
            return (false, "E-mail inválido.");

        if (await _repository.GetByEmailAsync(email) is not null)
            return (false, "Já existe um cliente com este e-mail.");

        await _repository.AddAsync(new Customer { Name = name, Email = email });
        return (true, "Cliente cadastrado com sucesso!");
    }

    public Task<List<Customer>> ListAsync() => _repository.GetAllAsync();

    public Task RemoveAsync(int id) => _repository.DeleteAsync(id);

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email && email.Contains('.');
        }
        catch
        {
            return false;
        }
    }
}
