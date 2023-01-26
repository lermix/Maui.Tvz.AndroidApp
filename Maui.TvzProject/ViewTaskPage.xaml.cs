using Maui.TvzProject.Database;
using Maui.TvzProject.Models;

namespace Maui.TvzProject;

public partial class ViewTaskPage : ContentPage
{
   private TaskItem task;
   private Action<TaskItem> removeFromGrid;

   public ViewTaskPage( TaskItem task, Action<TaskItem> removeFromGrid)
   {

	  InitializeComponent();

	  this.task=task;
	  this.removeFromGrid = removeFromGrid;

	  lblName.Prop = "Name:";
	  lblName.Value = task.Name;

	  dpDate.Date = task.Date;
	  tpTime.Time = task.Date.TimeOfDay;

	  lblDescription.Prop = "Description:";
	  lblDescription.Value = task.Description;

	  lblLocation.Prop = "Location:";
	  lblLocation.Value = task.Location;

	  chkChecked.IsChecked = task.Done;

	  btnDelte.Margin = new Thickness( 0, DeviceDisplay.MainDisplayInfo.Height / 10-80, 0, 0 );
   }



   private async void chkChecked_CheckedChanged( object sender, CheckedChangedEventArgs e )
   {
	  task.Done = chkChecked.IsChecked;
	  await(await TasksDatabase.Instance).SaveItemAsync(task);
   }

   private async void Button_Clicked(object sender, EventArgs e)
   {
	   await(await TasksDatabase.Instance).DeleteItemAsync(task);
	  removeFromGrid(task);
	  await Navigation.PopAsync();
   }
}