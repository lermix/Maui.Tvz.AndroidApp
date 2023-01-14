using Maui.TvzProject.Database;
using Maui.TvzProject.Models;

namespace Maui.TvzProject;

public partial class ViewTaskPage : ContentPage
{
   private TaskItem task;
   public ViewTaskPage( TaskItem task )
   {

	  InitializeComponent();

	  this.task=task;
	  lblName.Prop = "Name:";
	  lblName.Value = task.Name;

	  dpDate.Date = task.Date;
	  tpTime.Time = task.Date.TimeOfDay;

	  lblDescription.Prop = "Description:";
	  lblDescription.Value = task.Description;

	  lblLocation.Prop = "Location:";
	  lblLocation.Value = task.Location;
   }

   private async void chkChecked_CheckedChanged( object sender, CheckedChangedEventArgs e )
   {
	  task.Done = chkChecked.IsChecked;
	  await(await TasksDatabase.Instance).SaveItemAsync(task);

   }
}