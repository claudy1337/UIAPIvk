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
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Enums.Filters;
using UIKitTutorials.Model.Items;
using System.Threading;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для Messanger.xaml
    /// </summary>
    public partial class Messanger : Page
    {
        public Messanger()
        {
            InitializeComponent();
            Refresh();
            int num = 0;
            if (selected_chat != num)
            {
                RefreshDialog();
            }
        }
        public String ResponceContentVisibles
        {
            get { return (String)GetValue(ResponceContentPropertys); }
            set { SetValue(ResponceContentPropertys, value); }
        }
        public String ResponceMessage
        {
            get { return (String)GetValue(ResponceContentMessage); }
            set { SetValue(ResponceContentMessage, value); }
        }
        public String ResponceInfo
        {
            get { return (String)GetValue(ResponceContentInfo); }
            set { SetValue(ResponceContentInfo, value); }
        }
        public String ResponceUser
        {
            get { return (String)GetValue(ResponceContentUser); }
            set { SetValue(ResponceContentUser, value); }
        }

        public static readonly DependencyProperty ResponceContentPropertys =
            DependencyProperty.Register("ResponceContentVisibles", typeof(String), typeof(MainWindow));

        public static readonly DependencyProperty ResponceContentMessage =
            DependencyProperty.Register("ResponceMessage", typeof(String), typeof(MainWindow));

        public static readonly DependencyProperty ResponceContentInfo =
            DependencyProperty.Register("ResponceInfo", typeof(String), typeof(MainWindow));

        public static readonly DependencyProperty ResponceContentUser =
            DependencyProperty.Register("ResponceUser", typeof(String), typeof(MainWindow));
        public ObservableCollection<MessageConversations> Member { get; set; }
            = new ObservableCollection<MessageConversations>();

        public ObservableCollection<MessageConversations> Members { get; set; }
            = new ObservableCollection<MessageConversations>();
        public ObservableCollection<MessageConversations> MemberInformation { get; set; }
            = new ObservableCollection<MessageConversations>();
        public ObservableCollection<MessageConversations> MemberUsers { get; set; }
            = new ObservableCollection<MessageConversations>();
        private void lstvMessageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDialog();
            AddInfoChat();
        }
        public async void AddInfoChat() //
        {
            ResponceInfo = "....";
            var result = await Utility.FetchUserInfo(selected_chat.ToString(), APIKEY.USER_TOKEN);
            ResponceInfo = Utility.PrettyJson(result.Content);
            txtResponceInfo.Text = ResponceInfo;
            MemberInformation.Clear();
            var user = JsonConvert.DeserializeObject<VKClientInfo.Root>(txtResponceInfo.Text);
            infoList.ItemsSource = user.response;
        }
        int selected_chat;
        public async void RefreshDialog()
        {
            var selected = lstvMessageType.SelectedItem as Model.Items.Message.Item;
            selected_chat = selected.conversation.peer.local_id;
            txtResponcemessage.Text = "....";
            var result = await Utility.FetchMessage(APIKEY.USER_TOKEN, selected.ToString());
            ResponceMessage = Utility.PrettyJson(result.prettyMessage);
            txtResponcemessage.Text = ResponceMessage;
            Members.Clear();
            var usr = JsonConvert.DeserializeObject<MessageGet.Root>(txtResponcemessage.Text);
            var reverse = usr.response.items.ToArray().Reverse();
            msg.ItemsSource = reverse;
        }
        public async void Refresh()
        {
            ResponceContentVisibles = "....";
            var result = await Utility.FetchMessageConversations(APIKEY.USER_TOKEN);
            ResponceContentVisibles = Utility.PrettyJson(result.prettyContent);
            txtResponce.Text = ResponceContentVisibles;
            Member.Clear();
            var dialog = JsonConvert.DeserializeObject<Model.Items.Message.Root>(txtResponce.Text);
            lstvMessageType.ItemsSource = dialog.response.items;
        }
        
        private void msgSend_Click(object sender, RoutedEventArgs e)
        {
            if (txtSend.Text == null)
            {
                RefreshDialog();
            }
            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                AccessToken = APIKEY.USER_TOKEN,
                Password = APIKEY.PASSWORD,
                Login = APIKEY.LOGIN,
                ApplicationId = 8165811,
                Settings = Settings.All
            });
            // Получить адрес сервера для загрузки.
            try
            {
                api.Messages.Send(new MessagesSendParams()
                {
                    UserId = selected_chat,
                    PeerId = selected_chat,
                    Message = txtSend.Text,
                    RandomId = new Random().Next()
                });
                RefreshDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ограничение на отправку");
            }
        }
    }
}
