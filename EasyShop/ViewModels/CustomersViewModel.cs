using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _service;

    public CustomersViewModel(ICustomerService service)
    {
        _service = service;
        _ = LoadAsync();
    }

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string email = string.Empty;
    [ObservableProperty] private string status = string.Empty;

    public ObservableCollection<Customer> Customers { get; } = new();

    [RelayCommand]
    private async Task SaveAsync()
    {
        var (success, message) = await _service.RegisterAsync(Name, Email);
        Status = message;
        if (!success) return;

        Name = Email = string.Empty;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync(Customer customer)
    {
        await _service.RemoveAsync(customer.Id);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        var list = await _service.ListAsync();
        Customers.Clear();
        foreach (var c in list)
            Customers.Add(c);
    }
}
