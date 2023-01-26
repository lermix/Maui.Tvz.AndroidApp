using Maui.TvzProject.Database;
using Maui.TvzProject.Models;
using Plugin.LocalNotification;

namespace Maui.TvzProject;

public partial class CreateTaskPage : ContentPage
{

   Action<TaskItem> AddToGrid;
   private Action addToGrid;

   public CreateTaskPage(  Action<TaskItem> AddToGrid )
   {
	  InitializeComponent();
	  this.AddToGrid = AddToGrid;
	  dpDate.MaximumDate = DateTime.Now.AddYears(5);
	  dpDate.MinimumDate = DateTime.Now;
	  tpTime.Time = DateTime.Now.TimeOfDay;

	  entryName.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 5;
	  entryLocation.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 5;
	  entryDescription.WidthRequest = DeviceDisplay.MainDisplayInfo.Width / 5;
	  borderTimePick.WidthRequest  = DeviceDisplay.MainDisplayInfo.Width / 5;
   }

   private async void btnAdd_Clicked( object sender, EventArgs e )
   {
	  await CreateNotification();

	  var currentItem = GetCurrentTask();

	  currentItem.Id = await ( await TasksDatabase.Instance ).SaveItemAsync(currentItem);

	  AddToGrid(currentItem);


	  await Navigation.PopAsync();
   }

   private TaskItem GetCurrentTask()
   {
	  return ( new TaskItem
	  {
		 Id = 0,
		 Name = entryName.Text,
		 Date = new DateTime(dpDate.Date.Ticks + tpTime.Time.Ticks),
		 Description = entryDescription.Text,
		 Location = entryLocation.Text,
		 Done= false,
	  } );
   }


   private async Task CreateNotification()
   {
	  var request = new NotificationRequest
	  {
		 Title=  entryName.Text,
		 Subtitle = entryLocation.Text,
		 Description = entryDescription.Text,
		 CategoryType = NotificationCategoryType.Event,
		 Schedule = new NotificationRequestSchedule { NotifyTime = dpDate.Date.AddSeconds(tpTime.Time.TotalSeconds) },
		 Image = new NotificationImage { ResourceName = "add.svg" }
	  };

	  await LocalNotificationCenter.Current.Show(request);
   }

   private async void btnCancel_Clicked( object sender, EventArgs e )
   {
	  await Navigation.PopAsync();
   }
}