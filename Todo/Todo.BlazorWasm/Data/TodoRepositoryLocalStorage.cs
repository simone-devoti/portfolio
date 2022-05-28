using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Todo.BlazorWasm.Data
{
    public class TodoRepositoryLocalStorage : ITodoRepository
    {
        private readonly object _saveLock = new object();

        public TodoRepositoryLocalStorage()
        {
            Todos = new ObservableCollection<Todo>();
            Load();
            Todos.CollectionChanged += Todos_CollectionChanged;
        }

        public ObservableCollection<Todo> Todos { get; set; }

        private void Load() 
        {
            lock (_saveLock)
            {
                //...
            }
        }

        private void Save() 
        {
            lock (_saveLock)
            {
                //...
            }
        }

        private void Todos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) 
            => Task.Run(Save);
    }
}
