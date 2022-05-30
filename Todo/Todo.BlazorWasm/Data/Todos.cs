using Microsoft.JSInterop;

namespace Todo.BlazorWasm.Data
{
    public class Todos
    {
        public Todos(IJSRuntime jsRuntime)
        {
            Do = new TodoAutoSaveCollection_LocalStorage(jsRuntime, "Do");
            Schedule = new TodoAutoSaveCollection_LocalStorage(jsRuntime, "Schedule");
            Delegate = new TodoAutoSaveCollection_LocalStorage(jsRuntime, "Delegate");
            Eliminate = new TodoAutoSaveCollection_LocalStorage(jsRuntime, "Eliminate");
        }

        public async Task InitializeAsync() 
        {
            await Do.LoadAsync();
            await Schedule.LoadAsync();
            await Delegate.LoadAsync();
            await Eliminate.LoadAsync();
        }

        public ITodoAutoSaveCollection Do { get; }
        public ITodoAutoSaveCollection Schedule { get; }
        public ITodoAutoSaveCollection Delegate { get; }
        public ITodoAutoSaveCollection Eliminate { get; }
    }
}
