using prueba1.Model;
using prueba1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prueba1.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Establecimiento : ContentPage
    {
        EstablecimientoViewModel repositoryEstablecimiento = new EstablecimientoViewModel();
        EstablecimientoModel estable = new EstablecimientoModel();
        string idestablecimiento;
        public Establecimiento()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object, string>(this, "EstablecimientoMessage", (sender, arg) =>
            {
                idestablecimiento = arg;
            });


        }

        protected override async void OnAppearing()
        {

            var establecimi = await repositoryEstablecimiento.GetById(idestablecimiento);
            TxtNombre.Text = establecimi.Nombre;
            TxtDirector.Text = establecimi.Director;
            TxtContacto.Text = establecimi.Contacto;
            TxtDireccion.Text = estable.Direccion;
            base.OnAppearing();
        }


    }
}