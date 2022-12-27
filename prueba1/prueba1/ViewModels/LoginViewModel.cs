using CommunityToolkit.Mvvm.Input;
using prueba1.Model;
using prueba1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using prueba1.Views;
using prueba1.Views.Tabbed;
using System.Collections.ObjectModel;
using System.Linq;
using Firebase.Database;
using System.Security.Cryptography;

namespace prueba1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ObservableCollection<UserModel> _userModels = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserModels
        {
            get { return _userModels; }
            set
            {
                _userModels = value;
                OnPropertyChanged();
            }
        }
        #region Attribute
        public string email;
        public string password;
        public bool isRunning;
        public bool isVisible;
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        #endregion

        #region Properties
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }


        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }



        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                UserModels = getData();
                return new RelayCommand(LoginMethod);
            }
        }
        #endregion

        #region Methods
        public async void LoginMethod()
        {
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el correo.",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar una contraseña.",
                    "Aceptar");
                return;
            }
            try
            {
                var usuarios = await GetAll();

                foreach (var item in usuarios)
                {
                    if (item.Correo.Contains(email))
                    {
                        string passencrypted = GetSHA256(password);
                        if (item.Password.Contains(passencrypted))
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new ContainerTabbedPage(item));
                            break;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                   "Error",
                                   "Usuario o contraseña incorrectos",
                                   "Aceptar");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                       "Error",
                       ex.Message,
                       "Aceptar");
                return;
            }
        }

        public ObservableCollection<UserModel> getData()
        {
            var UserData = firebaseClient
                .Child(nameof(UserModel))
                .AsObservable<UserModel>()
                .AsObservableCollection();
            return UserData;
        }
        public async Task<List<UserModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(UserModel)).OnceAsync<UserModel>()).Select(item => new UserModel
            {
                Nombre = item.Object.Nombre,
                Apellido = item.Object.Apellido,
                Correo = item.Object.Correo,
                Password = item.Object.Password,
                VehiculoId = item.Object.VehiculoId,
                AlumnoRut = item.Object.AlumnoRut,
                Id = item.Key
            }).ToList();
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        #endregion
    }
}
