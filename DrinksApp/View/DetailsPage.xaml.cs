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
        _viewModel.ShowIngredients = true;
        _viewModel.ShowInstructions = true;
        _viewModel.ShowNotes = true;
        _viewModel.ToggleInstructionsBtnText = "˄";
        _viewModel.ToggleIngredientsBtnText = "˄";
        _viewModel.ToggleNotesBtnText = "˄";
        await _viewModel.Refresh();
    }
    
}