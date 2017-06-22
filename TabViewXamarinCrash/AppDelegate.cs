using Foundation;
using UIKit;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Push;

namespace TabViewXamarinCrash
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			// Code to start the Xamarin Test Cloud Agent
			// This should come before MobileCenter.Start() is called
			Push.PushNotificationReceived += (sender, e) =>
			{

				// Add the notification message and title to the message
				var summary = $"Push notification received:" +
                    $"\n\tNotification title: {e.Title}" +
						$"\n\tMessage: {e.Message}";

				// If there is custom data associated with the notification,
				// print the entries
				if (e.CustomData != null)
				{
					summary += "\n\tCustom data:\n";
					foreach (var key in e.CustomData.Keys)
					{
						summary += $"\t\t{key} : {e.CustomData[key]}\n";
					}
				}

				// Send the notification summary to debug output
				System.Diagnostics.Debug.WriteLine(summary);
			};
#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
#endif
			MobileCenter.Start("543fedd2-9a56-48c9-91e9-56d0cbfac8c1",
                               typeof(Analytics), typeof(Crashes), typeof(Push));


            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

