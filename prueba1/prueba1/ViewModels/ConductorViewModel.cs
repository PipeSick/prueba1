using Firebase.Database;
using prueba1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba1.ViewModels
{
    public class ConductorViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        public async Task<List<ConductorModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(ConductorModel)).OnceAsync<ConductorModel>()).Select(item => new ConductorModel
            {
                Rut = item.Object.Rut,
                Nombres = item.Object.Nombres,
                apellidos = item.Object.apellidos,
                NumeroContacto = item.Object.NumeroContacto,
                Id = item.Key,
            }).ToList();
        }
    }
}
