using MudBlazor;

namespace Client.Services;

internal interface ILoadingIndicator
{
    EventHandler<bool> OnLoadingChanged { get; set; }
    Task ShowLoadingIndicator(Func<Task> func);
}

public class LoadingIndicator : ILoadingIndicator
{
    private readonly ISnackbar _snackBar;
    public EventHandler<bool> OnLoadingChanged { get; set; }

    public LoadingIndicator(ISnackbar snackbar)
    {
        _snackBar = snackbar;
    }

    public async Task ShowLoadingIndicator(Func<Task> func)
    {
        try
        {
            OnLoadingChanged?.Invoke(this, true);
            await func();
        }
        catch (Exception exception)
        {
            _snackBar.Add(exception.Message, Severity.Error);
            Console.WriteLine(exception.Message);
        }
        finally
        {
            OnLoadingChanged?.Invoke(this, false);
        }
    }
}
