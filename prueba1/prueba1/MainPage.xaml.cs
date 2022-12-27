using prueba1.ViewModels;
using prueba1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace prueba1
{
    
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
            NavigationPage.SetHasBackButton(this, false);

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarUsuario());


        }
    }
}
