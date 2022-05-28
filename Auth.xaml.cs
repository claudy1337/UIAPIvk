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
using System.Windows.Shapes;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using UIKitTutorials.vk;
using xNet;
using Newtonsoft.Json.Linq;

namespace UIKitTutorials
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void oauth_Click(object sender, RoutedEventArgs e)
        {

            using (var avtoreg = new HttpRequest())
            {
                string danni = avtoreg.Get("https://oauth.vk.com/token?grant_type=password&client_id=2274003&client_secret=hHbZxrka2uZ6jB1inYsH&username=" + txtLogin.Text + "&password=" + txtPassword.Text).ToString(); //отправляем Get запрос 
                dynamic json = JObject.Parse(danni);
                string access_token = json.access_token;
                int user_id = json.user_id;
                APIKEY.select_id = user_id;
                APIKEY.USER_ID = user_id;
                APIKEY.LOGIN = txtLogin.Text;
                APIKEY.PASSWORD = txtPassword.Text;
                APIKEY.USER_TOKEN = access_token;
                APIKEY.ACCES_TOKEN = access_token;
            }
            // System.Diagnostics.Process.Start("https://oauth.vk.com/authorize?client_id=8165811&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52");
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 8165811,
                AccessToken = APIKEY.USER_TOKEN,
                Login = APIKEY.LOGIN,
                Password = APIKEY.PASSWORD,
                Settings = Settings.All
            });
            var p = api.Users.Get(new long[] { APIKEY.USER_ID }).FirstOrDefault();
            if (p == null)
                return;
            
            Model.User user = new Model.User()
            {
                Login = APIKEY.LOGIN,
                Domain = APIKEY.USER_ID.ToString(),
                Name = p.FirstName,
                LastName = p.LastName
            };
            Model.RequestHistory hisory = new Model.RequestHistory()
            {
                DateRequest = DateTime.Now,
                TypeRequest = "auth",
                idUser = user.id,
                LoginUser = APIKEY.LOGIN
            };
            Model.BD_Connection.Connection.bd.User.Add(user);
            Model.BD_Connection.Connection.bd.RequestHistory.Add(hisory);
            Model.BD_Connection.Connection.bd.SaveChanges();

            LoadData loadData = new LoadData();
            loadData.Show();
            MainWindow main = new MainWindow();
            main.Show();
        }

      
    }
}
