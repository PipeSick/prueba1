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
    public partial class Perfil : ContentPage
    {
        AlumnoViewModel repositoryAlumno = new AlumnoViewModel();
        EstablecimientoViewModel repositoryEstablecimiento = new EstablecimientoViewModel();
        string idEstablecimiento;
        public UserModel Usuario = new UserModel();
        public Perfil()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            MessagingCenter.Subscribe<object, UserModel>(this, "UserMessage", (sender, arg) =>
            {
                Usuario = arg;
            });

            cargarTxt();
        }

        public async void cargarTxt()
        {

            var alumno = await repositoryAlumno.GetAll();
            foreach (var item in alumno)
            {
                if (item.RutAlu == Usuario.AlumnoRut)
                {
                    TxtAlumnoNombre.Text = item.Nombre;
                    TxtAlumnoApellido.Text = item.Apellidos;
                    TxtRut.Text = item.RutAlu;
                    idEstablecimiento = item.EstableciminetoId;
                }
            }

            var establecimiento = await repositoryEstablecimiento.GetById(idEstablecimiento);

            TxtEstablecimiento.Text = establecimiento.Nombre;
            TxtNombre.Text = Usuario.Nombre;
            TxtApellido.Text = Usuario.Apellido;
            TxtCorreo.Text = Usuario.Correo;

            MessagingCenter.Send<Object, string>(this, "EstablecimientoMessage", idEstablecimiento);



        }
    }
}