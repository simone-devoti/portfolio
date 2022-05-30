using System.Collections.ObjectModel;

namespace Todo.BlazorWasm.Data
{
    public interface ITodoAutoSaveCollection
    {
        public Task LoadAsync();

        public ObservableCollection<string> Todos { get; }
    }
}
