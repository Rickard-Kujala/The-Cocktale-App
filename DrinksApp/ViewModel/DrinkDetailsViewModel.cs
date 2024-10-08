﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                await Refresh();
                await Shell.Current.DisplayAlert("Success", "Drink Added successfully to library!", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("oops...something happened!", e.Message, "OK");
            }
        }

        #region ObservableProperty's

        [ObservableProperty]
        private bool showNotesSection;

        [ObservableProperty]
        private bool showNotes;

        [ObservableProperty]
        private bool showImage;

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

        [ObservableProperty]
        private bool showSaveToLibraryBtn;

        [ObservableProperty]
        private string photoPath;

        [ObservableProperty]
        private string notes;

        #endregion 

        #region RelayCommands

        [RelayCommand]
        private void ToggleNotes()
        {
            ShowNotes = !ShowNotes;
            ToggleNotesBtnText = ShowNotes ? Constants.UpArrowFilePath : Constants.DownArrowFilePath;
        }
        [RelayCommand]
        private void ToggleInstructions()
        {
            ShowInstructions = !ShowInstructions;
            ToggleInstructionsBtnText = ShowInstructions ? Constants.UpArrowFilePath : Constants.DownArrowFilePath;
        }

        [RelayCommand]
        private void ToggleIngredients()
        {
            ShowIngredients = !ShowIngredients;
            ToggleIngredientsBtnText = ShowIngredients ? Constants.UpArrowFilePath : Constants.DownArrowFilePath;
        }
        [RelayCommand]
        private async Task DeletePhoto()
        {
            await _repo.DeletePhoto(Drink.Id);
            ShowImage = false;
            OnPropertyChanged(nameof(ShowImage));
            await Refresh();
        }

       
        [RelayCommand]
        private async Task SaveNotes()
        {
            try
            {
                Drink.Notes = Notes;

                Drink.PhotoPath = PhotoPath;    
                _repo.Update(Drink);
                await Refresh();
                await Shell.Current.DisplayAlert("Success", "Saved changes", "OK");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error updating notes", e.Message, "OK");
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
                    // Save the photo to the device's local storage
                    var savedPhotoPath = await SavePhotoToFileAsync(photo);
                    PhotoPath = savedPhotoPath;
                    OnPropertyChanged(nameof(PhotoPath));
                    ShowImage = true;
                    // Save the photo path to the database
                    //_repo.Update(Drink);
                    //await Refresh();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Handle other exceptions
            }
        }

        #endregion

        private async Task<string> SavePhotoToFileAsync(FileResult photo)
        {
            // Get a writable folder to store the photo
            var localPath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(localPath))
            {
                await stream.CopyToAsync(newStream);
            }

            return localPath;
        }
        
        public async Task Refresh()
        {
            var drinkFromDb = await _repo.GetWithId(Drink.Id);
            ShowIngredients = true;
            ShowInstructions = true;
            ToggleInstructionsBtnText = Constants.UpArrowFilePath;
            ToggleIngredientsBtnText = Constants.UpArrowFilePath;
            ToggleNotesBtnText = Constants.UpArrowFilePath;

            if (drinkFromDb is not null)
            {
                Drink = drinkFromDb;
                ShowSaveToLibraryBtn = false;
                ShowNotesSection = true;
                PhotoPath = Drink.PhotoPath;
                Notes = Drink.Notes;
                if (string.IsNullOrEmpty(Notes)) Notes = "Start typing here to add a note..";
                ShowNotes = true;
                if (!string.IsNullOrEmpty(PhotoPath))
                {
                    ShowImage = true;
                }
                else
                {
                    ShowImage = false;
                }
            }
            else
            {
                ShowSaveToLibraryBtn= true;
                ShowNotesSection= false; 
            }
        }
    }
}
    