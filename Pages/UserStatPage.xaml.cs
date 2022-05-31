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
using UIKitTutorials.Model.Items;
using UIKitTutorials.Model;
using UIKitTutorials.Model.BD_Connection;



namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserStatPage.xaml
    /// </summary>
    public partial class UserStatPage : Page
    {
        Model.vkStatEntities bd = new Model.vkStatEntities();
        public UserStatPage()
        {
            InitializeComponent();
            DGUser.ItemsSource = Model.BD_Connection.Connection.bd.RequestHistory.Where(u=> u.User.Login == APIKEY.LOGIN).ToList();
        }

        private void BFull_Click(object sender, RoutedEventArgs e)
        {
            DGUser.ItemsSource = null;
            DGUser.ItemsSource = Model.BD_Connection.Connection.bd.RequestHistory.ToList();
        }

        private void Bdelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGUser.SelectedItem == null)
            {
                MessageBox.Show("select item");
                return;
            }
            bd.RequestHistory.Remove((Model.RequestHistory)DGUser.SelectedItem);
            bd.SaveChanges();
            Refresh();
        }
        public void Refresh()
        {
            DGUser.ItemsSource = null;
            DGUser.ItemsSource = Model.BD_Connection.Connection.bd.RequestHistory.Where(u => u.User.Login == APIKEY.LOGIN).ToList();
        }
    }
}
