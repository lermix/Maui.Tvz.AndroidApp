using Maui.TvzProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.TvzProject.Database
{
   public class TasksDatabase
   {
	  static SQLiteAsyncConnection Database;

	  public TasksDatabase()
	  {
		 Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
	  }

	  public static readonly AsyncLazy<TasksDatabase> Instance =
		 new AsyncLazy<TasksDatabase>(async () =>
		 {
			var instance = new TasksDatabase();
			try
			{
			   CreateTableResult result = await Database.CreateTableAsync<TaskItem>();
			}
			catch (Exception)
			{

			   throw;
			}

			return instance;
		 });

	  async Task Init()
	  {
		 if (Database is not null)
			return;

		 Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
		 var result = await Database.CreateTableAsync<TaskItem>();
	  }
	  public async Task<List<TaskItem>> GetItemsAsync()
	  {
		 await Init();
		 return await Database.Table<TaskItem>().ToListAsync();
	  }

	  public async Task<List<TaskItem>> GetItemsNotDoneAsync()
	  {
		 await Init();
		 return await Database.Table<TaskItem>().Where(t => t.Done).ToListAsync();
	  }

	  public async Task<TaskItem> GetItemAsync( int id )
	  {
		 await Init();
		 return await Database.Table<TaskItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
	  }

	  public async Task<int> SaveItemAsync( TaskItem item )
	  {
		 await Init();
		 if (item.Id != 0)
			 await Database.UpdateAsync(item);
		 else
			 await Database.InsertAsync(item);

		 return item.Id;
	  }

	  public async Task<int> DeleteItemAsync( TaskItem item )
	  {
		 await Init();
		 return await Database.DeleteAsync(item);
	  }

	  public async Task<int> DeleteAll()
	  {
		 await Init();
		 return await Database.DeleteAllAsync<TaskItem>(  );
	  }

	  public async Task<int> DeleteWhere( Func<TaskItem, bool> predicate)
	  {
		 await Init();
		 var items = (await Database.Table<TaskItem>().ToListAsync()).Where( x => predicate(x)).ToArray();
		 for (int i = 0; i < items.Length; i++)
			await Database.DeleteAsync(items[i]);

		 return items.Length;
	  }

   }
}
