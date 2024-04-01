using DrinksApp.ViewModel;

namespace DrinksApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DrinkDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}