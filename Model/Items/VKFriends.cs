using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIKitTutorials.Model.Items
{
    public class VKFriends
    {
        public class Item
        {
            public int id { get; set; }
            public string domain { get; set; }
            public string bdate { get; set; }
            public LastSeen last_seen { get; set; }
            public string track_code { get; set; }
            public int sex { get; set; }
            public string photo_100 { get; set; }
            public int online { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public bool can_access_closed { get; set; }
            public bool is_closed { get; set; }
        }

        public class LastSeen
        {
            public int platform { get; set; }
            public int time { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public List<Item> items { get; set; }
        }

        public class Root
        {
            public Response response { get; set; }
        }
    }
}
