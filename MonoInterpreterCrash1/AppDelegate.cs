using System.Runtime.InteropServices;
using ObjCRuntime;

namespace MonoInterpreterCrash1;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		method();

		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController ();
		vc.View!.AddSubview (new UILabel (Window!.Frame) {
			BackgroundColor = UIColor.SystemBackground,
			TextAlignment = UITextAlignment.Center,
			Text = "Called P/Invoke method successfully!",
			AutoresizingMask = UIViewAutoresizing.All,
		});
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}

	[DllImport(Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
	private static extern void method(
		IntPtr a = default,
		IntPtr b = default,
		IntPtr c = default,
		IntPtr d = default,
		IntPtr e = default,
		IntPtr f = default,
		IntPtr g = default,
		IntPtr h = default,
		StructMoreThan16Bytes i = default);

	private struct StructMoreThan16Bytes
	{
		private readonly long _;
		private readonly long __;
		private readonly byte ___;
	}
}