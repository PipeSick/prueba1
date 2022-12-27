using Firebase.Database;
using prueba1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prueba1.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        public string VehiculoId;
        public string latitud;
        public string longitud;



    }
}
