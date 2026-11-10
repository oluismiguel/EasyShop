using EasyShop.ViewModels;

namespace EasyShop.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
