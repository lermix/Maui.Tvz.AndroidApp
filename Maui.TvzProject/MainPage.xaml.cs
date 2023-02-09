namespace Maui.TvzProject;
using Maui.TvzProject.Database;
using Maui.TvzProject.Models;
#if ANDROID
using Maui.TvzProject.Platforms.Android;
#endif
using Maui.TvzProject.ViewModels;

public partial class MainPage : ContentPage
{

   List<TaskItem> tasks = new List<TaskItem>();   
   
   public MainPage(  )
   {
	  InitializeComponent();
	  BindingContext = new ListViewWithGridViewModel(tasks);
#if ANDROID
	  DeviceOrientationService orientationService = new DeviceOrientationService();
	  lblOrientation.Text = orientationService.GetOrientation().ToString();
#endif
	  LoadDb();
   }

   private async void LoadDb()
   {
      tasks = await ( await TasksDatabase.Instance ).GetItemsAsync();
	  foreach (var task in tasks)
		 AddToGrid( task );

   }

   private void AddToGrid(TaskItem taskItem) => ( (ListViewWithGridViewModel) BindingContext ).Tasks.Add(taskItem);
   private void RemoveFromGrid(TaskItem taskItem) => ( (ListViewWithGridViewModel) BindingContext ).Tasks.Remove( taskItem );
   private TaskItem? FindInGrid(int Id) => ( (ListViewWithGridViewModel) BindingContext ).Tasks.FirstOrDefault( x => x.Id == Id ); 
   private async void btnNewTask_Clicked( object sender, EventArgs e ) => await Navigation.PushAsync(new CreateTaskPage( AddToGrid));

   private async void CheckBox_CheckedChanged( object sender, CheckedChangedEventArgs e )
   {	 	  
	  var itemId = ((int)( (CheckBox) sender ).BindingContext);
	  if ( itemId == 0 ) return;
	  var found = FindInGrid( itemId );
	  if ( found == null ) return;
	  found.Done = e.Value;
	  await ( await TasksDatabase.Instance ).SaveItemAsync( found );

#if ANDROID
	  DeviceOrientationService orientationService = new DeviceOrientationService();
	  lblOrientation.Text = orientationService.GetOrientation().ToString();
#endif
   }

   private async void ListView_ItemSelected( object sender, ItemTappedEventArgs e ) =>
		await Navigation.PushAsync(new ViewTaskPage( FindInGrid((( TaskItem) e.Item).Id), RemoveFromGrid ) );

}

