using DrinksApp.ViewModel;

namespace DrinksApp.View;

public partial class DetailsPage : ContentPage
{
    private DrinkDetailsViewModel _viewModel => BindingContext as DrinkDetailsViewModel;

    public DetailsPage(DrinkDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected async override void OnAppearing()
    {
       
        await _viewModel.Refresh();
    }
    
}