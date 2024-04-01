using DrinksApp.ViewModel;

namespace DrinksApp.View
{
    public partial class LibraryPage : ContentPage
    {
        public LibraryPage(LibraryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

