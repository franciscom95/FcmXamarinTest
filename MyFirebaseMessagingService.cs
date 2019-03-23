using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Widget;
using FcmXmarinTest;
using FcmXmarinTest.Adapters;
using Firebase.Messaging;
using Newtonsoft.Json;

namespace FcmXamarinTest
{
    

    [Service]
    [IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]

    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
     
        const string TAG = "MyFirebaseMsgService";



  

        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {


                 
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
 
                var body = message.GetNotification().Body;
                 SendNotification(body, message.Data);
            }catch(Exception ex)
            { 
            }
        }

        void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,MainActivity.NOTIFICATION_ID,intent,PendingIntentFlags.OneShot);
            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      //.SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                                      .SetContentTitle("FCM Message")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent); 
            var notificationManager = NotificationManagerCompat.From(this);

            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
 
        
        }


      
    }
}