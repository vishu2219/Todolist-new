using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todolist;
using Xamarin.Forms;

namespace Todolist_new
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        SQLiteAsyncConnection dataBase;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            listView1.ItemsSource = await database.GetItemsAsync1();
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(basePath, "SQLite.db3");

            dataBase = new SQLiteAsyncConnection(databasePath);
            await dataBase.CreateTableAsync(typeof(task));

        }
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new taskadd()
            {
                BindingContext = new task()
            });
        }

        async void OnClicklist(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Subtask()
                {
                    BindingContext = e.SelectedItem as task
                });
            }
        }

    }
}