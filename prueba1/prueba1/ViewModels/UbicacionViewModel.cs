using prueba1.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Linq;

namespace prueba1.ViewModels
{
    public class UbicacionViewModel : BaseViewModel
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");

        public ObservableCollection<LocationModel> getData()
        {
            var UserData = firebaseClient
                .Child(nameof(LocationModel))
                .AsObservable<LocationModel>()
                .AsObservableCollection();
            return UserData;
        }

        public async Task<List<LocationModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(LocationModel)).OnceAsync<LocationModel>()).Select(item => new LocationModel
            {
                Id = item.Key,
                Lat = item.Object.Lat,
                Lng = item.Object.Lng,
                patente = item.Object.patente,
            }).ToList();
        }

        public async Task<LocationModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(LocationModel) + "/" + id).OnceSingleAsync<LocationModel>());
        }








    }
}
