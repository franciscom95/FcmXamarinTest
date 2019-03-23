using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using FcmXamarinTest;
using FcmXmarinTest.Adapters;
using FcmXmarinTest.Services.DTOs;
using Firebase.Iid;
using Firebase.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FcmXmarinTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       
        static readonly string TAG = "MainActivity";
       
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
       


       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mainPage.mainActivity = this;
            SetContentView(Resource.Layout.activity_main);
            

                IsPlayServicesAvailable();
                CreateNotificationChannel();
                FirebaseMessaging.Instance.SubscribeToTopic("news");
             



            

        }





        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool IsPlayServicesAvailable()
        {
            // return true;
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    // msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                    return true;
                else
                {
                    //  msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                // msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                 return;
 
   



            }
          
                var channel = new NotificationChannel(CHANNEL_ID,
                                                  "FCM Notifications",
                                                  NotificationImportance.Default)
                {

                    Description = "Firebase Cloud Messages appear in this channel"
                };

                var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
                    
              

                notificationManager.CreateNotificationChannel(channel);
           
            
        }
    }

 

}

