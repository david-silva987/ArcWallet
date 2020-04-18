using ArcWallet.Droid;
using ArcWallet.Interfaces;
using System.Runtime.Remoting.Messaging;

/**
 * Génération de toasts avec Xamarin Partie Android
 * https://stackoverflow.com/questions/35279403/toast-equivalent-for-xamarin-forms
 */

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace ArcWallet.Droid
{
    class MessageAndroid : IMessage
    {
        public void Alert(string message)

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