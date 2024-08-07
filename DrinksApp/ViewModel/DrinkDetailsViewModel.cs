using CommunityToolkit.Mvvm.ComponentModel;
using DrinksApp.Model;
using MonkeyFinder.ViewModel;
using CommunityToolkit.Mvvm.Input;
using DrinksApp.Data;

namespace DrinksApp.ViewModel
{
    [QueryProperty(nameof(Drink), "Drink")]

    public partial  class DrinkDetailsViewModel : BaseViewModel
    {
        public DrinkDetailsViewModel(IDrinksRepository repo)
        {
            _repo = repo;
        }
        public bool IsRefreshing { get; set; }

        [ObservableProperty]
        Drink drink;

        private readonly IDrinksRepository _repo;

        [RelayCommand]
        async Task SaveAsync()
        {
            try
            {
                await _repo.Save(Drink); 
                await Shell.Current.DisplayAlert("Success", "Drink Added successfully to library!", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("oops...something happened!", e.Message, "OK");
            }
        }

        [ObservableProperty]
        private bool showNotes;

        [ObservableProperty]
        private bool showInstructions;

        [ObservableProperty]
        private bool showIngredients;

        [ObservableProperty]
        private string toggleInstructionsBtnText;

        [ObservableProperty]
        private string toggleIngredientsBtnText;

        [ObservableProperty]
        private string toggleNotesBtnText;

        //public DrinkDetailsViewModel()
        //{
        //    ShowInstructions = true;
        //    ShowIngredients = true;
        //}
        [RelayCommand]
        private void ToggleNotes()
        {
            ShowNotes = !ShowNotes;
            ToggleNotesBtnText = ShowNotes ? "˄" : "˅";
        }
        [RelayCommand]
        private void ToggleInstructions()
        {
            ShowInstructions = !ShowInstructions;
            ToggleInstructionsBtnText = ShowInstructions ? "˄" : "˅";
        }


        [RelayCommand]
        private void ToggleIngredients()
        {
            ShowIngredients = !ShowIngredients;
            ToggleIngredientsBtnText = ShowIngredients ? "˄" : "˅";
        }
        [RelayCommand]
        private async Task SaveNotes()
        {
            try
            {
                Drink.Notes = Notes;
                _repo.Update(Drink);
                await Shell.Current.DisplayAlert("Success", "Saved changes", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error updating notes", e.Message, "OK");
            }
        }
        private string _notes;

        public string Notes
        {
            get => _notes;
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged();
                }
            }
        }
        [RelayCommand]
        private async Task CapturePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    // Here, you can save the stream to a file or perform other actions
                    CapturedImagePath = photo.FullPath;
                    Console.WriteLine($"Photo captured: {CapturedImagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Capture photo error: {ex.Message}");
            }
        }
        private string _capturedImagePath;
        public string CapturedImagePath
        {
            get => _capturedImagePath;
            set => SetProperty(ref _capturedImagePath, value);
        }
        public async Task Refresh()
        {
            Drink = await _repo.GetWithId(Drink.Id);
        }
    }
}
    