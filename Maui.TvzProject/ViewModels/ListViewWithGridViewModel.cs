using Maui.TvzProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.TvzProject.ViewModels
{
   public class ListViewWithGridViewModel: INotifyPropertyChanged
   {
	  private ObservableCollection<TaskItem> _myObservableCollection;

	  public ListViewWithGridViewModel( List<TaskItem> taskItems )
	  {
		 Tasks = new ObservableCollection<TaskItem>(taskItems);
	  }

	  public ObservableCollection<TaskItem> Tasks
	  {
		 get { return _myObservableCollection; }
		 set
		 {
			_myObservableCollection = value;
			OnPropertyChanged();
		 }
	  }

	  public event PropertyChangedEventHandler PropertyChanged;

	  protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = null ) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
