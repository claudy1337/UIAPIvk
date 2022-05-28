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

namespace UIKitTutorials
{
    /// <summary>
    /// Логика взаимодействия для LoadData.xaml
    /// </summary>
    public partial class LoadData : Window
    {
        public LoadData()
        {
            InitializeComponent();
            if (APIKEY.FAKE_TOKEN != null)
            {
                this.Close();
            }
            Method();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public void Method()
        {
           
        }
    }
}
