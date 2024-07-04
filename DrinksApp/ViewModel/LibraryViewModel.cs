using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DrinksApp.Data;
using DrinksApp.Model;
using DrinksApp.View;
using MonkeyFinder.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DrinksApp.ViewModel
{
    public partial class LibraryViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<Drink> savedDrinks;
        public ObservableCollection<Drink> SavedDrinks
        {
            get => savedDrinks;
            set => SetProperty(ref savedDrinks, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private readonly IDrinksRepository _drinksRepo;

        public LibraryViewModel(IDrinksRepository drinksRepo)
        {
            _drinksRepo = drinksRepo;
            SavedDrinks = new ObservableCollection<Drink>();
            Task.Run(async () => await UpdateDrinks());
        }

        [RelayCommand]
        async Task DeleteAllAsync()
        {
            try
            {
                await _drinksRepo.DeleteAll();
                await UpdateDrinks();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Oops...something happened!", e.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task DeleteDrinkAsync(Drink drink)
        {
            if (drink == null)
                return;

            try
            {
                await _drinksRepo.Delete (drink);
                SavedDrinks.Remove(drink);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task RefreshAsync()
        {
           IsRefreshing = true;
            try
            {
                await UpdateDrinks();
            }
            catch (Exception)
            {
                Console.WriteLine("apa");
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        public async Task UpdateDrinks()
        {
            var drinksFromDb = await _drinksRepo.GetAllDrinks();
            var newDrinksCollection = new ObservableCollection<Drink>(drinksFromDb);
            SavedDrinks = newDrinksCollection;
        }

        [RelayCommand]
        async Task GoToDrinkDetails(Drink drink)
        {
            if (drink == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            {
                {"Drink", drink }
            });
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
