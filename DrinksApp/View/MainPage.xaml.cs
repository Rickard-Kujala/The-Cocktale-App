using DrinksApp.ViewModel;

namespace DrinksApp.View
{
    public partial class MainPage : ContentPage
    {
        private DrinksViewModel _viewModel => BindingContext as DrinksViewModel;

        public MainPage(DrinksViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        private void OnAlcoholicFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsAlcoholicFilterEnabled = e.Value;
            }
        }
        private void OnNonAlcoholicFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsNonAlcoholicFilterEnabled = e.Value;
            }
        }
    }

}
