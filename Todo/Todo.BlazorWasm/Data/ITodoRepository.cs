using System.Collections.ObjectModel;

namespace Todo.BlazorWasm.Data
{
    public interface ITodoRepository
    {
        public ObservableCollection<Todo> Todos { get; set; }
    }
}
