using DrinksApp.ViewModel;

namespace DrinksApp.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(DrinksViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }

}
