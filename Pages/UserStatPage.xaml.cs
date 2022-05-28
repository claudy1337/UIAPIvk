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

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserStatPage.xaml
    /// </summary>
    public partial class UserStatPage : Page
    {
        public UserStatPage()
        {
            InitializeComponent();
            DGCar.ItemsSource = Model.BD_Connection.Connection.bd.User.ToList();
        }
    }
}
