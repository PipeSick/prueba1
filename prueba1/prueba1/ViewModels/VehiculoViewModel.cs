using Firebase.Database;
using prueba1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba1.ViewModels
{
    public class VehiculoViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        public async Task<VehiculoModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(VehiculoModel) + "/" + id).OnceSingleAsync<VehiculoModel>());
        }
        public async Task<List<VehiculoModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(VehiculoModel)).OnceAsync<VehiculoModel>()).Select(item => new VehiculoModel
            {
                Id = item.Key,
                Patente = item.Object.Patente,
                ConductorId = item.Object.ConductorId,
                AñoFabricacion = item.Object.AñoFabricacion,
            }).ToList();
        }
    }
}
