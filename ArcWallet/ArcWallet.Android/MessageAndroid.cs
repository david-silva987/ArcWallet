using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ArcWallet.Droid;
using ArcWallet.Interfaces;

/**
 * Génération de toasts avec Xamarin Partie Android
 * https://stackoverflow.com/questions/35279403/toast-equivalent-for-xamarin-forms
 */

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace ArcWallet.Droid
{
    class MessageAndroid : IMessage
    {
    
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

    }
}