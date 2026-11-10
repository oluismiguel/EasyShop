using EasyShop.ViewModels;

namespace EasyShop.Views;

public partial class CustomersPage : ContentPage
{
    public CustomersPage(CustomersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
