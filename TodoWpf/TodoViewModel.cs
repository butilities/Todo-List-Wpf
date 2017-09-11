using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TodoWpf
{
    class TodoViewModel : BindableBase
    {
        private ObservableCollection<Todo> _todos = new ObservableCollection<Todo>();
        private Todo _selectedTodo;

        public TodoViewModel()
        {
            Load();
        }
        public ObservableCollection<Todo> Todos
        {
            get { return _todos; }
            set { SetProperty(ref _todos, value); }
        }

        public Todo SelectedTodo
        {
            get { return _selectedTodo; }
            set
            {
                SetProperty(ref _selectedTodo, value);
                Edit(value);
            }
        }

        internal void AddTask(string text)
        {
            var todo = new Todo
            {
                Name = text
            };
            Todos.Add(todo);
            AddTaskToDB(todo);
        }

        private void Load()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Todo>("todos");


                foreach (var item in col.FindAll())
                {
                    Todos.Add(item);
                }


            }
        }

        public void Edit(Todo todo)
        {
            if(todo != null)
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    // Get customer collection
                    var col = db.GetCollection<Todo>("todos");
                    var temp = col.FindOne(x => x.Id == todo.Id);
                    temp.Name = todo.Name;
                    temp.Done = todo.Done;
                    col.Update(temp);
                    //  MessageBox.Show(temp.Name + todo.Name);
                }
            }
           
        }

        private void AddTaskToDB(Todo todo)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Todo>("todos");
                col.Insert(todo);


            }
        }

        internal void Delete()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Todo>("todos");
                col.Delete(x => x.Id == SelectedTodo.Id);
                Todos.Remove(SelectedTodo);
                //  MessageBox.Show(temp.Name + todo.Name);
            }
        }
    }
}
