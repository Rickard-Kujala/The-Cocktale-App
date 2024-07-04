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
    protected override void OnAppearing()
    {
        _viewModel.ShowIngredients = true;
        _viewModel.ShowInstructions= true;
        _viewModel.ToggleInstructionsBtnText = "˄";
        _viewModel.ToggleIngredientsBtnText = "˄";
    }
}