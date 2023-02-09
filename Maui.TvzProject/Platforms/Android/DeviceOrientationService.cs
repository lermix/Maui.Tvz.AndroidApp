#if ANDROID
using Android.Content;
using Android.Runtime;
using Android.Views;
#endif
using Maui.TvzProject.Models;

namespace Maui.TvzProject.Platforms.Android
{
    public partial class DeviceOrientationService
    {
	  public DeviceOrientation GetOrientation()
	  {
		 IWindowManager windowManager = global::Android.App.Application.Context.GetSystemService( Context.WindowService ).JavaCast<IWindowManager>();
		 SurfaceOrientation orientation = windowManager.DefaultDisplay.Rotation;
		 bool isLandscape = orientation == SurfaceOrientation.Rotation90 || orientation == SurfaceOrientation.Rotation270;
		 return isLandscape ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
	  }
   }
}
