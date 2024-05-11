using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace FiveBabbittGames
{
    public static class AsyncUtils
    {
        // Makes AsyncOperations Awaitable
        public static TaskAwaiter GetAwaiter(this AsyncOperation operation)
        {
            var tcs = new TaskCompletionSource<AsyncOperation>();

            operation.completed += operation => tcs.SetResult(operation);

            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}
