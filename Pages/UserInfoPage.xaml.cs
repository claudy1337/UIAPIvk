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
using UIKitTutorials.vk;
using UIKitTutorials.Model.BD_Connection;
using UIKitTutorials.Model.Items;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        public static Model.vkStatEntities bd = new Model.vkStatEntities();
        public UserInfoPage()
        {
            InitializeComponent();
            usrList.ItemsSource = Model.BD_Connection.Connection.bd.User.Where(u => u.Login == APIKEY.LOGIN).ToList();
        }

        private void BShowAll_Click(object sender, RoutedEventArgs e)
        {
            usrList.ItemsSource = null;
            usrList.ItemsSource = Model.BD_Connection.Connection.bd.RequestHistory.ToList();
        }
        private void usrList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = usrList.SelectedItem as Model.User;
            try
            {
                txtLogin.Text = selected.Login;
                txtDomain.Text = selected.Domain;
                txtLastName.Text = selected.LastName;
                txtName.Text = selected.Name;
                Model.RequestHistory hisory = new Model.RequestHistory()
                {
                    DateRequest = DateTime.Now,
                    TypeRequest = "new user auth",
                    idUser = 38,
                    LoginUser = APIKEY.LOGIN
                };
                Model.BD_Connection.Connection.bd.RequestHistory.Add(hisory);
                Model.BD_Connection.Connection.bd.SaveChanges();
            }
            catch (System.NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
