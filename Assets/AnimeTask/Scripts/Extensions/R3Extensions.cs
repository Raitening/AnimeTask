#if ANIMETASK_R3_SUPPORT
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace AnimeTask.Extensions
{
    public static class R3Extensions
    {
        public static IDisposable SubscribeTask<T>(this IObservable<T> source, Func<T, CancellationToken, UniTask> taskFunc)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            return source.ToObservable().DoCancelOnCompleted(
                    cancellationTokenSource
                    )
                .Subscribe(x =>
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource = new CancellationTokenSource();
                    taskFunc(x, cancellationTokenSource.Token).Forget();
                });
        }
    }
}
#endif