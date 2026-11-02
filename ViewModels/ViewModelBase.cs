using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFUI.ViewModels;

/// <summary>
/// Base class for all MVVM ViewModels, providing standard INotifyPropertyChanged plumbing,
/// asynchronous execution state (IsBusy, BusyMessage), and error tracking (HasError, ErrorMessage).
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    private bool _isBusy;
    private string? _busyMessage;
    private bool _hasError;
    private string? _errorMessage;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets whether an asynchronous operation is currently in progress.
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    /// <summary>
    /// Optional status or descriptive message accompanying an active busy/loading operation.
    /// </summary>
    public string? BusyMessage
    {
        get => _busyMessage;
        set => SetProperty(ref _busyMessage, value);
    }

    /// <summary>
    /// Gets whether an operation has produced an unhandled or reported error state.
    /// </summary>
    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    /// <summary>
    /// Descriptive error message for user presentation.
    /// </summary>
    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (SetProperty(ref _errorMessage, value))
            {
                HasError = !string.IsNullOrWhiteSpace(value);
            }
        }
    }

    /// <summary>
    /// Updates the busy state and optional progress description.
    /// </summary>
    public void SetBusy(bool isBusy, string? message = null)
    {
        IsBusy = isBusy;
        BusyMessage = isBusy ? message : null;
    }

    /// <summary>
    /// Sets an error message and flags HasError as true.
    /// </summary>
    public void SetError(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Clears any reported error state.
    /// </summary>
    public void ClearError()
    {
        ErrorMessage = null;
        HasError = false;
    }

    /// <summary>
    /// Executes an asynchronous operation safely with automatic IsBusy state management and error capture.
    /// </summary>
    public async Task ExecuteAsync(Func<Task> operation, string? busyMessage = null, Action<Exception>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(operation);

        try
        {
            ClearError();
            SetBusy(true, busyMessage);
            await operation();
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
            if (onError != null)
            {
                onError(ex);
            }
            else
            {
                // Rethrow if no custom handler is provided so callers can inspect
                throw;
            }
        }
        finally
        {
            SetBusy(false);
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
