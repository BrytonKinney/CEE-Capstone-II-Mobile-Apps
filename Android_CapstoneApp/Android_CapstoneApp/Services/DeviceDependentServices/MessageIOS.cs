//using CapstoneApp.Shared.Services.Implementations;
//using CapstoneApp.Shared.Services.Interfaces;
//using Foundation;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using UIKit;

//[assembly: Xamarin.Forms.Dependency(typeof(MessageIOS))]
//namespace CapstoneApp.Shared.Services.Implementations
//{
//    public class MessageIOS : IMessageDisplay
//    {
//        const double LONG_DELAY = 3.5;
//        const double SHORT_DELAY = 2.0;

//        NSTimer alertDelay;
//        UIAlertController alert;

//        public void LongAlert(string message)
//        {
//            ShowAlert(message, LONG_DELAY);
//        }
//        public void ShortAlert(string message)
//        {
//            ShowAlert(message, SHORT_DELAY);
//        }

//        void ShowAlert(string message, double seconds)
//        {
//            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
//            {
//                dismissMessage();
//            });
//            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
//            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
//        }

//        void dismissMessage()
//        {
//            if (alert != null)
//            {
//                alert.DismissViewController(true, null);
//            }
//            if (alertDelay != null)
//            {
//                alertDelay.Dispose();
//            }
//        }
//    }
//}
