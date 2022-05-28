﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Text.Json;
using UIKitTutorials.Model.Items;
using UIKitTutorials.Model;
using System.Web;

namespace UIKitTutorials.vk
{
    public class Responce<T>
    {
        public String rawContent { get; set; }
        public T responce { get; set; }
    }
    public class ResponceUser<T>
    {
        public String Content { get; set; }
        public T responceUser { get; set; }
    }
    public class ResponceConversations<T>
    {
        public String prettyContent { get; set; }
        public T responceConversations { get; set; }
    }
    public class ResponceMessage<T>
    {
        public String prettyMessage { get; set; }
        public T responceMessage { get; set; }
    }
    public class ResponceChat<T>
    {
        public String ContentChat { get; set; }
        public T responceChat { get; set; }
    }
    public class Utility
    {
        private static HttpClient client = new HttpClient();


        public static Task<HttpResponseMessage> VKGet(String method, Dictionary<String, String> param)
        {
            var builder = new UriBuilder($"https://api.vk.com/method/{method}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_token"] = APIKEY.ACCES_TOKEN;
            query["v"] = APIKEY.Version;
            foreach (var key in param.Keys)
            {
                query[key] = param[key];
            }
            builder.Query = query.ToString();
            String url = builder.ToString();
            return client.GetAsync(url);

        }


        public static string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);
            return JsonSerializer.Serialize(jsonElement, options);
        }



        public static async Task<Responce<Model.Items.VKDictResponce<VKItemsResponse<VKGroupMember>>>> FetchMembersInfo(String groupId, string count)
        {
            HttpResponseMessage response = await VKGet("groups.getMembers", new Dictionary<string, string>
            {
                ["group_id"] = groupId,
                ["fields"] = "title , exports , last_seen , bdate , can_write_private_message , domain , online , sex , personal , photo_100 , user_id , photo_200",
                ["count"] = count,
                ["lang"] = "ru"
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<VKDictResponce<VKItemsResponse<VKGroupMember>>>(content);
            return new Responce<VKDictResponce<VKItemsResponse<VKGroupMember>>>
            {
                responce = itemsResponce,
                rawContent = content
            };
        }

        public static async Task<ResponceUser<VKUserResponce<VKClientInfo>>> FetchUserInfo(String userId, String access_token)
        {
            HttpResponseMessage response = await VKGet("users.get", new Dictionary<string, string>
            {
                ["access_token"] = access_token,
                ["user_id"] = userId,
                ["fields"] = "lists , is_no_index , followers_count , counters , can_be_invited_group , has_photo , wall_default , last_seen , bdate , domain , photo_100 , sex, counters"
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<ResponceUser<VKUserResponce<VKClientInfo>>>(content);
            return new ResponceUser<VKUserResponce<VKClientInfo>>
            {
                Content = content
            };
        }
        public static async Task<ResponceUser<VKUserResponce<VKGroupMember>>> FetchGetFriends(String access_token)
        {
            HttpResponseMessage response = await VKGet("friends.get", new Dictionary<string, string>
            {
                ["access_token"] = access_token,
                ["fields"] = "last_seen , bdate , domain , online , photo_100 , sex , first_name , last_name , id"
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<ResponceUser<VKUserResponce<VKGroupMember>>>(content);
            return new ResponceUser<VKUserResponce<VKGroupMember>>
            {
                Content = content
            };
        }
        public static async Task<ResponceConversations<VKDictResponce<VKItemsResponse<MessageConversations>>>> FetchMessageConversations(String token)
        {
            HttpResponseMessage response = await VKGet("messages.getConversations", new Dictionary<string, string>
            {
                ["access_token"] = token
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<VKDictResponce<VKItemsResponse<MessageConversations>>>(content);
            return new ResponceConversations<VKDictResponce<VKItemsResponse<MessageConversations>>>
            {
                responceConversations = itemsResponce,
                prettyContent = content
            };
        }
        public static async Task<ResponceChat<VKUserResponce<VKChatResponce>>> FetchGetChat(String access_token, String chat_id)
        {
            HttpResponseMessage response = await VKGet("messages.getChat", new Dictionary<string, string>
            {
                ["access_token"] = access_token,
                ["chat_id"] = chat_id
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<ResponceChat<VKUserResponce<VKChatResponce>>>(content);
            return new ResponceChat<VKUserResponce<VKChatResponce>>
            {
                ContentChat = content
            };

        }
        public static async Task<ResponceMessage<VKDictResponce<VKItemsResponse<MessageGet>>>> FetchMessage(String access_token, String user_id)
        {
            HttpResponseMessage response = await VKGet("messages.getHistory", new Dictionary<string, string>
            {
                ["access_token"] = access_token,
                ["user_id"] = user_id,
                ["count"] = "200"
            });
            var content = await response.Content.ReadAsStringAsync();
            var itemsResponce = JsonSerializer.Deserialize<VKDictResponce<VKItemsResponse<MessageGet>>>(content);
            return new ResponceMessage<VKDictResponce<VKItemsResponse<MessageGet>>>
            {
                responceMessage = itemsResponce,
                prettyMessage = content
            };
        }

        internal void MessagesSend(string uSER_ID, string v)
        {
            throw new NotImplementedException();
        }
    }
}
