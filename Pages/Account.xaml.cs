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
using System.Collections;
using System.Collections.ObjectModel;
using UIKitTutorials.Model;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Text.Json;
using UIKitTutorials.Pages;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Reflection;
using JsonSerializer = System.Text.Json.JsonSerializer;
using UIKitTutorials.Model.Items;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public Account()
        {
            InitializeComponent();
            Refresh();
        }
        public String ContentAccount
        {
            get { return (String)GetValue(ResponceContentAccount); }
            set { SetValue(ResponceContentAccount, value); }
        }
        public static readonly DependencyProperty ResponceContentAccount =
            DependencyProperty.Register("ContentAccount", typeof(String), typeof(MainWindow));
        public ObservableCollection<VKClientInfo> Members { get; set; }
            = new ObservableCollection<VKClientInfo>();

        public String ContentFriends
        {
            get { return (String)GetValue(ResponceContentFriends); }
            set { SetValue(ResponceContentFriends, value); }
        }
        public static readonly DependencyProperty ResponceContentFriends =
            DependencyProperty.Register("ContentFriends", typeof(String), typeof(MainWindow));
        public ObservableCollection<VKClientInfo> Member { get; set; }
            = new ObservableCollection<VKClientInfo>();
        public async void Refresh()
        {
            ContentAccount = "....";
            ContentFriends = "....";
            var result = await Utility.FetchUserInfo(APIKEY.USER_ID.ToString(), APIKEY.USER_TOKEN);
            var resultFriends = await Utility.FetchGetFriends(APIKEY.USER_TOKEN);
            ContentAccount = Utility.PrettyJson(result.Content);
            ContentFriends = Utility.PrettyJson(resultFriends.Content);
            ResponceUser.Text = ContentAccount;
            ResponceFriends.Text = ContentFriends;
            Members.Clear();
            Member.Clear();
            var user = JsonConvert.DeserializeObject<VKClientInfo.Root>(ResponceUser.Text);
            var friend = JsonConvert.DeserializeObject<VKFriends.Root>(ResponceFriends.Text);
            usrList.ItemsSource = user.response;
            friendsList.ItemsSource = friend.response.items;
        }
    }
    
}
