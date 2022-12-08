using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todolist
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class taskadd : ContentPage
    {
        public taskadd()
        {
            InitializeComponent();
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