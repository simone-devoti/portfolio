using Microsoft.JSInterop;

namespace Todo.BlazorWasm.Data
{
    public class TodosFactory
    {
        private Todos? _todos;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1,1);
        private readonly IJSRuntime _jSRuntime;

        public TodosFactory(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task<Todos> GetTodosAsync() 
        {
            await _semaphoreSlim.WaitAsync();

            try
            {
                if (_todos is null)
                {
                    var todos = new Todos(_jSRuntime);
                    await todos.InitializeAsync();
                    _todos = todos;
                }

                return _todos;
            }
            finally 
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
