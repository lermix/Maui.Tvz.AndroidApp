using Maui.TvzProject.Database;

namespace Maui.TvzProject;

public partial class App : Application
{

	public App(  )
	{
		InitializeComponent();
		MainPage = new NavigationPage( new MainPage() );
	}
}
