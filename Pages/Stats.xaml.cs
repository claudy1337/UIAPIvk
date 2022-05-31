using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using UIKitTutorials.vk;
using xNet;
using Newtonsoft.Json.Linq;
using UIKitTutorials.Model.BD_Connection;
using UIKitTutorials.Model;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для Stats.xaml
    /// </summary>
    public partial class Stats : Page
    {
        public Stats()
        {
            InitializeComponent();
            var api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 8165811,
                AccessToken = APIKEY.USER_TOKEN,
                Login = APIKEY.LOGIN,
                Password = APIKEY.PASSWORD,
                Settings = Settings.All
            });
            

        }
    }
}
