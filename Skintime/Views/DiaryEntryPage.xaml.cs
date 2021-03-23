﻿using System;
using Skintime.Models;
using Xamarin.Forms;

namespace Skintime.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DiaryEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public DiaryEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Diary();
        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Diary note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            note.Date = DateTime.UtcNow;
            //List
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Diary)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnNormalButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Azure) (sender as Button).BackgroundColor = Color.Blue;
            else (sender as Button).BackgroundColor = Color.Azure;
        }
        async void OnAcneButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Azure) (sender as Button).BackgroundColor = Color.Blue;
            else (sender as Button).BackgroundColor = Color.Azure;
        }
        async void OnEczemaButtonClicked(object sender, EventArgs e)
        {
            if ((sender as Button).BackgroundColor == Color.Azure) (sender as Button).BackgroundColor = Color.Blue;
            else (sender as Button).BackgroundColor = Color.Azure;
        }
    }
}