namespace Maui.TvzProject.Models
{
   public class TaskItem
   {
	  public int Id { get; set; }
	  public string Name { get; set; }
	  public DateTime Date { get; set; }
	  public string Description { get; set; }
	  public string Location { get; set; }
	  public bool Done { get; set; }
   }
}
