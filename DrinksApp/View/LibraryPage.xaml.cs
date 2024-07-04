using DrinksApp.ViewModel;

namespace DrinksApp.View
{
    public partial class LibraryPage : ContentPage
    {
        private LibraryViewModel _viewModel => BindingContext as LibraryViewModel;
        public LibraryPage(LibraryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        protected override async void OnAppearing()
        {
            await _viewModel.RefreshAsync();
        }
    }
}

