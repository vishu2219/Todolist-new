using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todolist
{
    public class TodoItemDatabase
    {
        static SQLiteAsyncConnection Database;


        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
        {
            var instance = new TodoItemDatabase();
           
            CreateTableResult result1 = await Database.CreateTableAsync<task>();
            CreateTableResult result2 = await Database.CreateTableAsync<subtask>();

            return instance;
            // var product = await Database.Table<TodoItem>().ToListAsync();
        });

        public TodoItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

       

        //////////////////////////////////////////////////////////////////////////////////


        public Task<List<task>> GetItemsAsync1()
        {
            return Database.Table<task>().ToListAsync();
        }
        public Task<List<task>> GetItemsNotDoneAsync1()
        {
            return Database.QueryAsync<task>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<task> GetItemAsync1(int id)
        {
            return Database.Table<task>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync1(task item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync1(task item)
        {
            return Database.DeleteAsync(item);
        }

        internal Task SaveItemAsync1(ImageSource source)
        {
            throw new NotImplementedException();
        }

        public static implicit operator TodoItemDatabase(ObservableCollection<task> v)
        {
            throw new NotImplementedException();
        }



        /////////////subtask///////////////////////////

        //private task _ta;

        //private int TI;
        /*public Task<List<subtask>> GetItemsAsync2()
        {
            /*if (_ta != null)
            {

                TI = _ta.Id;

                return Database.Table<subtask>().Where(i => i.Id == TI).ToListAsync();
                
            }
            return Database.Table<subtask>().ToListAsync();
        }*/

        // private task _ta;

        //sprivate int TI;
        public Task<List<subtask>> GetItemsAsync2(int TaskId)
        {

            /*TI = _ta.Id;
              if (_ta == null)
              {
                  return Database.Table<subtask>().ToListAsync();
              }
              else
              {
                  return Database.Table<subtask>().Where(i => i.taskId == TI).ToListAsync();
              }*/
            //return Database.Table<subtask>().ToListAsync();



            return Database.Table<subtask>().Where(i => i.taskId == TaskId).ToListAsync();
        }
        public Task<List<subtask>> GetItemsNotDoneAsync2()
        {
            return Database.QueryAsync<subtask>("SELECT * FROM [subtask] WHERE [Done] = 0");
        }
        public Task<subtask> GetItemAsync2(int id)
        {
            return Database.Table<subtask>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync2(subtask item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }

        }

        public Task<int> DeleteItemAsync2(subtask item)
        {
            return Database.DeleteAsync(item);
        }

        internal Task SaveItemAsync2(ImageSource source)
        {
            throw new NotImplementedException();
        }

        public static implicit operator TodoItemDatabase(ObservableCollection<subtask> v)
        {
            throw new NotImplementedException();
        }
    }
}