using prueba1.Model;
using prueba1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace prueba1.Views.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerTabbedPage : Xamarin.Forms.TabbedPage
    {
        public ContainerTabbedPage(UserModel User)
        {

            InitializeComponent();
            MessagingCenter.Send<Object, string>(this, "MessageKey", User.VehiculoId);
            MessagingCenter.Send<Object, UserModel>(this, "UserMessage", User);
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
            NavigationPage.SetHasNavigationBar(this, false);
        }


    }
}