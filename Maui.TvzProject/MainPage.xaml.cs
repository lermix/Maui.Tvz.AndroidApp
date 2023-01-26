namespace Maui.TvzProject;

using Maui.TvzProject.Database;
using Maui.TvzProject.Models;
using Maui.TvzProject.ViewModels;

public partial class MainPage : ContentPage
{

   List<TaskItem> tasks = new List<TaskItem>();   
   
   public MainPage(  )
   {
	  InitializeComponent();
	  BindingContext = new ListViewWithGridViewModel(tasks);	  

	  LoadDb();
   }

   private async void LoadDb()
   {
      tasks = await ( await TasksDatabase.Instance ).GetItemsAsync();
	  foreach (var task in tasks)
		 AddToGrid( task );

   }

   //private void AddToGrid(TaskItem task)
   //{
	  //TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
	  //tapGestureRecognizer.Tapped += async ( s, e ) =>
	  //{
		 //
	  //};

	  //mainGrid.AddRowDefinition(new RowDefinition());	  
	  //var labelName = new Label
	  //{
		 //Text = task.Name,
		 //FontSize = 18,
		 //Padding= 8,		 
	  //};
	  //labelName.GestureRecognizers.Add(tapGestureRecognizer);

	  //var labelDate = new Label
	  //{
		 //Text = task.Date.ToString("dd.MM.yyyy HH:mm"),
		 //FontSize = 18,
		 //Padding= 8,
	  //};
	  //labelDate.GestureRecognizers.Add(tapGestureRecognizer);
	  

	  //mainGrid.Add(labelName, 0, mainGrid.RowDefinitions.Count-1);
	  //mainGrid.Add(labelDate, 1, mainGrid.RowDefinitions.Count -1);
   //}

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
   }

   private async void ListView_ItemSelected( object sender, ItemTappedEventArgs e ) =>
		await Navigation.PushAsync(new ViewTaskPage( FindInGrid((( TaskItem) e.Item).Id), RemoveFromGrid ) );

}

