using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyShop.Models;
using EasyShop.Services;

namespace EasyShop.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly IProductService _service;

    public ProductsViewModel(IProductService service)
    {
        _service = service;
        Load();
    }

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string price = string.Empty;
    [ObservableProperty] private string category = string.Empty;
    [ObservableProperty] private string attributes = string.Empty;
    [ObservableProperty] private string status = string.Empty;

    public ObservableCollection<Product> Products { get; } = new();

    [RelayCommand]
    private void Save()
    {
        var (success, message) = _service.Register(Name, Price, Category, Attributes);
        Status = message;
        if (!success) return;

        Name = Price = Category = Attributes = string.Empty;
        Load();
    }

    [RelayCommand]
    private void Delete(Product product)
    {
        _service.Remove(product.Id);
        Load();
    }

    [RelayCommand]
    private void Load()
    {
        Products.Clear();
        foreach (var p in _service.List())
            Products.Add(p);
    }
}
