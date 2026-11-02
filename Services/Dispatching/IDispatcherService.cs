namespace WPFUI.Services.Dispatching;

/// <summary>
/// Service providing thread-safe Dispatcher marshaling to protect UI-bound operations.
/// </summary>
public interface IDispatcherService
{
    /// <summary>
    /// Executes the specified action on the UI thread synchronously.
    /// </summary>
    void Invoke(Action action);

    /// <summary>
    /// Executes the specified action on the UI thread asynchronously.
    /// </summary>
    Task InvokeAsync(Action action);

    /// <summary>
    /// Executes the specified function on the UI thread asynchronously and returns its result.
    /// </summary>
    Task<T> InvokeAsync<T>(Func<T> function);

    /// <summary>
    /// Determines whether the calling thread has access to the UI Dispatcher.
    /// </summary>
    bool CheckAccess();
}
