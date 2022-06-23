using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;


public class MobileMessagingClient
    {
        private static FirebaseMessaging messaging = null;

        public MobileMessagingClient()
        {
            if (messaging == null)
            {
                var app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("serviceAccountKey.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
                messaging = FirebaseMessaging.GetMessaging(app);
            }
        }

        private Message CreateNotification(string toUser, string chatId, string title, string notificationBody, string token)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            list["toUser"] = toUser;
            list["chatId"] = chatId;
            list["text"] = notificationBody;
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                },
                Data = list
        };
    }

    public async Task SendNotification(string username, string userchat, string token, string title, string body)
        {
            await messaging.SendAsync(CreateNotification(username, userchat, title, body, token));
        }
    }