using Firebase.Database;
using prueba1.Model;
using prueba1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace prueba1.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        string locationid = "default";
        string Latit;
        string Longi;
        string conductorRut;
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        UbicacionViewModel repository = new UbicacionViewModel();
        VehiculoViewModel repositoyVehiculo = new VehiculoViewModel();
        ConductorViewModel repositoryConductor = new ConductorViewModel();
        private string VehiculoId;
        private Pin pinRoute = new Pin
        {
            Label = "Ubicación del transporte"
        };

        public Inicio()
        {
            InitializeComponent();
            MyMap.Pins.Add(pinRoute);
            MessagingCenter.Subscribe<object, string>(this, "MessageKey", (sender, arg) =>
            {
                VehiculoId = arg;
            });
            ponerpins();
            GetDatosPrincipal(VehiculoId);
        }

        public async void ponerpins()
        {
            var l2 = await repository.GetAll();
            foreach (var item in l2)
            {
                if (item.patente == VehiculoId)
                {
                    locationid = item.Id;
                    break;
                }
            }
            var ubiii = await repository.GetById(locationid);
            Latit = ubiii.Lat;
            Longi = ubiii.Lng;
            Position position = new Position(-35.11428, -71.28232);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.8));
            MyMap.MoveToRegion(mapSpan);
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                pinRoute.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(Latit), Convert.ToDouble(Longi));
                actualizarUbicacion(locationid);
                return true;
            });        
        }
        public async void actualizarUbicacion(string id)
        {
            var ubi2 = await repository.GetById(id);
            Latit = ubi2.Lat;
            Longi = ubi2.Lng;
        }

        public async void GetDatosPrincipal(string id)
        {
            var vehiculo = await repositoyVehiculo.GetAll();
            foreach (var item in vehiculo)
            {
                if (item.Patente == VehiculoId)
                {
                    conductorRut = item.ConductorId;
                    break;
                }

            }
            var conductores = await repositoryConductor.GetAll();
            foreach (var item in conductores)
            {
                if (item.Rut == conductorRut)
                {
                    var nombres = item.Nombres + " " + item.apellidos;
                    var contacto = item.NumeroContacto;
                    n1.Text = nombres;
                    LabelContacto.Text = contacto;
                }
            }
            PatenteTxt.Text = VehiculoId;
        }


    }
}
