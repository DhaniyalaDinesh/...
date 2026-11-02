using System.Windows;
using System.Windows.Threading;

namespace WPFUI.Services.Dispatching;

/// <summary>
/// Default implementation of IDispatcherService backed by WPF Application.Current.Dispatcher.
/// </summary>
public class DispatcherService : IDispatcherService
{
    private Dispatcher Dispatcher => Application.Current?.Dispatcher ?? Dispatcher.CurrentDispatcher;

    public void Invoke(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (CheckAccess())
        {
            action();
        }
        else
        {
            Dispatcher.Invoke(action);
        }
    }

    public Task InvokeAsync(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        if (CheckAccess())
        {
            action();
            return Task.CompletedTask;
        }

        return Dispatcher.InvokeAsync(action).Task;
    }

    public Task<T> InvokeAsync<T>(Func<T> function)
    {
        ArgumentNullException.ThrowIfNull(function);

        if (CheckAccess())
        {
            return Task.FromResult(function());
        }

        return Dispatcher.InvokeAsync(function).Task;
    }

    public bool CheckAccess() => Dispatcher.CheckAccess();
}
