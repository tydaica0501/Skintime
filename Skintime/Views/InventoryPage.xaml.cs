﻿using System;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reactive.Linq;
using Skintime.Models;

namespace Skintime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public InventoryPage()
        {
            InitializeComponent();
            
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();

        }

        List<Cosmetics> disp1 = new List<Cosmetics>(); //CollectionView itemsource
        List<InventoryCosmetics> invent1 = new List<InventoryCosmetics>(); //Init disp1

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            BlobCache.ApplicationName = "Skintime";
            BlobCache.EnsureInitialized();
            disp1 = new List<Cosmetics>();
            BlobCache.Secure.GetAllObjects<InventoryCosmetics>().Subscribe(X => invent1 = X.ToList());
            List<KetQua> res = await App.Inventorydatabase.GetKeyAsync();

            foreach (InventoryCosmetics a in invent1)
            {
                disp1.Add(a.added);
            }
            Disp1Coll.ItemsSource = disp1; 
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cosmetics = (Cosmetics)e.CurrentSelection.FirstOrDefault();
                var DetailPage = new ProductDetailPage();
                DetailPage.BindingContext = cosmetics;
                await Navigation.PushAsync(DetailPage);
            }

        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await BlobCache.Secure.InvalidateAllObjects<InventoryCosmetics>();
            Disp1Coll.ItemsSource = new List<Cosmetics>();
            await App.Inventorydatabase.DeleteAll();
        }

    }
}
