using Foundation;
using UIKit;
using ObjCRuntime;
using System;

namespace FacebookBindings
{
	[BaseType (typeof (NSObject))]
	interface FBSDKAppEvents
	{
		[Static, Export ("activateApp")]
		void ActivateApp ();

		[Static, Export ("logEvent:valueToSum:parameters:")]
		void LogEvent (string eventName, double valueToSum, NSDictionary parameters);
	}

	[BaseType (typeof (NSObject))]
	interface FBSDKApplicationDelegate
	{
		[Static, Export ("sharedInstance")]
		FBSDKApplicationDelegate Current { get; }

		[Export ("application:didFinishLaunchingWithOptions:")]
		bool FinishedLaunching (UIApplication application, NSDictionary launchOptions);

		[Export ("application:openURL:sourceApplication:annotation:")]
		bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);
	}

	[BaseType (typeof (UIButton),
		Delegates=new[] { "WeakDelegate" },
		Events = new[] { typeof (FBSDKLoginButtonDelegate)})]
	interface FBSDKLoginButton
	{
		[Export ("readPermissions")]
		string[] ReadPermissions { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		FBSDKLoginButtonDelegate Delegate { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[Model, Protocol]
	interface FBSDKLoginButtonDelegate
	{
		[Export ("loginButton:didCompleteWithResult:error:"), EventArgs ("LoginResult")]
		void Completed (FBSDKLoginButton button, FBSDKLoginManagerLoginResult result, NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface FBSDKLoginManagerLoginResult
	{
		
	}

	[BaseType (typeof (NSObject))]
	interface FBSDKAccessToken
	{
		[Static, Export ("currentAccessToken")]
		FBSDKAccessToken Current { get; set; }

		[Export ("tokenString")]
		string Token { get; }

		[Export ("permissions")]
		string[] Permissions { get; }
	}

	[BaseType (typeof (NSObject))]
	interface FBSDKLoginManager
	{
		[Export ("logOut")]
		void Logout ();
	}

	[BaseType (typeof (NSObject))]
	interface FBSDKAppInviteContent
	{
		[Export ("initWithAppLinkURL:")]
		IntPtr Constructor (NSUrl url);
	}

	[BaseType (typeof (NSObject),
		Delegates=new[] { "WeakDelegate" },
		Events = new[] { typeof (FBSDKAppInviteDialogDelegate)})]
	interface FBSDKAppInviteDialog
	{
		[Export ("delegate", ArgumentSemantic.Assign)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		FBSDKAppInviteDialogDelegate Delegate { get; set; }

		[Export ("content")]
		FBSDKAppInviteContent Content { get; set; }

		[Export ("canShow")]
		bool CanShow { get; }

		[Export ("show")]
		bool Show ();
	}

	[BaseType (typeof (NSObject))]
	[Model, Protocol]
	interface FBSDKAppInviteDialogDelegate
	{
		[Export ("appInviteDialog:didCompleteWithResults:"), EventArgs ("InviteResult")]
		void Completed (FBSDKAppInviteDialog dialog, NSDictionary result);

		[Export ("appInviteDialog:didFailWithError:"), EventArgs ("InviteError")]
		void Failed (FBSDKAppInviteDialog dialog, NSError error);
	}
}