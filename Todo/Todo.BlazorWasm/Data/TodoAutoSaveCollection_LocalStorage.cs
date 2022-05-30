using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;

namespace Todo.BlazorWasm.Data
{
    public class TodoAutoSaveCollection_LocalStorage : ITodoAutoSaveCollection
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly IJSRuntime _js;
        private readonly string _key;

        public TodoAutoSaveCollection_LocalStorage(IJSRuntime js, string key)
        {
            _js = js;
            _key = key;

            Todos = new ObservableCollection<string>();
            Todos.CollectionChanged += Todos_CollectionChanged;
        }

        public ObservableCollection<string> Todos { get; }

        public async Task LoadAsync()
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                var todosString = await _js.InvokeAsync<string>("getFromLocalStorage", _key);

                List<string>? todos = null;

                if (todosString is not null)
                    todos = JsonSerializer.Deserialize<List<string>>(todosString);


                Todos.CollectionChanged -= Todos_CollectionChanged;
                Todos.Clear();

                foreach (var item in todos ?? Enumerable.Empty<string>())
                {
                    Todos.Add(item);
                }

                Todos.CollectionChanged += Todos_CollectionChanged;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private async Task SaveAsync()
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                var todosString = JsonSerializer.Serialize(Todos);
                await _js.InvokeVoidAsync("setToLocalStorage", _key, todosString);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private void Todos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => Task.Run(SaveAsync);
    }
}
