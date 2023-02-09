using CoreLocation;
using Maui.TvzProject.Models;
using UIKit;

namespace Maui.TvzProject.Platforms.IOS
{
   public partial class DeviceOrientationService
    {
	  public DeviceOrientation GetOrientation()
	  {
		 UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;
		 bool isPortrait = orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown;
		 return isPortrait ? DeviceOrientation.Portrait : DeviceOrientation.Landscape;
	  }
   }
}
