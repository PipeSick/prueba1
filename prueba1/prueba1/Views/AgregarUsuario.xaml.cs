using prueba1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarUsuario : ContentPage
    {
        public AgregarUsuario()
        {
            InitializeComponent();
            BindingContext = new AgregarUsuarioViewModel();

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());


        }
    }
}