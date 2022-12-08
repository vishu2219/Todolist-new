using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todolist
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Subtask : ContentPage
{
    public Subtask()
    {
        InitializeComponent();
    }
        SQLiteAsyncConnection dataBase;
        protected async override void OnAppearing()
        {


            base.OnAppearing();
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            var item = (task)BindingContext;
            //var s= (subtask)BindingContext;
            var id = item.Id;
            //var id = item.subtasks;
            //subtask.taskId = item.Id;
            /* foreach(var it in id)
             {
                 listView1.ItemsSource = await database.GetItemsAsync2(it.taskId);
             }*/
            listView1.ItemsSource = await database.GetItemsAsync2(item.Id);

            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(basePath, "SQLite.db3");

            dataBase = new SQLiteAsyncConnection(databasePath);
            await dataBase.CreateTableAsync(typeof(subtask));
        }
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new subtaskadd()
            {
                BindingContext = new subtask()
            });
        }
        async void OnClicklist(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new subtaskadd()
                {
                    BindingContext = e.SelectedItem as subtask
                });
            }
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem1 = (task)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.SaveItemAsync1(todoItem1);
            await Navigation.PopAsync();
        }
        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (task)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.DeleteItemAsync1(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
       
    }
}
    
