namespace Maui.TvzProject;

using Maui.TvzProject.Database;
using Maui.TvzProject.Models;

public partial class MainPage : ContentPage
{

   List<TaskItem> tasks = new List<TaskItem>();   

   public MainPage(  )
   {
	  InitializeComponent();
	  mainGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
	  mainGrid.ColumnDefinitions.Add(new ColumnDefinition(140));
	  mainGrid.AddRowDefinition(new RowDefinition() {  });
	  mainGrid.Add(new Label
	  {
		 Text = "Task Name",
		 FontSize = 18,
		 Padding= 8,
	  }, 0, 0);
	  mainGrid.Add(new Label
	  {
		 Text = "Date",
		 Padding= 8,
		 FontSize = 18,
	  }, 1, 0);

	  LoadDb();
   }

   private async void LoadDb()
   {
	  tasks = await ( await TasksDatabase.Instance ).GetItemsAsync();
	  foreach (var task in tasks)	  
		 AddToGrid(task);
	  
   }

   private void AddToGrid(TaskItem task)
   {
	  TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
	  tapGestureRecognizer.Tapped += async ( s, e ) =>
	  {
		 await Navigation.PushAsync(new ViewTaskPage(task));
	  };

	  mainGrid.AddRowDefinition(new RowDefinition());	  
	  var labelName = new Label
	  {
		 Text = task.Name,
		 FontSize = 18,
		 Padding= 8,		 
	  };
	  labelName.GestureRecognizers.Add(tapGestureRecognizer);

	  var labelDate = new Label
	  {
		 Text = task.Date.ToString("dd.MM.yyyy HH:mm"),
		 FontSize = 18,
		 Padding= 8,
	  };
	  labelDate.GestureRecognizers.Add(tapGestureRecognizer);
	  

	  mainGrid.Add(labelName, 0, mainGrid.RowDefinitions.Count-1);
	  mainGrid.Add(labelDate, 1, mainGrid.RowDefinitions.Count -1);
   }

   private async void btnNewTask_Clicked( object sender, EventArgs e )
   {
	  
	  await Navigation.PushAsync(new CreateTaskPage(tasks, AddToGrid));

   }
}

